using Application.Common;
using Application.Common.Extension;
using Application.Common.Interfaces;
using Domain.Entities.System;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Application.Cqrs.Setting
{
    public class UpdateSettingCommand : IRequest<Result>
    {

        public string? TitleSite { set; get; }
        public IFormFile LogoFile { set; get; }
        public string? Logo { set; get; }
        public string? Number { set; get; }
        public string? Email { set; get; }
        public string? Instagram { set; get; }
        public string? WhatsApp { set; get; }
        public string? Linkdin { set; get; }
        public string? Telegram { set; get; }
        public string? ApiSms { set; get; }
        public string? ApiNumber { set; get; }
        public string? Footer { set; get; }
        public string? Header { set; get; }
        public string? MainText { set; get; }
        public string? MainSubText { set; get; }
        public string? AboutSectionTitle { set; get; }
        public string? AboutSectionDescription { set; get; }
    }

    public class UpdateSettingCommandHandler
        (IEfRepository<SettingEntity> repository, IDistributedCache cache,ILogger<UpdateSettingCommandHandler> logger) :
        IRequestHandler<UpdateSettingCommand, Result>
    {
        private readonly ILogger<UpdateSettingCommandHandler> _logger=logger;
        private readonly IEfRepository<SettingEntity> _repository = repository;
        private readonly IDistributedCache _cache = cache;
        public async Task<Result> Handle(UpdateSettingCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var setting = await _repository.FirstOrDefaultAsync(cancellationToken);
                request.Adapt(setting);
                if (request.LogoFile != null)
                {
                    setting.Logo = request.LogoFile.UploadImage("Setting");
                    request.Logo = setting.Logo;
                }
                await _repository.UpdateAsync(setting);
                var cacheValue = await _cache.GetAsync(CacheKey.SettinKey);
                var serializedSetting = JsonSerializer.Serialize(request);
                byte[] settingEncoded = Encoding.UTF8.GetBytes(serializedSetting);
                if (cacheValue == null)
                {
                    await _cache.SetAsync(CacheKey.SettinKey, settingEncoded,
                      new DistributedCacheEntryOptions()
                    
                      .SetSlidingExpiration(TimeSpan.FromDays(36500)));
                }
                else
                {
                    await _cache.RemoveAsync(CacheKey.SettinKey, cancellationToken);
                   
                    await _cache.SetAsync(CacheKey.SettinKey, settingEncoded,
                        new DistributedCacheEntryOptions()

                        .SetAbsoluteExpiration(TimeSpan.FromDays(36500)));
                }
                return Result.Success(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"خطای پیکربندی تنظیمات رخ داده است - {ex.Message}");
            }
            return Result.Fail(FailMessage.Internal);
        }
    }
}
