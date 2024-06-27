
using Application.Common.Model;
using Application.Common.ViewModel;
using Application.Common;
using Microsoft.AspNetCore.Identity;
using Application.Cqrs.Identity.Role;
using Microsoft.EntityFrameworkCore;
using Application.Common.Extension;
using Application.Core;
using Mapster;
using Domain.Entities.System.Identity;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services.Role
{
    public class Role(RoleManager<RoleEntity> role,ILogger<Role> logger) : IRole
    {
        private readonly ILogger<Role> _logger = logger;
        private readonly RoleManager<RoleEntity> _role = role;


        public async Task<Result> DeleteRoleAsync(DeleteRoleCommand command, CancellationToken cancellation)
        {
            var model = await _role.Roles.SingleOrDefaultAsync(s => s.Id == command.Id, cancellation);
            if (model == null)
            {
                _logger.LogError("نقش کاربری برای حذف پیدا نشد.");
                return Result.Fail();
            }
            var resultDeleteRole = await _role.DeleteAsync(model);
            if (resultDeleteRole.Succeeded)
            {
                return Result.Success(true);
            }
            else
            {
                string error = string.Empty;
                foreach (var item in resultDeleteRole.Errors)
                {
                    error += item.Description;
                }
                _logger.LogError($"خطای حذف نقش کاربری رخ داده است - {error}");
                return Result.Fail(error);
            }
        }

        public async Task<Result> GetRoleByIdAsync(GetRoleQuery query, CancellationToken cancellation)
        {
            var model = await _role.Roles.SingleOrDefaultAsync(s => s.Id == query.Id, cancellation);
            if (model == null)
            {
                _logger.LogError("نقش کاربری پیدا نشد.");
                return Result.Fail();
            }
            UpdateRoleCommand role = model.Adapt<UpdateRoleCommand>();
            return Result.Success(role);
        }

        public async Task<List<ItemViewModel<string, string>>> GetRoleItemsAsync(CancellationToken cancellation)
        {
            var query = _role.Roles.AsQueryable();
            return await query.Select(s => new ItemViewModel<string, string>
            {
                Id = s.Name!,
                Title = s.PresianName
            }).ToListAsync();
        }

        public async Task<PaginatedList<TModel>> GetRolesAsync<TModel>(IPagination pageOption, CancellationToken cancellation)
        {
            var query = _role.Roles.AsQueryable();
            int count;
            PaginatedList<TModel> model = new();
            if (string.IsNullOrEmpty(pageOption.keyword))
            {
                count = query.Count().PageCount(pageOption.pageSize);
                model = await query
                    .MappingedAsync<RoleEntity, TModel>(pageOption.curentPage, pageOption.pageSize, count);
            }
            else
            {
                count = query.Count(c => c.PresianName.Contains(pageOption.keyword)).PageCount(pageOption.pageSize);
                model = await query.Where(w => w.PresianName.Contains(pageOption.keyword))
                    .MappingedAsync<RoleEntity, TModel>(pageOption.curentPage, pageOption.pageSize, count);
            }
            return model;
        }

        public async Task<Result> InsertRoleAsync(InsertRoleCommand command, CancellationToken cancellation)
        {
            RoleEntity role = command.Adapt<RoleEntity>();





            var resultInsertRole = await _role.CreateAsync(role);
            if (resultInsertRole.Succeeded)
            {
                return Result.Success(true);
            }
            else
            {
                string error = string.Empty;
                foreach (var item in resultInsertRole.Errors)
                {
                    error += item.Description;
                }
                _logger.LogError($"خطای ایجاد نقش کاربری رخ داده است - {error}");
                return Result.Fail(error);
            }
        }

        public async Task<Result> UpdateRoleAsync(UpdateRoleCommand command, CancellationToken cancellation)
        {
            var model = await _role.Roles.AsTracking().SingleOrDefaultAsync(s => s.Id == command.Id, cancellation);
            if (model == null)
            {
                _logger.LogError("نقش کاربری پیدا نشد.");
                return Result.Fail(FailMessage.Internal);
            }
            model.Name = command.Name;
            model.PresianName = command.PresianName;
            var resultUpdateRole = await _role.UpdateAsync(model);
            if (resultUpdateRole.Succeeded)
            {
                return Result.Success();
            }
            string error = string.Empty;
            foreach (var item in resultUpdateRole.Errors)
            {
                error += item.Description + "\n";
            }
            _logger.LogError($"خطای بروزرسانی نقش کاربری رخ داده است - {error}");
            return Result.Fail(error);







        }
    }
}