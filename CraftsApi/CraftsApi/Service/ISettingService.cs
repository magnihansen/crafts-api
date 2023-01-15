using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Controllers.V1.Requests;

namespace CraftsApi.Service
{
	public interface ISettingService
	{
        Task<List<ViewModels.DomainSettingVM>> GetDomainSettingsAsync(string host);
        Task<List<ViewModels.SettingVM>> GetSettingsAsync(string host);
        Task<List<ViewModels.SettingKeyVM>> GetSettingKeysAsync();
        Task<List<ViewModels.SettingTypeVM>> GetSettingTypesAsync();
        Task<ViewModels.SettingVM> GetSettingAsync(string host, int settingId);
        Task<ViewModels.SettingVM> InsertSettingAsync(string host, DomainModels.Setting setting);
        Task<ViewModels.SettingKeyVM> InsertSettingKeyAsync(DomainModels.SettingKey settingKey);
        Task<bool> UpdateSettingAsync(string host, DomainModels.Setting setting);
        Task<bool> DeleteSettingAsync(string host, int settingId);
    }
}

