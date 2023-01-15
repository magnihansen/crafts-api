using System;
namespace CraftsApi.DomainModels
{
	public class CdnToken : BaseDateColumns
	{
		public int Id { get; set; }
		public string Token { get; set; }
		public DateTime ExpireDate { get; set; }
	}
}

