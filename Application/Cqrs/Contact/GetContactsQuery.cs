using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Common.ViewModel.Home;
using Application.Core;
using Domain.Entities.Employment;
using Domain.Entities.System;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Cqrs.Contact
{
    public class GetContactsQuery
     : IRequest<PaginatedList<ContactViewModel>>
    {
        public IPagination pagination { set; get; }
    }


    public class GetContactsQueryHandler
        : IRequestHandler<GetContactsQuery, PaginatedList<ContactViewModel>>
    {
        private readonly IEfRepository
            <ContactEntity> _repository;
        public GetContactsQueryHandler(
            IEfRepository
            <ContactEntity> repository
            )
        {
            _repository = repository;
        }
        public async Task<PaginatedList<ContactViewModel>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetByQueryAsync();

            int count;
            PaginatedList<ContactViewModel> model = new();
            if (string.IsNullOrEmpty(request.pagination!.keyword))
            {
                count = query.Count().PageCount(request.pagination!.pageSize);
                model = await query
               .MappingedAsync<ContactEntity, ContactViewModel>
                (request.pagination!.curentPage,
                request.pagination!.pageSize, count);
            }
            else
            {
                count = query.Count(c =>
                c.FullName!.Contains(request.pagination!.keyword) &&
                c.Subject!.ToString().Contains(request.pagination!.keyword))
                 .PageCount(request.pagination!.pageSize);

                model = await query.Where(w =>
                w.FullName!.Contains(request.pagination!.keyword) &&
                w.Subject!.ToString().Contains(request.pagination!.keyword)
            )
                .MappingedAsync<ContactEntity, ContactViewModel>(request.pagination!
                .curentPage, request.pagination!.pageSize, count);
            }
            return model;
        }
    }

    public class ContactViewModel
    {
        public Guid Id { get; set; }
        public required string FullName { set; get; }
        public string? PhoneNumber { set; get; }
        public string? Message { set; get; }
        public string? Subject { set; get; }

    }


}
