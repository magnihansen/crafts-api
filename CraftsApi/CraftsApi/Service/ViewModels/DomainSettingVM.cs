using System;
namespace CraftsApi.Service.ViewModels
{
	public class DomainSettingVM
	{
		public DomainSettingVM(string key, string value)
		{
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }
    }
}

