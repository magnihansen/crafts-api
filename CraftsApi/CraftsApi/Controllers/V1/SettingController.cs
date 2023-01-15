using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CraftsApi.Auth;
using CraftsApi.DomainModels;
using CraftsApi.Helpers;
using CraftsApi.Service;
using CraftsApi.Service.Authentication;
using CraftsApi.Controllers.V1.Requests;
using CraftsApi.Service.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CraftsApi.Service.Mappings;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    public class SettingController : BaseController
    {
        private readonly ISettingService _settingService;

        public SettingController(
            ISettingService settingService
        )
        {
            _settingService = settingService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DomainSettingVM>))]
        public async Task<IActionResult> GetDomainSettings()
        {
            List<DomainSettingVM> settings = await _settingService.GetDomainSettingsAsync(Request.GetAddressHost());

            return Ok(settings);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SettingVM>))]
        public async Task<IActionResult> GetSettings()
        {
            List<SettingVM> settings = await _settingService.GetSettingsAsync(Request.GetAddressHost());

            return Ok(settings);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SettingKeyVM>))]
        public async Task<IActionResult> GetSettingKeys()
        {
            List<SettingKeyVM> settingKeys = await _settingService.GetSettingKeysAsync();

            return Ok(settingKeys);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SettingTypeVM>))]
        public async Task<IActionResult> GetSettingTypes()
        {
            List<SettingTypeVM> settingTypes = await _settingService.GetSettingTypesAsync();

            return Ok(settingTypes);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SettingVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSetting(int settingId)
        {
            var user = await _settingService.GetSettingAsync(
                host: Request.GetAddressHost(),
                settingId: settingId
            );
            if (user is null)
            {
                return NotFound("Setting not found");
            }

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SettingVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertSetting(InsertSettingRequest insertSettingRequest)
        {
            var setting = new DomainModels.Setting()
            {
                SettingKeyId = insertSettingRequest.SettingKeyId,
                Value = insertSettingRequest.Value,
                CreatedBy = insertSettingRequest.CreatedBy
            };
            SettingVM insertedSettingVM = await _settingService.InsertSettingAsync(
                host: Request.GetAddressHost(),
                setting: setting
            );
            if(insertedSettingVM is null)
            {
                return NotFound("Setting not inserted");
            }
            return Ok(insertedSettingVM);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SettingVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertSettingKey(InsertSettingKeyRequest insertSettingKeyRequest)
        {
            var settingKey = new DomainModels.SettingKey()
            {
                SettingTypeId = insertSettingKeyRequest.SettingTypeId,
                Key = insertSettingKeyRequest.Key,
                IsDefault = insertSettingKeyRequest.IsDefault,
                DefaultValue = insertSettingKeyRequest.DefaultValue
            };
            SettingKeyVM insertedSettingKeyVM = await _settingService.InsertSettingKeyAsync(
                settingKey: settingKey
            );
            if (insertedSettingKeyVM is null)
            {
                return NotFound("Setting key not inserted");
            }
            return Ok(insertedSettingKeyVM);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> UpdateSetting(UpdateSettingRequest updateSettingRequest)
        {
            DomainModels.Setting setting = updateSettingRequest.MapUpdateSettingRequestToSetting();
            bool userUpdated = await _settingService.UpdateSettingAsync(
                host: Request.GetAddressHost(),
                setting: setting
            );
            return Ok(userUpdated);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteSetting(int settingId)
        {
            bool userDeleted = await _settingService.DeleteSettingAsync(
                host: Request.GetAddressHost(),
                settingId: settingId
            );
            return Ok(userDeleted);
        }
    }
}
 