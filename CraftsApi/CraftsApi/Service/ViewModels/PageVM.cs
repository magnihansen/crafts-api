using System;
namespace CraftsApi.Service.ViewModels
{
    public class PageVM
    {
        public PageVM(int id, int? parentId, int pageTypeId, string title, string content,
            int sort, string link, bool isRouterLink, bool active, DateTime createdDate, string createdBy,
            DateTime? updatedDate, string updatedBy)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(link))
            {
                throw new ArgumentException($"'{nameof(link)}' cannot be null or whitespace.", nameof(link));
            }

            if (string.IsNullOrWhiteSpace(createdBy))
            {
                throw new ArgumentException($"'{nameof(createdBy)}' cannot be null or whitespace", nameof(createdBy));
            }

            Id = id;
            ParentId = parentId;
            PageTypeId = pageTypeId;
            Title = title;
            Content = content;
            Sort = sort;
            Link = link;
            IsRouterLink = isRouterLink;
            Active = active;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            UpdatedDate = updatedDate;
            UpdatedBy = updatedBy ?? "";
        }

        public int Id { get; }
        public string Title { get; }
        public int? ParentId { get; }
        public int PageTypeId { get; }
        public string Content { get; }
        public int Sort { get; }
        public string Link { get; }
        public bool IsRouterLink { get; }
        public bool Active { get; }
        public DateTime CreatedDate { get; }
        public string CreatedBy { get; }
        public DateTime? UpdatedDate { get; }
        public string UpdatedBy { get; }
    }
}
