using System;
namespace CraftsApi.DomainModels
{
    public class PageType : BaseDateColumns
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }      
    }
}
