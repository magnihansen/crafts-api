using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DomainModels;
using CraftsApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    public class DataController : BaseController
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContact(int contactId)
        {
            var contact = await _dataService.GetContactAsync(contactId);
            if (contact == null)
            {
                return NotFound(contact);
            }
            return Ok(contact);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Contact>))]
        public async Task<IActionResult> GetContacts()
        {
            List<Service.ViewModels.Contact> contacts = await _dataService.GetContactsAsync();
            return Ok(contacts);
        }
    }
}
 