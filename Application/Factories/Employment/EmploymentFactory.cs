using Application.Common;
using Application.Common.Const;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Record.Employment;
using Application.Common.ViewModel.Business;
using Application.Common.ViewModel.Employment;
using Application.Common.ViewModel.Home;
using Application.Core;
using Domain.Entities.Business;
using Domain.Entities.Employment;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Application.Factories.Employment
{
    public class EmploymentFactory : IEmploymentFactory
    {
        private readonly IEfRepository<BusinessEntity> _businessRepository;
        private readonly IEfRepository<EmploymentEntity> _employmentRepository;
        private readonly ILogger<EmploymentFactory> _logger;
        private readonly IDapper _dapper;
        public EmploymentFactory
            (IEfRepository<EmploymentEntity> employmentRepository,
            ILogger<EmploymentFactory> logger, IDapper dapper, IEfRepository<BusinessEntity> businessRepository)
        {
            _businessRepository = businessRepository;
            _employmentRepository
                = employmentRepository;
            _logger = logger;
            _dapper
                = dapper;
        }

        public async Task<List<BusinessFilterViewModel>> GetBusinessFilterAsync(CancellationToken cancellation = default)
        {
            List<BusinessFilterViewModel> filters = new();
            var query = await _businessRepository.GetByQueryAsync();
            var businesses = await query.Where(w=>w.IsActive==true && w.status==Domain.Enum.StatusEnum.Accepted).ToListAsync();
            filters = businesses.Adapt<List<BusinessFilterViewModel>>();
            return filters;
        }

        public async Task<EmploymentDetailViewModel> GetEmploymentDetailAsync
            (Guid Id, CancellationToken cancellationToken = default)
        {
            var query = await _employmentRepository.GetByQueryAsync();
            var model = await query.Include(i => i.Business)
                .Include(i=>i.City)
                .Include(i=>i.Province)
                .Include(i => i.User).SingleOrDefaultAsync(s => s.Id == Id, cancellationToken);
            EmploymentDetailViewModel employment = new();
            employment = model.Adapt<EmploymentDetailViewModel>();
            employment.CreateTime = model!.CreateTime.PersianTime();
            return employment;
        }

        public async Task<Result> GetEmploymentsAsync(EmploymentFilter filter, CancellationToken cancellation = default)
        {
            var query = await _employmentRepository.GetByQueryAsync();
            query = query.Include(i => i.Business)
                .Include(i => i.Province).ThenInclude(t => t!.cities);
            EmploymentsViewModel employments = new();
            List<StoredProcedureParmeter> parmeters = new();
            parmeters.Add(new StoredProcedureParmeter()
            {
                ParmeterName = "@page",
                ParmeterType = DbType.Int32,
                ParmeterValue = filter.page
            });

            if (filter.business != null && filter.business.Any())
            {
                string business = string.Empty;

                for (int i = 0; i < filter.business.Count; i++)
                {
                    if (i == 0)
                    {
                        business += filter.business[i].ToString();
                    }
                    else if (i == filter.business.Count && i != 0)
                    {
                        business += filter.business[i].ToString();
                    }
                    else
                    {
                        business += "-" + filter.business[i].ToString();
                    }
                }
                parmeters.Add(new StoredProcedureParmeter()
                {
                    ParmeterName = "@businesses",
                    ParmeterType = DbType.String,
                    ParmeterValue = business
                });
                query = query.Where(w => filter.business!.Contains(w.BusinessId!.Value));
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

            var result = await _dapper.ExecuteStoredProcedureAsync<EmploymentViewModel>("GetEmployments", parmeters);
            if (!result.IsSuccess)
            {
                return Result.Fail();
            }
            employments.Employments = result.Data as List<EmploymentViewModel>;
            employments.Filter = filter;
            int count = await query.CountAsync();
            employments.Total = count.PageCount(18);




            return Result.Success(employments);
        }
    }
}
