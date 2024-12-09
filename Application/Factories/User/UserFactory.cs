using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Common.Record.UserPanel;
using Application.Common.ViewModel;
using Application.Common.ViewModel.User;
using Application.Core;
using Domain.Entities.Business;
using Domain.Entities.Employment;
using Domain.Entities.Resume;
using Domain.Entities.System.Identity;
using Domain.Enum;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Factories.User
{
    public class UserFactory : IUserFactory
    {
        private readonly IEfRepository<EducationalRecordEntity> _educationRepository;
        private readonly IEfRepository<WorkExperienceEntity> _experienceRepository;
        private readonly IEfRepository<ResumeEntity> _resumeRepository;
        private readonly IEfRepository<BusinessPictureEntity> _pictureRepository;
        private readonly IEfRepository<BusinessPropertyEntity> _propertyRepository;
        private readonly IEfRepository<BusinessTimeEntity> _timeRepository;
        private readonly IEfRepository<BusinessEntity> _businessRepository;
        private readonly IEfRepository<EmploymentEntity> _employmentRepository;
        private readonly IEfRepository<EmploymentRequestEntity> _employmentRequestRepsitory;
        private readonly ILogger<UserFactory> _logger;
        private readonly IContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public UserFactory(UserManager<UserEntity> userManager, IContext
            context, ILogger<UserFactory> logger,
            IEfRepository<BusinessTimeEntity> timeRepository,
            IEfRepository<BusinessEntity> businessRepository,
            IEfRepository<BusinessPictureEntity> pictureRepository,
            IEfRepository<BusinessPropertyEntity> propertyRepository,
            IEfRepository<EmploymentEntity> employmentRepository,
            IEfRepository<ResumeEntity> resumeRepository,
             IEfRepository<EducationalRecordEntity> educationRepository,
            IEfRepository<WorkExperienceEntity> experienceRepository,
            IEfRepository<EmploymentRequestEntity> employmentRequestRepsitory
            )
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _timeRepository = timeRepository;
            _businessRepository = businessRepository;
            _pictureRepository = pictureRepository;
            _propertyRepository = propertyRepository;
            _employmentRepository = employmentRepository;
            _resumeRepository = resumeRepository;
            _educationRepository = educationRepository;
            _experienceRepository = experienceRepository;
            _employmentRequestRepsitory = employmentRequestRepsitory;
        }

        private delegate Task<Result> ChangeResumeEvent(string json, Guid UserId, CancellationToken cancellation);





        private delegate Task<Result>
            UserUpadatePasswordEvent(UserEntity user, string password, CancellationToken cancellation);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserInformationViewModel> GetUserInformationAsync(Guid Id,
            CancellationToken cancellation = default)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(s => s.Id == Id, cancellation);
            UserInformationViewModel information = user.Adapt<UserInformationViewModel>();
            return information;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<UserInformationRecord> GetUserInformationProfileAsync(Guid Id, CancellationToken cancellation = default)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(s => s.Id == Id, cancellation);
            UserInformationRecord userInformationRecord = user.Adapt<UserInformationRecord>();
            return userInformationRecord;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<Result> UpdateUserInformationProfileAsync(UserInformationRecord record,
            CancellationToken cancellation = default)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(s => s.Id == record.Id, cancellation);
            if (user == null)
            {
                _logger.LogError("خطای واکشی اطلاعات کاربر برای ویرایش پروفایل رخ داده است.");
                return Result.Fail(FailMessage.PublicInternalError);
            }
            record.Adapt(user);
            if (record.AvatarFile != null)
            {
                var resultRemoveImage = record.Avatar!.RemoveImage("User");
                if (!resultRemoveImage.IsSuccess)
                {
                    _logger.LogError("خطای حذف تصویر قدیمی کاربر برای ویرایش پروفایل رخ داده است.");
                    return Result.Fail(FailMessage.PublicInternalError);
                }
                user.Avatar = record.AvatarFile.UploadImage("User");
            }

            try
            {
                using var transaction = await _context.BeginTransactionAsync();

                var resultUpdateUser = await _userManager.UpdateAsync(user);
                if (!resultUpdateUser.Succeeded)
                {
                    string error = string.Empty;
                    foreach (var item in resultUpdateUser.Errors)
                    {
                        error += item.Description;
                    }
                    _logger.LogError($"خطای حذف تصویر قدیمی کاربر برای ویرایش پروفایل رخ داده است. {error}");
                    return Result.Fail(FailMessage.PublicInternalError);
                }
                if (!string.IsNullOrEmpty(record.Password))
                {
                    UserUpadatePasswordEvent updatePassword = UpdatePasswoedAsync;
                    var resultSetNewPassword = await updatePassword(user, record.Password, cancellation);
                    if (!resultSetNewPassword.IsSuccess)
                    {
                        await transaction.RollbackAsync();
                        return Result.Fail(FailMessage.PublicInternalError);
                    }
                }
                await transaction.CommitAsync(cancellation);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای غیر منتظره در ویرایش پروفایل کاربر رخ داده است.-{ex.Message}");
            }
            return Result.Fail(FailMessage.PublicInternalError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        private async Task<Result> UpdatePasswoedAsync
            (UserEntity user, string password, CancellationToken cancellation = default)
        {
            var resultRemovePassword = await _userManager.RemovePasswordAsync(user);
            if (!resultRemovePassword.Succeeded)
            {
                string error = string.Empty;
                foreach (var item in resultRemovePassword.Errors)
                {
                    error += item.Description;
                }
                _logger.LogError($"خطای حذف رمز قدیمی کاربر برای ویرایش پروفایل رخ داده است. {error}");
                return Result.Fail();
            }
            var setNewPassword = await _userManager.AddPasswordAsync(user, password);
            if (!setNewPassword.Succeeded)
            {
                string error = string.Empty;
                foreach (var item in setNewPassword.Errors)
                {
                    error += item.Description;
                }
                _logger.LogError($"خطای حذف رمز قدیمی کاربر برای ویرایش پروفایل رخ داده است. {error}");
                return Result.Fail();
            }
            return Result.Success();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<Result> AddBusinessAsync(AddBusinessRecord record, CancellationToken cancellation = default)
        {
            var businessEntity = record.Adapt<BusinessEntity>();
            businessEntity.status = StatusEnum.Waiting;
            businessEntity.IsActive = false;
            if (!string.IsNullOrEmpty(record.JsonTime) && record.JsonTime != "[]")
            {
                businessEntity.IsTime = true;
            }
            using var transaction = await _context.BeginTransactionAsync();

            try
            {
                await _businessRepository.InsertAsync(businessEntity, cancellation);

                if (record.Files != null)
                {
                    var businessPictures = new List<BusinessPictureEntity>();

                    foreach (var file in record.Files)
                    {
                        var pictureEntity = new BusinessPictureEntity
                        {
                            Image = file.UploadImage("Business"),
                            BusinessId = businessEntity.Id
                        };

                        businessPictures.Add(pictureEntity);
                    }

                    await _pictureRepository.InsertListAsync(businessPictures, cancellation);
                }

                if (!string.IsNullOrEmpty(record.JsonKeyValue))
                {
                    var properties = JsonSerializer.Deserialize<List<KeyValueModel>>(record.JsonKeyValue);

                    if (properties != null)
                    {
                        var businessProperties = properties.Select(property => new BusinessPropertyEntity
                        {
                            Key = property.Key,
                            Value = property.Value,
                            BusinessId = businessEntity.Id
                        }).ToList();

                        await _propertyRepository.InsertListAsync(businessProperties, cancellation);
                    }
                }

                if (!string.IsNullOrEmpty(record.JsonTime))
                {
                    var times = JsonSerializer.Deserialize<List<KeyValueModel>>(record.JsonTime);

                    if (times != null)
                    {
                        var businessTimes = times.Select(time => new BusinessTimeEntity
                        {
                            BusinessId = businessEntity.Id,
                            Day = Enum.TryParse<DayEnum>(time.Key, out var day) ? day : DayEnum.Friday,
                            Time = time.Value
                        }).ToList();

                        await _timeRepository.InsertListAsync(businessTimes, cancellation);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"زمان ایجاد کسب و کار برای کاربر خطایی رخ داده است - {ex.Message}");
                await transaction.RollbackAsync(cancellation);
                return Result.Fail(FailMessage.PublicInternalError);
            }

            await transaction.CommitAsync(cancellation);
            return Result.Success();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<UserBusinessViewModel>> GetUserBusinessesAsync
            (Guid UserId, CancellationToken cancellation = default)
        {
            var query = await _businessRepository.GetByQueryAsync();
            var models = await query.Where(w => w.UserId == UserId).ToListAsync(cancellation);
            List<UserBusinessViewModel> businesses = models.Adapt<List<UserBusinessViewModel>>();
            return businesses;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<UpdateBusinessRecord> GetUserBusinessByIdAsync(Guid id, CancellationToken cancellation = default)
        {
            var query = await _businessRepository.GetByQueryAsync();
            var model = await query.Include(i => i.Pictures)
                .Include(i => i.Properties)
                .Include(i => i.Times)
                .SingleOrDefaultAsync(s => s.Id == id, cancellation);
            UpdateBusinessRecord business = model.Adapt<UpdateBusinessRecord>();
            return business;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result> UpdateUserBusinessAsync
            (UpdateBusinessRecord record, CancellationToken cancellation = default)
        {
            var business = await _businessRepository.GetByIdAsync(record.Id, cancellation);
            if (business == null)
            {
                return Result.Fail();
            }
            record.Adapt(business);

            if (!string.IsNullOrEmpty(record.JsonTime) && record.JsonTime != "[]")
            {
                business.IsTime = true;
            }
            using var transaction = await _context.BeginTransactionAsync();

            try
            {
                await _businessRepository.UpdateAsync(business);

                if (record.Files != null)
                {
                    var businessPictures = new List<BusinessPictureEntity>();

                    foreach (var file in record.Files)
                    {
                        var pictureEntity = new BusinessPictureEntity
                        {
                            Image = file.UploadImage("Business"),
                            BusinessId = business.Id
                        };

                        businessPictures.Add(pictureEntity);
                    }

                    await _pictureRepository.InsertListAsync(businessPictures, cancellation);
                }

                if (!string.IsNullOrEmpty(record.JsonKeyValue))
                {
                    var properties = JsonSerializer.Deserialize<List<KeyValueModel>>(record.JsonKeyValue);

                    if (properties != null)
                    {
                        var businessProperties = properties.Select(property => new BusinessPropertyEntity
                        {
                            Key = property.Key,
                            Value = property.Value,
                            BusinessId = business.Id
                        }).ToList();

                        await _propertyRepository.InsertListAsync(businessProperties, cancellation);
                    }
                }

                if (!string.IsNullOrEmpty(record.JsonTime))
                {
                    var times = JsonSerializer.Deserialize<List<KeyValueModel>>(record.JsonTime);

                    if (times != null)
                    {
                        var businessTimes = times.Select(time => new BusinessTimeEntity
                        {
                            BusinessId = business.Id,
                            Day = Enum.TryParse<DayEnum>(time.Key, out var day) ? day : DayEnum.Friday,
                            Time = time.Value
                        }).ToList();

                        await _timeRepository.InsertListAsync(businessTimes, cancellation);
                    }
                }



                if (!string.IsNullOrEmpty(record.JsonRemoveImage) && record.JsonRemoveImage != "[]")
                {
                    var deletedIdImages = JsonSerializer.Deserialize<List<ItemIdModel>>(record.JsonRemoveImage);
                    List<BusinessPictureEntity> pictures = new();

                    foreach (var item in deletedIdImages!)
                    {
                        var pictureEntity = await _pictureRepository.GetByIdAsync(item.Id, cancellation);
                        var resultRemoveImage = pictureEntity.Image!.RemoveImage("Business");
                        if (!resultRemoveImage.IsSuccess)
                        {
                            _logger.LogError($"زمان ویرایش کسب و کار برای کاربر(حذف تصویر قدیمی) خطایی رخ داده است");
                            await transaction.RollbackAsync(cancellation);
                            return Result.Fail(FailMessage.PublicInternalError);
                        }
                        pictures.Add(pictureEntity);
                    }
                    await _pictureRepository.DeleteListAsync(pictures);
                }



                if (!string.IsNullOrEmpty(record.JsonRemoveKeyValue) && record.JsonRemoveKeyValue != "[]")
                {
                    var deletedIdProperties = JsonSerializer.Deserialize<List<ItemIdModel>>(record.JsonRemoveKeyValue);
                    List<BusinessPropertyEntity> properties = new();

                    foreach (var item in deletedIdProperties!)
                    {
                        var propertyEntity = await _propertyRepository.GetByIdAsync(item.Id, cancellation);

                        properties.Add(propertyEntity);
                    }
                    await _propertyRepository.DeleteListAsync(properties);
                }


                if (!string.IsNullOrEmpty(record.JsonRemoveTime) && record.JsonRemoveTime != "[]")
                {
                    var deletedIdTimes = JsonSerializer.Deserialize<List<ItemIdModel>>(record.JsonRemoveTime);
                    List<BusinessTimeEntity> times = new();

                    foreach (var item in deletedIdTimes!)
                    {
                        var timeEntity = await _timeRepository.GetByIdAsync(item.Id, cancellation);

                        times.Add(timeEntity);
                    }
                    await _timeRepository.DeleteListAsync(times);
                }





            }
            catch (Exception ex)
            {
                _logger.LogError($"زمان ویرایش کسب و کار برای کاربر خطایی رخ داده است - {ex.Message}");
                await transaction.RollbackAsync(cancellation);
                return Result.Fail(FailMessage.PublicInternalError);
            }

            await transaction.CommitAsync(cancellation);
            return Result.Success();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<PaginatedList<UserEmploymentAdvertisementViewModel>>
            GetUserEmploymentAdvertisementsAsync(UserPaginationRecord userPagination, CancellationToken cancellation = default)
        {
            var query = await _employmentRepository.GetByQueryAsync();
            query = query.Include(i => i.User).Include(i => i.Business).Where(w => w.UserId == userPagination.UserId);
            int count;
            PaginatedList<UserEmploymentAdvertisementViewModel> model = new();
            if (string.IsNullOrEmpty(userPagination.pagination!.keyword))
            {
                count = query.Count().PageCount(userPagination.pagination!.pageSize);
                model = await query
               .MappingedAsync<EmploymentEntity, UserEmploymentAdvertisementViewModel>
                (userPagination.pagination!.curentPage,
                userPagination.pagination!.pageSize, count);
            }
            else
            {
                count = query.Count(c =>
                c.Title!.Contains(userPagination.pagination!.keyword) &&
                c.Code!.ToString().Contains(userPagination.pagination!.keyword))
                 .PageCount(userPagination.pagination!.pageSize);

                model = await query.Where(w =>
                w.Title!.Contains(userPagination.pagination!.keyword) &&
                w.Code!.ToString().Contains(userPagination.pagination!.keyword)
            )
                .MappingedAsync<EmploymentEntity, UserEmploymentAdvertisementViewModel>(userPagination.pagination!
                .curentPage, userPagination.pagination!.pageSize, count);
            }
            return model;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<List<ItemViewModel<string, string>>> GetUserBusinessesItemAsync(Guid UserId)
        {
            var query = await _businessRepository.GetByQueryAsync();
            return await query.
                Where(w => w.UserId == UserId && w.IsActive == true && w.status == StatusEnum.Accepted)
                .Select(s => new ItemViewModel<string, string>
                {
                    Id = s.Id.ToString(),
                    Title = s.AccountName
                }).ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result> AddEmploymentAsync(AddEmploymentRecord record, CancellationToken cancellation = default)
        {
            EmploymentEntity employment = record.Adapt<EmploymentEntity>();
            employment.IsActive = false;
            employment.status = StatusEnum.Waiting;
            try
            {
                await _employmentRepository.InsertAsync(employment, cancellation);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای ایجاد آگهی توسط کاربر رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result> GetUserEmploymentByIdAsync
            (Guid Id, CancellationToken cancellation = default)
        {
            var entity = await _employmentRepository.GetByIdAsync(Id, cancellation);
            if (entity == null)
            {
                _logger.LogWarning("آگهی پیدا نشد.");
                return Result.Fail();
            }
            UpdateEmploymentRecord record = entity.Adapt<UpdateEmploymentRecord>();

            return Result.Success(record);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result> UpdateUserEmploymentAsync(UpdateEmploymentRecord record, CancellationToken cancellation = default)
        {
            var entity = await _employmentRepository.GetByIdAsync(record.Id, cancellation);
            if (entity == null)
            {
                return Result.Fail(FailMessage.Internal);
            }


            long code = (long)entity.Code!;
            entity = record.Adapt<EmploymentEntity>();
            entity.Code = code;
            try
            {
                await _employmentRepository.UpdateAsync(entity);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای بروزرسانی آگهی توسط کاربر رخ داده است.");
            }
            return Result.Fail(FailMessage.Internal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserStatisticalInformationViewModel> GetUserStatisticalInformationAsync(Guid Id, CancellationToken cancellation = default)
        {
            var queryBusiness = await _businessRepository.GetByQueryAsync();
            var queryEmployment = await _employmentRepository.GetByQueryAsync();
            UserStatisticalInformationViewModel statisticalInformation = new();
            statisticalInformation.BusinessCount = await queryBusiness.CountAsync();
            statisticalInformation.EmploymentAdCount = await queryEmployment.CountAsync();
            return statisticalInformation;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MyResumeRecord> GetUserResumeAsync(Guid UserId, CancellationToken cancellation = default)
        {
            var query = await _resumeRepository.GetByQueryAsync();
            bool IsExsit = await query.AnyAsync(a => a.UserId == UserId);
            MyResumeRecord record = new();
            ResumeEntity? resume = await query.SingleOrDefaultAsync(s => s.UserId == UserId, cancellation);
            if (resume == null)
            {
                resume = new();
                resume!.UserId = UserId;
                await _resumeRepository.InsertAsync(resume, cancellation);
            }
            record = resume.Adapt<MyResumeRecord>();
            if (IsExsit)
            {
                var educationQuery = await _educationRepository.GetByQueryAsync();
                var experienceQuery = await _experienceRepository.GetByQueryAsync();

                var education = await educationQuery.Where(w => w.ResumeId == resume.Id)
                    .Select(s => new ResumeHistory
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Description = s.Description,
                        FromDate = s.FromDate,
                        ToDate = s.ToDate,
                        IsActive = s.IsStudying
                    }).ToListAsync();
                var experience = await experienceQuery.Where(w => w.ResumeId == resume.Id)
                     .Select(s => new ResumeHistory
                     {
                         Id = s.Id,
                         Title = s.Title,
                         Description = s.Description,
                         FromDate = s.FromDate,
                         ToDate = s.ToDate,
                         IsActive = s.IsWorking
                     }).ToListAsync();
                record.JsonEducationals = JsonSerializer.Serialize(education);
                record.JsonExperiences = JsonSerializer.Serialize(experience);
                record.Educationals = education;
                record.Experiences = experience;
            }
            if (!string.IsNullOrEmpty(resume.Skills))
            {

                char separator = '-';

                string[] skillArray = resume.Skills.Split(separator); // تقسیم رشته بر اساس خط تیره

                record.UserSkills = new List<string>(skillArray); // تبدیل آرایه به لیست
            }
            return record;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<Result> UpdateMyResumeAsync(MyResumeRecord record,
            CancellationToken cancellation)
        {
            var resume = await _resumeRepository.GetByIdAsync(record.Id, cancellation);
            record.Adapt(resume);

            string skills = string.Empty;
            if (record.UserSkills == null)
            {
                resume.Skills = null;
            }
            else
            {
                foreach (var skill in record.UserSkills)
                {
                    skills += "-" + skill;
                }
                resume.Skills = skills;
            }

            using (var transaction = await _context.BeginTransactionAsync())
            {
                try
                {
                    await _resumeRepository.UpdateAsync(resume);
                    ChangeResumeEvent changerEducation = ChangeEducations;
                    if (!string.IsNullOrEmpty(record.JsonEducationals))
                    {

                        var resultChangeEducation = await changerEducation(record.JsonEducationals, record.Id, cancellation);
                        if (!resultChangeEducation.IsSuccess)
                        {
                            await transaction.RollbackAsync(cancellation);
                            return Result.Fail(FailMessage.PublicInternalError);
                        }
                    }
                    ChangeResumeEvent changerExperienc = ChangeExperiences;
                    if (!string.IsNullOrEmpty(record.JsonExperiences))
                    {
                        var resultChangeExperienc = await changerExperienc(record.JsonExperiences, record.Id, cancellation);
                        if (!resultChangeExperienc.IsSuccess)
                        {
                            await transaction.RollbackAsync(cancellation);
                            return Result.Fail(FailMessage.PublicInternalError);
                        }
                    }
                    await transaction.CommitAsync(cancellation);


                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellation);
                    _logger.LogError($"خطای بروزرسانی رزومه توسط کاربر رخ داده است - {ex.Message}");
                }
            }
            return Result.Success();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="JsonSerialized"></param>
        /// <param name="UserId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        private async Task<Result> ChangeExperiences(string JsonSerialized, Guid Id, CancellationToken cancellation)
        {
            var quey = await _experienceRepository.GetByQueryAsync();
            var experiences = await quey.Where(w=>w.ResumeId==Id).ToListAsync(cancellation);
            try
            {
                if (experiences != null)
                {
                    await _experienceRepository.DeleteListAsync(experiences.ToList());
                }
                if (JsonSerialized == "[]")
                {
                    return Result.Success();
                }
                var modelDeserialized = JsonSerializer.Deserialize<List<ResumeHistory>>(JsonSerialized);
                var model = modelDeserialized!.Select(s => new WorkExperienceEntity
                {
                    Id = s.Id,
                    Description = s.Description,
                    FromDate = s.FromDate,
                    IsWorking = s.IsActive,
                    ToDate = s.ToDate,
                    ResumeId = Id,Title = s.Title
                }).ToList();

                await _experienceRepository.InsertListAsync(model!, cancellation);
                return Result.Success();

            }
            catch (Exception ex)
            {
                _logger.LogError($"خطا تغییر سوابق کاری توسط کاربر رخ داده است. - {ex.Message}");
            }
            return Result.Fail();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="JsonSerialized"></param>
        /// <param name="UserId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        private async Task<Result> ChangeEducations(string JsonSerialized, Guid Id, CancellationToken cancellation)
        {
            var query = await _educationRepository.GetByQueryAsync();
            var educations = await query.Where(w=>w.ResumeId==Id).ToListAsync(cancellation);
            try
            {
                if (educations != null)
                {
                    await _educationRepository.DeleteListAsync(educations.ToList());
                }
                if (JsonSerialized == "[]")
                {
                    return Result.Success();
                }
                var modelDeserialized = JsonSerializer.Deserialize<List<ResumeHistory>>(JsonSerialized);
                var model = modelDeserialized!.Select(s => new EducationalRecordEntity
                {
                    Id = s.Id,
                    Description = s.Description,
                    FromDate = s.FromDate,
                    IsStudying = s.IsActive,
                    ToDate = s.ToDate,
                    ResumeId = Id,
                    Title = s.Title
                }).ToList();
                await _educationRepository.InsertListAsync(model!, cancellation);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطا تغییر سوابق آموزشی توسط کاربر رخ داده است. - {ex.Message}");
            }
            return Result.Fail();

        }

        public async Task<Result> GetRequestEmploymentByEmploymentIdAsync
            (Pagination pagination,Guid EmploymentId, CancellationToken cancellation = default)
        {
            try
            {
                var query = await _employmentRequestRepsitory.GetByQueryAsync();
                query = query.Include(i => i.Resume).ThenInclude(t => t.User)
                  .Where(w => w.EmploymentId == EmploymentId);
                int count;
                PaginatedList<RequestEmploymentViewModel> model = new();
                if (string.IsNullOrEmpty(pagination!.keyword))
                {
                    count = query.Count().PageCount(pagination!.pageSize);
                    model = await query
                   .MappingedAsync<EmploymentRequestEntity, RequestEmploymentViewModel>
                    (pagination!.curentPage,
                   pagination!.pageSize, count);
                }
                else
                {
                    query = query.Where(w => w.Resume!.User!.FirstName.Contains(pagination!.keyword) ||
                    w.Resume.User.LastName.Contains(pagination!.keyword));
                    count = query.Count(
                        s => s.Resume!.User!.FirstName.Contains(pagination!.keyword) ||
                        s.Resume.User.LastName.Contains(pagination!.keyword)
                        ).PageCount(pagination!.pageSize);
                    model = await query
                   .MappingedAsync<EmploymentRequestEntity, RequestEmploymentViewModel>
                    (pagination!.curentPage,
                    pagination!.pageSize, count);
                }
                return Result.Success(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"لیست درخواست های آگهی استخدام واکشی نشد - {ex.Message}");
                return Result.Fail();
            }
        }

        public async Task<Result> ChangeEmploymentRequestStatusAsync
            (UpdateEmploymentRequestRecord record, CancellationToken cancellation = default)
        {
            try
            {
                var model = await _employmentRequestRepsitory.GetByIdAsync(record.Id, cancellation);
                model.Comment = record.Comment;
                switch (record.Status)
                {
                    case "0":
                        model.Status = StatusEnum.Accepted;
                        break;
                    case "1":
                        model.Status = StatusEnum.Rejected;
                        break;
                    case "2":
                        model.Status = StatusEnum.Waiting;
                        break;
                }


                await _employmentRequestRepsitory.UpdateAsync(model);
            }
            catch(Exception ex)
            {
                _logger.LogError($"هنگام تغییر وضعیت درخواست استخدام خطایی رخ داده است. - {ex.Message}");
                return Result.Fail();
            }
            return Result.Success();
        }

        public async Task<List<EmploymentRequestAlertViewModel>> 
            GetEmploymentRequestAlertAsync(Guid UserId, CancellationToken cancellation = default)
        {
            var query = await _employmentRequestRepsitory.GetByQueryAsync();
            var list=await query
                .Include(i=>i.Employment)
                .Include(i=>i.Resume)
                .Where(w=>w.Resume!.UserId == UserId).ToListAsync();
            List<EmploymentRequestAlertViewModel> alerts = new();
            var config = new TypeAdapterConfig();
            config.NewConfig<EmploymentRequestEntity, EmploymentRequestAlertViewModel>()
                .Map(a=>a.Id,b=>b.Id)
                .Map(a=>a.Comment,b=>b.Comment)
                .Map(a=>a.EmploymentTitle,b=>b.Employment!.Title)
                .Map(a=>a.Status,b=>b.Status.MapStatus()).Compile();
            alerts=list.Adapt<List<EmploymentRequestAlertViewModel>>(config);  
            return alerts;
        }

        public async Task<Result> GetResumeForPdfGeneratorAsync
            (Guid Id, CancellationToken cancellation = default)
        {
           var query=await _resumeRepository.GetByQueryAsync();
            var model=await query
                .Include(i=>i.Educationals)
                .Include(i=>i.Experiences)
                .Include(i=>i.User)
                .SingleOrDefaultAsync(s=>s.Id==Id,cancellation);
            if (model == null)
            {
                return Result.Fail("رزومه ای برای کاربر وجود ندارد.");
            }
            RequestResumeViewModel resume = new();
            resume = model!.Adapt<RequestResumeViewModel>();
            return Result.Success(resume);
        }
    }
}
