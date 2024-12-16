using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Record;
using Application.Common.Record.Home;
using Application.Common.ViewModel.Business;
using Application.Common.ViewModel.Employment;
using Application.Common.ViewModel.Home;
using Domain.Entities.Business;
using Domain.Entities.Employment;
using Domain.Entities.System;
using Domain.Entities.System.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace Application.Factories.Home
{
    public class HomeFactory : IHomeFactory
    {
        private readonly ILogger<HomeFactory> _logger;
        private readonly IContext _context;
        private readonly IDistributedCache _cache;
        private readonly IEfRepository<ContactEntity> _contactRepository;
        private readonly IEfRepository<ProvinceEntity> _provinceRepository;
        private readonly IEfRepository<SettingEntity> _settingRepository;
        private readonly IEfRepository<CategoryEntity> _categoryRepository;
        private readonly IEfRepository<BusinessEntity> _businessRepository;
        private readonly IEfRepository<EmploymentEntity> _employmentRepository;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        public HomeFactory(
            ILogger<HomeFactory> logger,
            IContext context,
            IEfRepository<ProvinceEntity> provinceRepository,
            IEfRepository<EmploymentEntity> employmentRepository,
            IEfRepository<CategoryEntity> categoryRepository,
            IEfRepository<SettingEntity> settingRepository,
            IEfRepository<BusinessEntity> businessRepository,
            IEfRepository<ContactEntity> contactRepository,
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager,
            IDistributedCache cache)
        {
            _contactRepository = contactRepository;
            _logger = logger;
            _context = context;
            _cache = cache;
            _provinceRepository = provinceRepository;
            _settingRepository = settingRepository;
            _categoryRepository = categoryRepository;
            _businessRepository = businessRepository;
            _employmentRepository = employmentRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<LastBusinessViewModel>
            GetLastBusinessesAsync(CancellationToken cancellation = default)
        {
            var query = await _businessRepository.GetByQueryAsync();
            var model = await query
                .Where(w => w.IsActive == true && w.status == Domain.Enum.StatusEnum.Accepted)
                .Include(i => i.Province)
                .Include(i => i.City)
                .Include(i => i.Category)
                .Include(i => i.Pictures).Take(8)
                .Select(s => new
                {
                    Id = s.Id,
                    ProvinceName = s.Province!.Name,
                    CategoryName = s.Category!.Name,
                    CityName = s.City!.Name,
                    Code = s.Code,
                    AccountName = s.AccountName,
                    BusinessPictureImage = s.Pictures!.FirstOrDefault()!.Image
                })
                .ToListAsync();
            LastBusinessViewModel business = new();
            business.businesses = model.Adapt<List<BusinessViewModel>>();
            var setting = await GetSettingFromCacheAsync(cancellation);
            business.BusinessText = setting.BusinessText;
            return business;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<HeaderViewModel> GetHeaderAsync(CancellationToken cancellation
            = default)
        {

            var setting = await GetSettingFromCacheAsync(cancellation);
            HeaderViewModel header = setting.Adapt<HeaderViewModel>();
            return header;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ParentCategoryViewModel>> GetParentCategoryAsync(CancellationToken cancellation = default)
        {
            var categories = await _categoryRepository.GetByQueryAsync();
            categories = categories.Where(w => w.ParentCategoryId == null)
                .Include(i => i.Businesses);
            IEnumerable<ParentCategoryViewModel> parentCategories
                = new List<ParentCategoryViewModel>();

            var config = new TypeAdapterConfig();
            config.NewConfig<ParentCategoryViewModel, CategoryEntity>()
                .Map(a => a.Image, b => b.Image)
                .Map(a => a.Id, b => b.Id)
                .Map(a => a.Name, b => b.Name)
                .Map(a => a.Businesses!.Count, b => b.Count)
                .Compile();
            parentCategories = categories.Adapt<List<ParentCategoryViewModel>>(config);
            return parentCategories;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        private async Task<SettingChached> GetSettingFromCacheAsync
            (CancellationToken cancellation = default)
        {
            SettingChached setting;


            var settingCached = await _cache.GetAsync(CacheKey.SettinKey);
            if (settingCached == null)
            {
                var  settingdb = await _settingRepository.FirstOrDefaultAsync(cancellation);
                setting = settingdb.Adapt<SettingChached>();
                var serializedSetting = JsonSerializer.Serialize(setting);
                byte[] settingEncoded = Encoding.UTF8.GetBytes(serializedSetting);

                await _cache.SetAsync(CacheKey.SettinKey, settingEncoded,
                     new DistributedCacheEntryOptions()
          
                     .SetAbsoluteExpiration(TimeSpan.FromDays(36500)));
            }
            else
            {
                 setting = JsonSerializer.Deserialize<SettingChached>
                    (Encoding.UTF8.GetString(settingCached!))!;
            }
            return setting;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<LastEmploymentViewModel> GetLastEmploymentAsync(CancellationToken cancellation = default)
        {
            var query = await _employmentRepository.GetByQueryAsync();
            var model = await query
                .Where(w => w.IsActive == true && w.status == Domain.Enum.StatusEnum.Accepted)
                 .Include(i => i.Province).
                  Include(i => i.City).
                  Include(i => i.Business).ThenInclude(t => t.Pictures)
                 .Select(s => new
                 {
                     Id = s.Id,
                     Code = s.Code,
                     Title = s.Title,
                     Salary = s.Salary,
                     BusinessAccountName = s.Business!.AccountName,
                     ProvinceName = s.Province!.Name,
                     CityName = s.City!.Name,
                     Image = s.Business!.Pictures!.FirstOrDefault()!.Image
                 })
                 .ToListAsync();
            LastEmploymentViewModel employment =
                new ();
            employment.employments = model.Adapt<List<EmploymentViewModel>>();
            var setting = await GetSettingFromCacheAsync(cancellation);
            employment.EmploymentText = setting.EmploymentText;
            return employment;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result> SignInAsync(SignInRecord record, CancellationToken cancellation = default)
        {
            var user = await _userManager.FindByNameAsync(record.NationalCode!);
            if (user == null)
            {
                return Result.Fail(FailMessage.UserNotFound);
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, record.Password!);
            if (!checkPassword)
            {
                return Result.Fail(FailMessage.UserNotFound);
            }
            if (user.PhoneNumberConfirmed is false || user.EmailConfirmed is false)
            {
                return Result.Fail(FailMessage.AccountNotActive);
            }
            var checkSignIn = await _signInManager.CheckPasswordSignInAsync(user, record.Password!, true);
            if (!checkSignIn.Succeeded)
            {
                return Result.Fail(FailMessage.UserNotFound);
            }
            string path = "/";
            var userRoles=await _userManager.GetRolesAsync(user);
            if (userRoles != null && userRoles.Any(a=>a== "Admin")) 
            {
                path = "/Admin/Dashboard";
            }
            await _signInManager.SignInAsync(user, isPersistent: record.SavePassword);
            return Result.Success(path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result> SignUpAsync(SignUpRecord record, CancellationToken cancellation = default)
        {
            StringBuilder textError = new StringBuilder();
            UserEntity user = record.Adapt<UserEntity>();
            user.Avatar = "defaultAvatar.jpg";
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            using (var transaction = await _context.BeginTransactionAsync())
            {

                var resultInsertUser = await _userManager.CreateAsync(user);
                if (!resultInsertUser.Succeeded)
                {
                    foreach (var error in resultInsertUser.Errors)
                    {
                        textError.AppendLine(error.Description);
                    }
                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای ایجاد کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(FailMessage.SignUpError);
                }
                var resultInserUserRole = await _userManager.AddToRoleAsync(user, "User");
                if (!resultInserUserRole.Succeeded)
                {
                    foreach (var error in resultInserUserRole.Errors)
                    {
                        textError.AppendLine(error.Description);
                    }
                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای ایجاد نقش کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(FailMessage.SignUpError);
                }
                var resultInserUserPassword = await _userManager.AddPasswordAsync(user, record.Password!);
                if (!resultInserUserPassword.Succeeded)
                {
                    foreach (var error in resultInserUserPassword.Errors)
                    {
                        textError.AppendLine(error.Description);
                    }
                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای ایجاد رمز عبور کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(FailMessage.SignUpError);
                }
                await transaction.CommitAsync();

                List<Claim> claims = new();
                claims.Add(new Claim("Id", user.Id.ToString()));
                claims.Add(new Claim("FullName", user.FirstName + user.LastName));

                await _userManager.AddClaimsAsync(user, claims);
                await _signInManager.SignInAsync(user, isPersistent: true);
                return Result.Success();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<LogoViewModel> GetLogoAsync(CancellationToken cancellation = default)
        {
            var setting = await GetSettingFromCacheAsync(cancellation);
            LogoViewModel logo = setting.Adapt<LogoViewModel>();
            return logo;
        }

        public async Task<ProvinceViewModel> GetProvinceInfoAsync(CancellationToken cancellation = default)
        {
            ProvinceViewModel province = new();

            var query = await _provinceRepository.GetByQueryAsync();
            var model = await query.Include(i => i.Employments).Take(6)
                .Select(s => new ProvinceInfoViewModel
                {
                    Id = s.Id,
                    Image = s.Image,
                    Name = s.Name,
                    AdsCount = s.Employments!.Count()

                }).ToListAsync(cancellation);
            province.provinces = model;
            var setting = await GetSettingFromCacheAsync(cancellation);
            province.ProvinceText = setting.ProvinceText;
            return province;
        }

        public async Task<CategoryViewModel> GetCategoriesAsync(CancellationToken cancellation =default)
        {
            CategoryViewModel category = new();
            var query = await _categoryRepository.GetByQueryAsync();
            var model = await query.Where(w => w.ParentCategoryId == null)
                .Include(i => i.Businesses)
                .Include(i => i.ChildCategories!)
                .ThenInclude(t => t.Businesses)
                .Select(s => new CategoryInfoViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Image = s.Image,
                    Count = s.Businesses!.Count(),
                    SubCategories = s.ChildCategories!.Where(w=>w.ParentCategoryId !=null)
                    .Select(c => new SubCategoryInfoViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Count = c.Businesses!.Count()
                    }).ToList()
                }).ToListAsync(cancellation);
            category.categories = model;
            var setting = await GetSettingFromCacheAsync(cancellation);
            category.CategoryText = setting.CategoryText;
            return category;
        }

        public async Task<SocialFooterViewModel> GetSocialsAsync(CancellationToken cancellation = default)
        {
            var setting = await GetSettingFromCacheAsync(cancellation);
            SocialFooterViewModel social = new();
            social = setting.Adapt<SocialFooterViewModel>();
            return social;
        }

        public async Task<InfoContactFooterViewModel> GetInfoContactAsync
            (CancellationToken cancellation = default)
        {
            InfoContactFooterViewModel info = new();
            var setting = await GetSettingFromCacheAsync(cancellation);
            info= setting.Adapt<InfoContactFooterViewModel>();
            return info;
        }

        public async Task<Result> AddContactMessageAsync(AddContactRecord record, 
            CancellationToken cancellation = default)
        {
            ContactEntity contact = record.Adapt<ContactEntity>();
            try
            {
              await _contactRepository.InsertAsync(contact,cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError($"هنگام ثبت پیام خطایی رخ داده است. - {ex.Message}");
                return Result.Fail();
            }
            return Result.Success();
        }

        public async Task<ContactViewModel>
            GetContactUsInformationAsync(CancellationToken cancellation = default)
        {
            var setting=await GetSettingFromCacheAsync(cancellation);
            ContactViewModel contact = new();
            if (setting == null)
            {
                return contact;
            }
             contact = setting.Adapt<ContactViewModel>();
            return contact;
        }

        public async Task<TextViewModel> GetLawAsync(CancellationToken cancellation = default)
        {
            TextViewModel text = new TextViewModel();
            var setting = await _settingRepository.FirstOrDefaultAsync(cancellation);
            text.Text=setting.Law;
            return text;
        }

        public async Task<TextViewModel> GetAboutAsync(CancellationToken cancellation = default)
        {
            TextViewModel text = new TextViewModel();
            var setting = await _settingRepository.FirstOrDefaultAsync(cancellation);
            text.Text = setting.About;
            return text;
        }

        public async Task<Result> OtpSignInAsync(string userName, CancellationToken cancellation = default)
        {
            UserEntity user = null;
            var isPhoneNumber = ValidationHelper.IsPhoneNumber(userName);
            if (isPhoneNumber)
            {
                var resultPhoneNumber = await _userManager.Users.FirstOrDefaultAsync(p => p.PhoneNumber == userName);
                if (resultPhoneNumber != null)
                    user = resultPhoneNumber;
            }
            else
            {
                var isNationalCode = ValidationHelper.IsNationalCode(userName);
                if (isNationalCode)
                {
                    var resultNationalCode = await _userManager.Users.FirstOrDefaultAsync(p => p.UserName == userName);
                    if (resultNationalCode != null)
                        user = resultNationalCode;
                }
            }

            if (user == null)
                return Result.Fail(FailMessage.UserNotFound);

            if (user.PhoneNumberConfirmed is false || user.EmailConfirmed is false)
            {
                return Result.Fail(FailMessage.AccountNotActive);
            }

            string path = "/";
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles != null && userRoles.Any(a => a == "Admin"))
            {
                path = "/Admin/Dashboard";
            }

            await _signInManager.SignInAsync(user, true);

            return Result.Success(path);

        }
    }
}
