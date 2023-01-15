using System;
namespace CraftsApi.DomainModels
{
    public class Page : BaseDateColumns
    {
        public int Id { get; set; }
        public int DomainId { get; set; }
        public string Uid { get; set; }
        public int? ParentId { get; set; }
        public int PageTypeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Sort { get; set; }
        public string Link { get; set; }
        public bool IsRouterLink { get; set; }
        public bool Active { get; set; }      
    }
}
