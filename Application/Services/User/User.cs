using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Core;
using Application.Cqrs.Identity.User;
using Domain.Entities.System.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Application.Services.User
{
    public class User(UserManager<UserEntity> user, IContext context, ILogger<User> logger) : IUser
    {
        private readonly UserManager<UserEntity> _user = user;
        private readonly IContext _context = context;
        private readonly ILogger<User> _logger = logger;

        public delegate Task<Result> ChangePasswordEvent(UserEntity entity, string password, CancellationToken cancellation);
        public delegate Task<Result> ChangeRolesEvent(UserEntity entity, List<string> roles, CancellationToken cancellation);
        public async Task<Result> ChangeUserPasswordAsync(UserEntity user, string password, CancellationToken cancellation)
        {
            StringBuilder textError = new StringBuilder();
            var resultRemoveOldUserPassword = await _user.RemovePasswordAsync(user);
            if (!resultRemoveOldUserPassword.Succeeded)
            {
                foreach (var error in resultRemoveOldUserPassword.Errors)
                {
                    textError.AppendLine(error.Description);
                }

                return Result.Fail(textError.ToString());
            }


            var resultInserUserNewPassword = await _user.AddPasswordAsync(user, password);
            if (!resultInserUserNewPassword.Succeeded)
            {
                foreach (var error in resultInserUserNewPassword.Errors)
                {
                    textError.AppendLine(error.Description);
                }

                return Result.Fail(textError.ToString());
            }
            return Result.Success();
        }

        public async Task<Result> ChangeUserRoleAsync(UserEntity user, List<string> roles, CancellationToken cancellationToken)
        {
            StringBuilder textError = new StringBuilder();
            var oldUserRoles = await _user.GetRolesAsync(user);

            if (!oldUserRoles.Any())
            {

                return Result.Fail(FailMessage.Internal);
            }

            var resultRemoveUserRoles = await _user.RemoveFromRolesAsync(user, oldUserRoles);
            if (!resultRemoveUserRoles.Succeeded)
            {
                foreach (var error in resultRemoveUserRoles.Errors)
                {
                    textError.AppendLine(error.Description);
                }

                return Result.Fail(textError.ToString());
            }

            var resultInsertUserRoles = await _user.AddToRolesAsync(user, roles!);

            if (!resultInsertUserRoles.Succeeded)
            {
                foreach (var error in resultInsertUserRoles.Errors)
                {
                    textError.AppendLine(error.Description);
                }

                return Result.Fail(textError.ToString());
            }
            return Result.Success();
        }

        public async Task<Result> GetUserByIdAsync(GetUserQuery query, CancellationToken cancellationToken)
        {
            var model = await _user.Users.AsNoTracking().SingleOrDefaultAsync(s => s.Id == query.Id, cancellationToken);
            if (model == null)
            {
                _logger.LogError("کاربر پیدا نشد.");
                return Result.Fail();
            }
            UpdateUserCommand user = model.Adapt<UpdateUserCommand>();
            var roles = await _user.GetRolesAsync(model);
            if (roles != null)
            {
                user.Roles = roles.Adapt<List<string>>();
            }

            return Result.Success(user);
        }

        public async Task<PaginatedList<TModel>> GetUsersAsync<TModel>(IPagination pageOption, CancellationToken cancellation)
        {
            var query = _user.Users.AsQueryable();
            int count;
            PaginatedList<TModel> model = new();
            if (string.IsNullOrEmpty(pageOption.keyword))
            {
                count = query.Count().PageCount(pageOption.pageSize);
                model = await query.AsNoTracking().MappingedAsync<UserEntity, TModel>(pageOption.curentPage, pageOption.pageSize, count);

            }
            else
            {
                count = query.Count(c =>
                    c.UserName!.Contains(pageOption.keyword)
                || c.LastName!.Contains(pageOption.keyword)
                || c.PhoneNumber!.Contains(pageOption.keyword)
                ).PageCount(pageOption.pageSize);
                model = await query.AsNoTracking().Where(w =>
                    w.UserName!.Contains(pageOption.keyword) ||
                    w.LastName!.Contains(pageOption.keyword)
                || w.PhoneNumber!.Contains(pageOption.keyword)).MappingedAsync
                <UserEntity, TModel>(pageOption.curentPage, pageOption.pageSize, count);
            }
            return model;
        }

        public async Task<Result> InsertUserAsync(InsertUserCommand command, CancellationToken cancellationToken)
        {
            StringBuilder textError = new StringBuilder();
            UserEntity user = command.Adapt<UserEntity>();
            user.Avatar = command.AvatarFile!.UploadImage("User", "defaultAvatar.jpg");
            using (var transaction = await _context.BeginTransactionAsync())
            {

                var resultInsertUser = await _user.CreateAsync(user);
                if (!resultInsertUser.Succeeded)
                {
                    foreach (var error in resultInsertUser.Errors)
                    {
                        textError.AppendLine(error.Description);
                    }
                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای ایجاد کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(textError.ToString());
                }
                var resultInserUserRole = await _user.AddToRolesAsync(user, command.Roles!);
                if (!resultInserUserRole.Succeeded)
                {
                    foreach (var error in resultInserUserRole.Errors)
                    {
                        textError.AppendLine(error.Description);
                    }
                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای ایجاد نقش کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(textError.ToString());
                }
                var resultInserUserPassword = await _user.AddPasswordAsync(user, command.Password!);
                if (!resultInserUserPassword.Succeeded)
                {
                    foreach (var error in resultInserUserPassword.Errors)
                    {
                        textError.AppendLine(error.Description);
                    }
                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای ایجاد رمز عبور کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(textError.ToString());
                }
                await transaction.CommitAsync();
                return Result.Success();
            }
        }

        public async Task<Result> UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken)
        {

            StringBuilder textError = new StringBuilder();
            var user = await _user.Users.FirstOrDefaultAsync(s => s.Id == command.Id, cancellationToken);
            if (user == null)
            {
                _logger.LogError("کاربر پیدا نشد.");
                return Result.Fail(FailMessage.Internal);
            }

            var config = new TypeAdapterConfig();
            config.NewConfig<UpdateUserCommand, UserEntity>()
                .Map(a => a.Email, b => b.Email)
                .Map(a => a.EmailConfirmed, b => b.EmailConfirmed)
                .Map(a => a.LastName, b => b.LastName)
                .Map(a => a.FirstName, b => b.FirstName)
                .Map(a => a.UserName, b => b.UserName)
                .Map(a => a.PhoneNumber, b => b.PhoneNumber)
                .Map(a => a.PhoneNumberConfirmed, b => b.PhoneNumberConfirmed)
                .Compile();


            command.Adapt(user, config);



            if (command.AvatarFile != null)
            {
                user.Avatar.RemoveImage("User", "defaultAvatar.jpg");
                user.Avatar = command.AvatarFile.UploadImage("User", "defaultAvatar.jpg");
            }

            using (var transaction = await _context.BeginTransactionAsync())
            {


                var resultUpdateUser = await _user.UpdateAsync(user);
                if (!resultUpdateUser.Succeeded)
                {
                    foreach (var error in resultUpdateUser.Errors)
                    {
                        textError.AppendLine(error.Description);
                    }
                    _logger.LogError($"خطای بروزرسانی کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(textError.ToString());
                }



                ChangeRolesEvent changeRoles = ChangeUserRoleAsync;
                var resultChangeRoles = await changeRoles(user, command.Roles!, cancellationToken);
                if (!resultChangeRoles.IsSuccess)
                {
                    textError.AppendLine(resultChangeRoles.Message!.First());
                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای تغییر نقش کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(textError.ToString());
                }


                if (!string.IsNullOrEmpty(command.Password))
                {
                    ChangePasswordEvent changePassword = ChangeUserPasswordAsync;
                    var resultChangePassword = await changePassword(user, command.Password!, cancellationToken);
                    if (!resultChangePassword.IsSuccess)
                    {
                        textError.AppendLine(resultChangePassword.Message!.First());
                        await transaction.RollbackAsync();
                        _logger.LogError($"خطای تغییر رمز عبور کاربر رخ داده است - {textError.ToString()}");
                        return Result.Fail(textError.ToString());
                    }
                }


                await transaction.CommitAsync();
                return Result.Success();
            }
        }

        public async Task<Result> DeleteUserAsync(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            StringBuilder textError = new StringBuilder();
            var user = await _user.Users.SingleOrDefaultAsync(s => s.Id == command.Id, cancellationToken);
            if (user == null)
            {
                return Result.Fail(FailMessage.Internal);
            }
            using (var transaction = await _context.BeginTransactionAsync())
            {
                var resultRemovePassword = await _user.RemovePasswordAsync(user);
                if (!resultRemovePassword.Succeeded)
                {
                    foreach (var item in resultRemovePassword.Errors)
                    {
                        textError.AppendLine(item.Description);
                    }
                    _logger.LogError($"خطای حذف رمز عبور کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(textError.ToString());
                }


                var resultGetUserRoles = await _user.GetRolesAsync(user);
                if (!resultGetUserRoles.Any())
                {
                    await transaction.RollbackAsync();

                    _logger.LogError($"خطای عدم وجود نقش کاربر رخ داده است.");
                    return Result.Fail(FailMessage.Internal);
                }


                var resultRemoveRoles = await _user.RemoveFromRolesAsync(user, resultGetUserRoles);
                if (!resultRemoveRoles.Succeeded)
                {
                    foreach (var item in resultRemoveRoles.Errors)
                    {
                        textError.AppendLine(item.Description);
                    }
                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای حذف نقش کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(textError.ToString());
                }

                var resultRemmoveAvatar = user.Avatar.RemoveImage("User");
                if (!resultRemmoveAvatar.IsSuccess)
                {

                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای حذف تصویر کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(resultRemmoveAvatar.Message!.First());
                }
                var resultRemoveUser = await _user.DeleteAsync(user);
                if (!resultRemoveUser.Succeeded)
                {
                    foreach (var item in resultRemoveUser.Errors)
                    {
                        textError.AppendLine(item.Description);
                    }
                    await transaction.RollbackAsync();
                    _logger.LogError($"خطای حذف کاربر رخ داده است - {textError.ToString()}");
                    return Result.Fail(textError.ToString());
                }
                await transaction.CommitAsync();
                return Result.Success();
            }
        }
    }
}