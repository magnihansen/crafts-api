using System;
namespace CraftsApi.Service.ViewModels
{
	public class SettingTypeVM
	{
		public SettingTypeVM(int id, string name)
		{
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}

