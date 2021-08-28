using System.Collections.Generic;
using CraftsApi.Service.Requests;

namespace CraftsApi.Service.Mappings
{
    public static class Helper
    {
        /* Page mappings */

        public static List<ViewModels.Page> MapListOfDomainPagesToListOfViewPages(this List<DomainModels.Page> pages)
        {
            List<ViewModels.Page> mapped_pages = new List<ViewModels.Page>();

            foreach(DomainModels.Page domainPage in pages)
            {
                mapped_pages.Add(domainPage.MapDomainPageToViewPage());
            }

            return mapped_pages;
        }

        public static ViewModels.Page MapDomainPageToViewPage(this DomainModels.Page page)
        {
            return new ViewModels.Page(
                page.Id,
                page.Uid,
                page.Title,
                page.ParentUid,
                page.Content,
                page.PageRank,
                page.Link,
                page.Active,
                page.CreatedDate,
                page.CreatedBy,
                page.UpdatedDate,
                page.UpdatedBy
            );
        }

        public static DomainModels.Page MapAddPageRequestToPage(this AddPageRequest addPageRequest)
        {
            return new DomainModels.Page
            {
                Uid = addPageRequest.Uid,
                Title = addPageRequest.Title,
                ParentUid = addPageRequest.Parent,
                Content = addPageRequest.Content,
                PageRank = addPageRequest.PageRank,
                Link = addPageRequest.Link,
                Active = addPageRequest.Active,
                CreatedBy = addPageRequest.CreatedBy
            };
        }

        public static DomainModels.Page MapUpdatePageRequestToPage(this UpdatePageRequest updatePageRequest)
        {
            return new DomainModels.Page
            {
                Id = updatePageRequest.Id,
                Uid = updatePageRequest.Uid,
                Title = updatePageRequest.Title,
                ParentUid = updatePageRequest.Parent,
                Content = updatePageRequest.Content,
                PageRank = updatePageRequest.PageRank,
                Link = updatePageRequest.Link,
                Active = updatePageRequest.Active,
                UpdatedDate = updatePageRequest.UpdatedDate,
                UpdatedBy = updatePageRequest.UpdatedBy
            };
        }

        /* User mappings */

        public static List<ViewModels.User> MapListOfDomainUsersToListOfViewUsers(this List<DomainModels.User> users)
        {
            List<ViewModels.User> mapped_users = new List<ViewModels.User>();

            foreach (DomainModels.User domainUser in users)
            {
                mapped_users.Add(domainUser.MapDomainUserToViewUser());
            }

            return mapped_users;
        }

        public static ViewModels.User MapDomainUserToViewUser(this DomainModels.User user)
        {
            if (user.Id == 0)
            {
                return null;
            }
            return new ViewModels.User(
                user.Id,
                user.Username,
                user.Firstname,
                user.Lastname,
                user.Address,
                user.Zip,
                user.City,
                user.Country,
                user.Email,
                user.Phone,
                user.Active,
                user.CreatedDate,
                user.CreatedBy,
                user.UpdatedDate,
                user.UpdatedBy
            );
        }

        public static DomainModels.User MapAddUserRequestToUser(this AddUserRequest addUserRequest)
        {
            return new DomainModels.User
            {
                Username = addUserRequest.Username,
                Password = addUserRequest.Password,
                Firstname = addUserRequest.Firstname,
                Lastname = addUserRequest.Lastname,
                Address = addUserRequest.Address,
                Zip = addUserRequest.Zip,
                City = addUserRequest.City,
                Country = addUserRequest.Country,
                Email = addUserRequest.Email,
                Phone = addUserRequest.Phone,
                Active = addUserRequest.Active,
                CreatedBy = addUserRequest.CreatedBy
            };
        }

        public static DomainModels.User MapUpdateUserRequestToUser(this UpdateUserRequest updateUserRequest)
        {
            return new DomainModels.User
            {
                Username = updateUserRequest.Username,
                Password = updateUserRequest.Password,
                Firstname = updateUserRequest.Firstname,
                Lastname = updateUserRequest.Lastname,
                Address = updateUserRequest.Address,
                Zip = updateUserRequest.Zip,
                City = updateUserRequest.City,
                Country = updateUserRequest.Country,
                Email = updateUserRequest.Email,
                Phone = updateUserRequest.Phone,
                Active = updateUserRequest.Active,
                UpdatedDate = updateUserRequest.UpdatedDate,
                UpdatedBy = updateUserRequest.UpdatedBy
            };
        }

        /* Data mappings */
        public static ViewModels.Contact MapDomainContactToViewContact(this DomainModels.Contact contact)
        {
            return new ViewModels.Contact(
                contact.Id,
                contact.Name,
                contact.Email,
                contact.Phone,
                contact.Message,
                contact.CreatedDate,
                contact.CreatedBy
            );
        }

        public static List<ViewModels.Contact> MapListOfDomainContactsToListOfViewContacts(this List<DomainModels.Contact> contacts)
        {
            List<ViewModels.Contact> mapped_contacts = new List<ViewModels.Contact>();

            foreach (DomainModels.Contact domainContact in contacts)
            {
                mapped_contacts.Add(domainContact.MapDomainContactToViewContact());
            }

            return mapped_contacts;
        }
    }
}
