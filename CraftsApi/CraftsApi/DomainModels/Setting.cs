using System;
namespace CraftsApi.DomainModels
{
	public class Setting : BaseDateColumns
    {
		public int Id { get; set; }
		public int? DomainId { get; set; }
		public int SettingKeyId { get; set; }
		public string Value { get; set; }
	}
}

