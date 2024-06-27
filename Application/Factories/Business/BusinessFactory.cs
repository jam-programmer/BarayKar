using Application.Common;
using Application.Common.Const;
using Application.Common.Interfaces;
using Application.Common.Record.Business;
using Application.Common.ViewModel.Business;
using Application.Common.ViewModel.Home;
using Application.Core;
using Domain.Entities.Business;
using Domain.Entities.System;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Application.Factories.Business
{
    public class BusinessFactory : IBusinessFactory
    {
        private readonly IEfRepository<ProvinceEntity> _provinceRepository;
        private readonly IEfRepository<CategoryEntity> _categoryRepository;
        private readonly IEfRepository<BusinessPictureEntity> _pictureRepository;
        private readonly IEfRepository<BusinessEntity> _businessRepository;
        private readonly IEfRepository<BusinessPropertyEntity> _propertyRepository;
        private readonly IEfRepository<BusinessTimeEntity> _timeRepository;
        private readonly IDapper _dapper;
        public BusinessFactory(
            IEfRepository<BusinessPictureEntity> pictureRepository,
             IEfRepository<BusinessEntity> businessRepository,
             IEfRepository<BusinessPropertyEntity> propertyRepository,
             IEfRepository<BusinessTimeEntity> timeRepository,
             IEfRepository<CategoryEntity> categoryRepository,
             IEfRepository<ProvinceEntity> provinceRepository,
             IDapper dapper
            )
        {
            _provinceRepository = provinceRepository;
            _categoryRepository = categoryRepository;
            _timeRepository = timeRepository;
            _propertyRepository = propertyRepository;
            _businessRepository = businessRepository;
            _pictureRepository = pictureRepository;
            _dapper = dapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<BusinessDetailViewModel> GetBusinessDetailAsync
            (Guid Id, CancellationToken cancellation = default)
        {
            var query = await _businessRepository.GetByQueryAsync();
            var model = await query.Include(i => i.Category)
                .Include(i => i.City)
                .Include(i => i.Province)
                .Include(i => i.User)
                .SingleOrDefaultAsync(s => s.Id == Id, cancellation);
            BusinessDetailViewModel business = model.Adapt<BusinessDetailViewModel>();
            return business;
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result> GetBusinessesAsync(BusinessFilter filter, CancellationToken cancellation = default)
        {
            var query = await _businessRepository.GetByQueryAsync();
            query = query.Include(i => i.Category)
                .Include(i => i.Province).ThenInclude(t => t!.cities);
            BusinessesViewModel businesses = new();
            List<StoredProcedureParmeter> parmeters = new();
            parmeters.Add(new StoredProcedureParmeter()
            {
                ParmeterName = "@page",
                ParmeterType = DbType.Int32,
                ParmeterValue = filter.page
            });

            if (filter.category != null && filter.category.Any())
            {
                string category = string.Empty;

                for (int i = 0; i < filter.category.Count; i++)
                {
                    if (i == 0)
                    {
                        category += filter.category[i].ToString();
                    }
                    else if (i == filter.category.Count && i != 0)
                    {
                        category += filter.category[i].ToString();
                    }
                    else
                    {
                        category += "-" + filter.category[i].ToString();
                    }
                }
                parmeters.Add(new StoredProcedureParmeter()
                {
                    ParmeterName = "@categories",
                    ParmeterType = DbType.String,
                    ParmeterValue = category
                });
                query = query.Where(w => filter.category!.Contains(w.CategoryId!.Value));
            }

            if (filter.province != null && filter.province!.Any())
            {
                string province = string.Empty;

                for (int i = 0; i < filter.province.Count; i++)
                {
                    if (i == 0)
                    {
                        province += filter.province[i].ToString();
                    }
                    else if (i == filter.province.Count && i != 0)
                    {
                        province += filter.province[i].ToString();
                    }
                    else
                    {
                        province += "-" + filter.province[i].ToString();
                    }
                }
                parmeters.Add(new StoredProcedureParmeter()
                {
                    ParmeterName = "@provinces",
                    ParmeterType = DbType.String,
                    ParmeterValue = province
                });
                query = query.Where(w => filter.province.Contains(w.ProvinceId!.Value));
            }

            if (filter.city != null && filter.city.Any())
            {
                string city = string.Empty;

                for (int i = 0; i < filter.city.Count; i++)
                {
                    if (i == 0)
                    {
                        city += filter.city[i].ToString();
                    }
                    else if (i == filter.city.Count && i != 0)
                    {
                        city += filter.city[i].ToString();
                    }
                    else
                    {
                        city += "-" + filter.city[i].ToString();
                    }
                }
                parmeters.Add(new StoredProcedureParmeter()
                {
                    ParmeterName = "@cities",
                    ParmeterType = DbType.String,
                    ParmeterValue = city
                });
                query = query.Where(w => filter.city.Contains(w.CityId!.Value));
            }

            if (!string.IsNullOrEmpty(filter.search))
            {
                parmeters.Add(new StoredProcedureParmeter()
                {
                    ParmeterName = "@search",
                    ParmeterType = DbType.String,
                    ParmeterValue = filter.search
                });
            }

            var result = await _dapper.ExecuteStoredProcedureAsync<BusinessViewModel>("GetBusinesses", parmeters);
            if (!result.IsSuccess)
            {
                return Result.Fail();
            }
            businesses.Businesses = result.Data as List<BusinessViewModel>;
            businesses.Filter = filter;
            int count= await query.CountAsync();
            businesses.Total = count.PageCount(18);




            return Result.Success(businesses);
        }






























        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PictureBusinessViewModel>> GetBusinessPicturesAsync
            (Guid Id, CancellationToken cancellation = default)
        {
            var query = await _pictureRepository.GetByQueryAsync();
            var model = await query.Where(w => w.BusinessId == Id).ToListAsync();
            List<PictureBusinessViewModel> pictures = model.Adapt<List<PictureBusinessViewModel>>();
            return pictures;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PropertyBusinessViewModel>>
            GetBusinessPropertiesAsync(Guid Id, CancellationToken cancellation = default)
        {
            var query = await _propertyRepository.GetByQueryAsync();
            var model = await query.Where(w => w.BusinessId == Id).ToListAsync();
            List<PropertyBusinessViewModel> properties = model.Adapt<List<PropertyBusinessViewModel>>();
            return properties;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<TimeBusinessViewModel>>
            GetBusinessTimesAsync(Guid Id, CancellationToken cancellation = default)
        {
            var query = await _timeRepository.GetByQueryAsync();
            var model = await query.Where(w => w.BusinessId == Id).ToListAsync();
            List<TimeBusinessViewModel> times = model.Adapt<List<TimeBusinessViewModel>>();
            return times;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<CategoryFilterViewModel>> GetCategoryFilterAsync(CancellationToken cancellation = default)
        {
            List<CategoryFilterViewModel> filters = new();
            var categories = await _categoryRepository.GetListAsync(cancellation);
            filters = categories.Adapt<List<CategoryFilterViewModel>>();
            return filters;
        }

        public async Task<List<ProvinceFilterViewModel>> GetProvinceFilterAsync(CancellationToken cancellation = default)
        {
            List<ProvinceFilterViewModel> filters = new();
            var query = await _provinceRepository.GetByQueryAsync();
            var provinces = await query.Include(i => i.cities).ToListAsync();
            filters = provinces.Adapt<List<ProvinceFilterViewModel>>();
            return filters;
        }
    }
}
