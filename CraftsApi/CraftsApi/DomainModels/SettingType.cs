using System;
namespace CraftsApi.DomainModels
{
	public class SettingType : BaseDateColumns
    {
		public int Id { get; set; }
		public int DomainId { get; set; }
		public string Name { get; set; }
	}
}

