using System;
namespace CraftsApi.Controllers.V1.Requests
{
    public class AddPageRequest
    {
        public AddPageRequest(string uid, int? parentId, int pageTypeId, string title, string content, int sort,
            string link, bool isRouterLink, bool active, string createdBy)
        {
            if (string.IsNullOrWhiteSpace(uid))
            {
                throw new ArgumentException($"'{nameof(uid)}' cannot be null or whitespace", nameof(uid));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(link))
            {
                throw new ArgumentException($"'{nameof(link)}' cannot be null or whitespace", nameof(link));
            }

            if (string.IsNullOrWhiteSpace(createdBy))
            {
                throw new ArgumentException($"'{nameof(createdBy)}' cannot be null or whitespace", nameof(createdBy));
            }

            Uid = uid;
            ParentId = parentId;
            PageTypeId = pageTypeId;
            Title = title;
            Content = content;
            Sort = sort;
            Link = link;
            IsRouterLink = isRouterLink;
            Active = active;
            CreatedBy = createdBy;
        }

        public string Uid { get; }
        public int? ParentId { get; }
        public int PageTypeId { get; }
        public string Title { get; }
        public string Content { get; }
        public int Sort { get; }
        public string Link { get; }
        public bool IsRouterLink { get; }
        public bool Active { get; }
        public string CreatedBy { get; }
    }
}
