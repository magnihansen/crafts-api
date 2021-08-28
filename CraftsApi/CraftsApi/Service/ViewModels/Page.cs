using System;
namespace CraftsApi.Service.ViewModels
{
    public class Page
    {
        public Page(int id, string uid, string title, string parent, string content, string pageRank, string link,
            bool active, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy)
        {
            if (string.IsNullOrWhiteSpace(uid))
            {
                throw new ArgumentException($"'{nameof(uid)}' cannot be null or whitespace", nameof(uid));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException($"'{nameof(content)}' cannot be null or whitespace", nameof(content));
            }

            if (string.IsNullOrWhiteSpace(pageRank))
            {
                throw new ArgumentException($"'{nameof(pageRank)}' cannot be null or whitespace", nameof(pageRank));
            }

            if (string.IsNullOrWhiteSpace(createdBy))
            {
                throw new ArgumentException($"'{nameof(createdBy)}' cannot be null or whitespace", nameof(createdBy));
            }

            Id = id;
            Uid = uid;
            Title = title;
            Parent = parent;
            Content = content;
            PageRank = pageRank;
            Link = link ?? throw new ArgumentNullException(nameof(link));
            Active = active;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            UpdatedDate = updatedDate;
            UpdatedBy = updatedBy;
        }

        public int Id { get; }

        public string Uid { get; }

        public string Title { get; }

        public string Parent { get; }

        public string Content { get; }

        public string PageRank { get; }

        public string Link { get; }

        public bool Active { get; }

        public DateTime CreatedDate { get; }

        public string CreatedBy { get; }

        public DateTime UpdatedDate { get; }

        public string UpdatedBy { get; }
    }
}
