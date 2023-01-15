using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.Repository
{
	public interface ISettingRepository
	{
        Task<List<DomainModels.SettingDTO>> GetSettingsAsync(string host);
        Task<List<DomainModels.SettingKey>> GetSettingKeysAsync();
        Task<List<DomainModels.SettingType>> GetSettingTypesAsync();
        Task<DomainModels.SettingDTO> GetSettingAsync(string host, int settingId);
        Task<DomainModels.SettingKey> GetSettingKeyAsync(int settingKeyId);
        Task<DomainModels.SettingKey> GetSettingKeyByKeyAsync(string key);
        Task<DomainModels.SettingDTO> InsertSettingAsync(string host, int? domainId, int settingKeyId, string value, string createdBy);
        Task<DomainModels.SettingKey> InsertSettingKeyAsync(int settingTypeId, string key, bool isDefault, string defaultValue);
        Task<bool> UpdateSettingAsync(int id, int? domainId, int settingKeyId, string value, string updatedBy);
        Task<bool> DeleteSettingAsync(string host, int settingId);
    }
}

