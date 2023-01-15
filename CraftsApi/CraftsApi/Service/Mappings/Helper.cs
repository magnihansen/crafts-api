using System.Collections.Generic;
using System.Linq;
using CraftsApi.Controllers.V1.Requests;

namespace CraftsApi.Service.Mappings
{
    public static class Helper
    {
        #region Page mappings

        public static List<ViewModels.PageVM> MapPageToPageVM(this List<DomainModels.Page> pages)
        {
            List<ViewModels.PageVM> mapped_pages = pages.Select(p => p.MapPageToPageVM()).ToList();

            return mapped_pages;
        }

        public static ViewModels.PageVM MapPageToPageVM(this DomainModels.Page page)
        {
            if (page is null)
            {
                // throw new System.Exception("Page null");
            }

            if (page.ParentId is null)
            {
                // throw new System.Exception("ParentId null");
            }

            if (page.Title is null)
            {
                // throw new System.Exception("Title null");
            }

            if (page.Content is null)
            {
                // throw new System.Exception("Content null");
            }

            if (page.Link is null)
            {
                // throw new System.Exception("Link null");
            }

            if (page.UpdatedBy is null)
            {
                // throw new System.Exception("UpdatedBy null");
            }

            return new ViewModels.PageVM(
                id: page.Id,
                parentId: page.ParentId,
                pageTypeId: page.PageTypeId,
                title: page.Title,
                content: page.Content,
                sort: page.Sort,
                link: page.Link,
                isRouterLink: page.IsRouterLink,
                active: page.Active,
                createdDate: page.CreatedDate,
                createdBy: page.CreatedBy,
                updatedDate: page.UpdatedDate,
                updatedBy: page.UpdatedBy
            );
        }

        public static DomainModels.Page MapAddPageRequestToPage(this AddPageRequest page)
        {
            return new DomainModels.Page
            {
                Uid = page.Uid,
                ParentId = page.ParentId,
                PageTypeId = page.PageTypeId,
                Title = page.Title,
                Content = page.Content,
                Sort = page.Sort,
                Link = page.Link,
                IsRouterLink = page.IsRouterLink,
                Active = page.Active,
                CreatedBy = page.CreatedBy
            };
        }

        public static DomainModels.Page MapUpdatePageRequestToPage(this UpdatePageRequest page)
        {
            return new DomainModels.Page
            {
                Id = page.Id,
                ParentId = page.ParentId,
                PageTypeId = page.PageTypeId,
                Title = page.Title,
                Content = page.Content,
                Sort = page.Sort,
                Link = page.Link,
                IsRouterLink = page.IsRouterLink,
                Active = page.Active,
                UpdatedDate = page.UpdatedDate,
                UpdatedBy = page.UpdatedBy
            };
        }

        #endregion

        #region PageType mappings

        public static List<ViewModels.PageTypeVM> MapPageTypeToPageTypeVM(this List<DomainModels.PageType> pageTypes)
        {
            List<ViewModels.PageTypeVM> mapped_pages = pageTypes.Select(pt => pt.MapPageTypeToPageTypeVM()).ToList();

            return mapped_pages;
        }

        public static ViewModels.PageTypeVM MapPageTypeToPageTypeVM(this DomainModels.PageType pageType)
        {
            return new ViewModels.PageTypeVM(
                id: pageType.Id,
                name: pageType.Name,
                active: pageType.Active,
                createdDate: pageType.CreatedDate,
                createdBy: pageType.CreatedBy,
                updatedDate: pageType.UpdatedDate,
                updatedBy: pageType.UpdatedBy
            );
        }

        public static DomainModels.PageType MapAddPageTypeRequestToPageType(this AddPageTypeRequest pageType)
        {
            return new DomainModels.PageType
            {
                Name = pageType.Name,
                Active = pageType.Active,
                CreatedBy = pageType.CreatedBy
            };
        }

        public static DomainModels.PageType MapUpdatePageTypeRequestToPageType(this UpdatePageTypeRequest pageType)
        {
            return new DomainModels.PageType
            {
                Id = pageType.Id,
                Name = pageType.Name,
                Active = pageType.Active,
                UpdatedDate = pageType.UpdatedDate,
                UpdatedBy = pageType.UpdatedBy
            };
        }

        #endregion

        #region User mappings

        public static List<ViewModels.UserVM> MapUserToUserVM(this List<DomainModels.User> users)
        {
            List<ViewModels.UserVM> mapped_users = users.Select(u => u.MapUserToUserVM()).ToList();

            return mapped_users;
        }

        public static ViewModels.UserVM MapUserToUserVM(this DomainModels.User user)
        {
            if (user.Id == 0)
            {
                return null;
            }

            return new ViewModels.UserVM(
                id: user.Id,
                domainId: user.DomainId,
                username: user.Username,
                firstname: user.Firstname,
                lastname: user.Lastname,
                address: user.Address,
                zip: user.Zip,
                city: user.City,
                country: user.Country,
                email: user.Email,
                phone: user.Phone,
                active: user.Active,
                createdDate: user.CreatedDate,
                createdBy: user.CreatedBy,
                updatedDate: user.UpdatedDate,
                updatedBy: user.UpdatedBy,
                isAdmin: user.IsAdmin
            );
        }

