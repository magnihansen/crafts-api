using System;
namespace CraftsApi.DomainModels
{
	public class SettingKey
	{
		public int Id { get; set; }
		public int SettingTypeId { get; set; }
		public string Key { get; set; }
		public bool IsDefault { get; set; }
		public string DefaultValue { get; set; }
	}
}

