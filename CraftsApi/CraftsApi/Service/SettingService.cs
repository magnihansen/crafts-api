using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Repository;
using CraftsApi.Service.Mappings;
using CraftsApi.Controllers.V1.Requests;
using Microsoft.Extensions.Hosting;

namespace CraftsApi.Service
{
	public class SettingService : ISettingService
	{
        private readonly ISettingRepository _settingRepository;
        private readonly IDomainRepository _domainRepository;

        public SettingService(ISettingRepository settingRepository, IDomainRepository domainRepository)
        {
            _settingRepository = settingRepository;
            _domainRepository = domainRepository;
        }

        public async Task<List<ViewModels.DomainSettingVM>> GetDomainSettingsAsync(string host)
        {
            List<DomainModels.SettingDTO> settings = await _settingRepository.GetSettingsAsync(host);
            return settings.MapSettingDTOToDomainSettingVM();
        }

        public async Task<List<ViewModels.SettingVM>> GetSettingsAsync(string host)
        {
            List<DomainModels.SettingDTO> settings = await _settingRepository.GetSettingsAsync(host);
            return settings.MapSettingDTOToSettingVM();
        }

        public async Task<List<ViewModels.SettingKeyVM>> GetSettingKeysAsync()
        {
            List<DomainModels.SettingKey> settingKeys = await _settingRepository.GetSettingKeysAsync();
            return settingKeys.MapSettingKeyToSettingKeyVM();
        }

        public async Task<List<ViewModels.SettingTypeVM>> GetSettingTypesAsync()
        {
            List<DomainModels.SettingType> settingTypes = await _settingRepository.GetSettingTypesAsync();
            return settingTypes.MapSettingTypeToSettingTypeVM();
        }

        public async Task<ViewModels.SettingVM> GetSettingAsync(string host, int settingId)
        {
            DomainModels.SettingDTO setting = await _settingRepository.GetSettingAsync(host, settingId);
            return setting.MapSettingDTOToSettingVM();
        }

        public async Task<ViewModels.SettingVM> InsertSettingAsync(string host, DomainModels.Setting setting)
        {
            DomainModels.Domain domain = await _domainRepository.GetDomainAsync(host);
            DomainModels.SettingDTO insertedSetting = await _settingRepository.InsertSettingAsync(
                host: host,
                domainId: domain.Id,
                settingKeyId: setting.SettingKeyId,
                value: setting.Value,
                createdBy: setting.CreatedBy
            );
            return insertedSetting.MapSettingDTOToSettingVM();
        }

        public async Task<ViewModels.SettingKeyVM> InsertSettingKeyAsync(DomainModels.SettingKey settingKey)
        {
            DomainModels.SettingKey insertedSettingKey = await _settingRepository.InsertSettingKeyAsync(
                settingTypeId: settingKey.SettingTypeId,
                key: settingKey.Key,
                isDefault: settingKey.IsDefault,
                defaultValue: settingKey.DefaultValue
            );
            return insertedSettingKey.MapSettingKeyToSettingKeyVM();
        }

        public async Task<bool> UpdateSettingAsync(string host, DomainModels.Setting setting)
        {
            DomainModels.Domain domain = await _domainRepository.GetDomainAsync(host);
            
            bool updated = await _settingRepository.UpdateSettingAsync(
                id: setting.Id,
                domainId: domain.Id,
                settingKeyId: setting.SettingKeyId,
                value: setting.Value,
                updatedBy: setting.UpdatedBy
            );
            return updated;
        }

        public async Task<bool> DeleteSettingAsync(string host, int settingId)
        {
            bool deleted = await _settingRepository.DeleteSettingAsync(host, settingId);
            return deleted;
        }
    }
}

