using Application.Common.Enum;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Core;
using Domain.Entities.Business;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cqrs.Business
{
    public class GetBusinesiesQuery : IRequest<PaginatedList<BusinessViewModel>>
    {
        public IPagination? pagination { set; get; }
    }
    public class GetBusinesiesQueryHandler(IEfRepository<BusinessEntity> repository) :
        IRequestHandler<GetBusinesiesQuery, PaginatedList<BusinessViewModel>>
    {
        private readonly IEfRepository<BusinessEntity> _repository = repository;
        public async Task<PaginatedList<BusinessViewModel>> Handle(GetBusinesiesQuery request, CancellationToken cancellationToken)
        {

            var query = await _repository.GetByQueryAsync();
            query = query.Include(i => i.User).Include(i => i.Category);

            #region Config
            var config = new TypeAdapterConfig();
            config.NewConfig<BusinessEntity, BusinessViewModel>()
                .Map(a => a.AccountName, b => b.AccountName)
                .Map(a => a.Code, b => b.Code)
                .Map(a => a.Id, b => b.Id)
                .Map(a => a.status, b => b.status)
                .Map(a => a.IsTime, b => b.IsTime)
                .Map(a => a.CategoryName, b => b.Category!.Name)
                .Map(a => a.Publisher, b => b.User!.FirstName + " " + b.User.LastName)
                .Compile();
            #endregion

            int count;
            PaginatedList<BusinessViewModel> model = new();
            if (string.IsNullOrEmpty(request.pagination!.keyword))
            {
                count = query.Count().PageCount(request.pagination!.pageSize);
                model = await query
               .MappingedAsync<BusinessEntity, BusinessViewModel>
                (request.pagination!.curentPage,
                request.pagination!.pageSize, count, config);
            }
            else
            {
                count = query.Count(c =>
                c.AccountName!.Contains(request.pagination!.keyword) &&
                c.Code!.Value.ToString().Contains(request.pagination!.keyword) &&
                c.PhoneNumber!.Contains(request.pagination!.keyword))
                 .PageCount(request.pagination!.pageSize);

                model = await query.Where(w =>
                w.AccountName!.Contains(request.pagination!.keyword) &&
                w.Code!.Value.ToString().Contains(request.pagination!.keyword) &&
                w.PhoneNumber!.Contains(request.pagination!.keyword)
            )
                .MappingedAsync<BusinessEntity, BusinessViewModel>(request.pagination!
                .curentPage, request.pagination!.pageSize, count, config);
            }
            return model;
        }
    }



    public class BusinessViewModel
    {
        public string? AccountName { set; get; }
        public long? Code { set; get; }
        public string? Publisher { set; get; }
        public Status status { set; get; }
        public bool IsTime { set; get; }
        public string? CategoryName { set; get; }
        public Guid Id { set; get; }
    }
}
