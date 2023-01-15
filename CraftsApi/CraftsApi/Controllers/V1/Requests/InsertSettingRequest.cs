using System;
namespace CraftsApi.Controllers.V1.Requests
{
	public class InsertSettingRequest
	{
		public InsertSettingRequest(int settingKeyId, string value, string createdBy)
		{
            SettingKeyId = settingKeyId;
            Value = value;
            CreatedBy = createdBy;
        }

		public int SettingKeyId { get; }
		public string Value { get; }
		public string CreatedBy { get; }
    }
}

