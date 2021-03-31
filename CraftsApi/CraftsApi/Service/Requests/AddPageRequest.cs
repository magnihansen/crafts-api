using System;
namespace CraftsApi.Service.Requests
{
    public class AddPageRequest
    {
        public AddPageRequest(string uid, string title, string parent, string content, string pageRank,
            string link, bool active, string createdBy)
        {
            if (string.IsNullOrWhiteSpace(uid))
            {
                throw new ArgumentException($"'{nameof(uid)}' cannot be null or whitespace", nameof(uid));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(pageRank))
            {
                throw new ArgumentException($"'{nameof(pageRank)}' cannot be null or whitespace", nameof(pageRank));
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
            Title = title;
            Parent = parent;
            Content = content;
            PageRank = pageRank;
            Link = link;
            Active = active;
            CreatedBy = createdBy;
        }

        public string Uid { get; }

        public string Title { get; }

        public string Parent { get; }

        public string Content { get; }

        public string PageRank { get; }

        public string Link { get; }

        public bool Active { get; }

        public string CreatedBy { get; }
    }
}
