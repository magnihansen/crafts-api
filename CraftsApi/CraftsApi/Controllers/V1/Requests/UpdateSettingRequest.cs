using System;
namespace CraftsApi.Controllers.V1.Requests
{
	public class UpdateSettingRequest
	{
		public UpdateSettingRequest(int id, int settingKeyId, string value, string updatedBy)
        {
            Id = id;
            SettingKeyId = settingKeyId;
            Value = value;
            UpdatedBy = updatedBy;
        }

        public int Id { get; }
        public int SettingKeyId { get; }
        public string Value { get; }
        public string UpdatedBy { get; }
    }
}