        public static DomainModels.User MapAddUserRequestToUser(this AddUserRequest user)
        {
            return new DomainModels.User
            {
                Username = user.Username,
                Password = user.Password,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Address = user.Address,
                Zip = user.Zip,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                Phone = user.Phone,
                Active = user.Active,
                CreatedBy = user.CreatedBy
            };
        }

        public static DomainModels.User MapUpdateUserRequestToUser(this UpdateUserRequest user)
        {
            return new DomainModels.User
            {
                Username = user.Username,
                Password = user.Password,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Address = user.Address,
                Zip = user.Zip,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                Phone = user.Phone,
                Active = user.Active,
                UpdatedDate = user.UpdatedDate,
                UpdatedBy = user.UpdatedBy
            };
        }

        #endregion

        #region Data mappings

        public static ViewModels.ContactVM MapDomainContactToViewContact(this DomainModels.Contact contact)
        {
            return new ViewModels.ContactVM(
                id: contact.Id,
                name: contact.Name,
                email: contact.Email,
                phone: contact.Phone,
                message: contact.Message,
                createdDate: contact.CreatedDate,
                createdBy: contact.CreatedBy
            );
        }

        public static List<ViewModels.ContactVM> MapContactToContactVM(this List<DomainModels.Contact> contacts)
        {
            List<ViewModels.ContactVM> mapped_contacts = contacts.Select(c => c.MapDomainContactToViewContact()).ToList();

            return mapped_contacts;
        }

        #endregion

        #region Setting mappings

        public static ViewModels.DomainSettingVM MapSettingDTOToDomainSettingVM(this DomainModels.SettingDTO setting)
        {
            return new ViewModels.DomainSettingVM(
                key: setting.Key,
                value: setting.Value
            );
        }

        public static ViewModels.SettingVM MapSettingDTOToSettingVM(this DomainModels.SettingDTO setting)
        {
            return new ViewModels.SettingVM(
                id: setting.Id,
                settingKeyId: setting.SettingKeyId,
                key: setting.Key,
                value: setting.Value,
                createdDate: setting.CreatedDate,
                createdBy: setting.CreatedBy,
                updatedDate: setting.UpdatedDate,
                updatedBy: setting.UpdatedBy
            );
        }

        public static ViewModels.SettingKeyVM MapSettingKeyToSettingKeyVM(this DomainModels.SettingKey settingKey)
        {
            return new ViewModels.SettingKeyVM(
                id: settingKey.Id,
                settingTypeId: settingKey.SettingTypeId,
                key: settingKey.Key,
                isDefault: settingKey.IsDefault,
                defaultValue: settingKey.DefaultValue
            );
        }

        public static ViewModels.SettingTypeVM MapSettingTypeToSettingTypeVM(this DomainModels.SettingType settingType)
        {
            return new ViewModels.SettingTypeVM(
                id: settingType.Id,
                name: settingType.Name
            );
        }

        public static List<ViewModels.DomainSettingVM> MapSettingDTOToDomainSettingVM(this List<DomainModels.SettingDTO> settings)
        {
            List<ViewModels.DomainSettingVM> mapped_settings = settings.Select(s => s.MapSettingDTOToDomainSettingVM()).ToList();

            return mapped_settings;
        }

        public static List<ViewModels.SettingVM> MapSettingDTOToSettingVM(this List<DomainModels.SettingDTO> settings)
        {
            List<ViewModels.SettingVM> mapped_settings = settings.Select(s => s.MapSettingDTOToSettingVM()).ToList();

            return mapped_settings;
        }

        public static List<ViewModels.SettingKeyVM> MapSettingKeyToSettingKeyVM(this List<DomainModels.SettingKey> settingKeys)
        {
            List<ViewModels.SettingKeyVM> mapped_settingKeys = settingKeys.Select(s => s.MapSettingKeyToSettingKeyVM()).ToList();

            return mapped_settingKeys;
        }

        public static List<ViewModels.SettingTypeVM> MapSettingTypeToSettingTypeVM(this List<DomainModels.SettingType> settingTypes)
        {
            List<ViewModels.SettingTypeVM> mapped_settingTypes = settingTypes.Select(s => s.MapSettingTypeToSettingTypeVM()).ToList();

            return mapped_settingTypes;
        }

        public static DomainModels.Setting MapAddSettingRequestToSetting(this InsertSettingRequest addSettingRequest)
        {
            return new DomainModels.Setting(

            );
        }

        public static DomainModels.Setting MapUpdateSettingRequestToSetting(this UpdateSettingRequest updateSettingRequest)
        {
            return new DomainModels.Setting(

            );
        }

        #endregion
    }
}
