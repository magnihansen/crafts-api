using System;
using System.Threading.Tasks;
using CraftsApi.Auth;
using CraftsApi.Helpers;
using CraftsApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    public class CdnController : BaseController
    {
        private readonly ICdnTokenService _cdnTokenService;

        public CdnController(ICdnTokenService cdnTokenService)
        {
            _cdnTokenService = cdnTokenService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tokens))]
        public async Task<Tokens> GetImagesAsync()
        {
            Tokens token = await _cdnTokenService.GenerateCdnTokenAsync(Request.GetAddressHost());
            return token;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DomainModels.Image))]
        public async Task<DomainModels.CdnToken> GetCdnTokenAsync(int cdnTokenId)
        {
            DomainModels.CdnToken cdnToken = await _cdnTokenService.GetCdnTokenAsync(cdnTokenId);
            return cdnToken;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> InsertCdnTokenAsync(DomainModels.CdnToken cdnToken)
        {
            bool inserted = await _cdnTokenService.InsertCdnTokenAsync(cdnToken);
            return inserted;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> UpdateCdnTokenAsync(DomainModels.CdnToken cdnToken)
        {
            bool updated = await _cdnTokenService.UpdateCdnTokenAsync(cdnToken);
            return updated;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> DeleteCdnTokenAsync(int cdnTokenId)
        {
            bool deleted = await _cdnTokenService.DeleteCdnTokenAsync(cdnTokenId);
            return deleted;
        }
    }
}

