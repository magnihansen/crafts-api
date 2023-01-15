using System;
namespace CraftsApi.Service.ViewModels
{
	public class SettingVM
	{
		public SettingVM(int id, int settingKeyId, string key, string value, DateTime createdDate,
            string createdBy, DateTime updatedDate, string updatedBy)
		{
            Id = id;
            SettingKeyId = settingKeyId;
            Key = key;
            Value = value;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            UpdatedDate = updatedDate;
            UpdatedBy = updatedBy;
        }

        public int Id { get; }
        public int SettingKeyId { get; }
        public string Key { get; }
        public string Value { get; }
        public DateTime CreatedDate { get; }
        public string CreatedBy { get; }
        public DateTime UpdatedDate { get; }
        public string UpdatedBy { get; }
    }
}

