using System;
namespace CraftsApi.Controllers.V1.Requests
{
	public class InsertSettingKeyRequest
	{
		public InsertSettingKeyRequest(
            int settingTypeId,
            string key,
            bool isDefault,
            string defaultValue)
		{
            SettingTypeId = settingTypeId;
            Key = key;
            IsDefault = isDefault;
            DefaultValue = defaultValue;
        }

        public int SettingTypeId { get; }
        public string Key { get; }
        public bool IsDefault { get; }
        public string DefaultValue { get; }
    }
}

