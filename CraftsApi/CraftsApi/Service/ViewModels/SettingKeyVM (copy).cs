using System;
namespace CraftsApi.Service.ViewModels
{
	public class SettingKeyVM
	{
		public SettingKeyVM(int id, int settingTypeId, string key, bool isDefault, string defaultValue)
		{
            Id = id;
            SettingTypeId = settingTypeId;
            Key = key;
            IsDefault = isDefault;
            DefaultValue = defaultValue;
        }

        public int Id { get; }
        public int SettingTypeId { get; }
        public string Key { get; }
        public bool IsDefault { get; }
        public string DefaultValue { get; }
    }
}

