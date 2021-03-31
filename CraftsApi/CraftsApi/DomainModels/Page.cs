using System;
namespace CraftsApi.DomainModels
{
    public class Page : BaseDateColumns
    {
        public int Id { get; set; }

        public string Uid { get; set; }

        public string Title { get; set; }

        public string ParentUid { get; set; }

        public string Content { get; set; }

        public string PageRank { get; set; }

        public string Link { get; set; }

        public bool Active { get; set; }      
    }
}
