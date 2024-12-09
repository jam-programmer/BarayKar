using Application.Common.Enum;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Core;
using Application.Cqrs.Business;
using Domain.Entities.Business;
using Domain.Entities.Employment;
using Domain.Enum;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Employment
{
    public class GetEmploymentsQuery
        :IRequest<PaginatedList<EmploymentViewModel>>
    {
        public IPagination pagination { set; get; }
    }
    public class GetEmploymentsQueryHandler 
        (IEfRepository<EmploymentEntity> repository)
        : IRequestHandler<GetEmploymentsQuery, PaginatedList<EmploymentViewModel>>
    {
        private readonly IEfRepository<EmploymentEntity> _repository = repository;
        public async Task<PaginatedList<EmploymentViewModel>> Handle(GetEmploymentsQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();
            query = query.Include(i => i.User).Include(i => i.Business);

         

            int count;
            PaginatedList<EmploymentViewModel> model = new();
            if (string.IsNullOrEmpty(request.pagination!.keyword))
            {
                count = query.Count().PageCount(request.pagination!.pageSize);
                model = await query
               .MappingedAsync<EmploymentEntity, EmploymentViewModel>
                (request.pagination!.curentPage,
                request.pagination!.pageSize, count);
            }
            else
            {
                count = query.Count(c =>
                c.Title!.Contains(request.pagination!.keyword) &&
                c.Code!.ToString().Contains(request.pagination!.keyword))
                 .PageCount(request.pagination!.pageSize);

                model = await query.Where(w =>
                w.Title!.Contains(request.pagination!.keyword) &&
                w.Code!.ToString().Contains(request.pagination!.keyword) 
            )
                .MappingedAsync<EmploymentEntity, EmploymentViewModel>(request.pagination!
                .curentPage, request.pagination!.pageSize, count);
            }
            return model;
        }
    }
    public class EmploymentViewModel
    {
        public Guid Id { get; set; }    
        public string? BusinessAccountName { set; get; }
        public long Code { set; get; }
        public Status status { set; get; }
        public bool IsActive { set; get; } = false;
        public string? Title { set; get; }
    }
}
