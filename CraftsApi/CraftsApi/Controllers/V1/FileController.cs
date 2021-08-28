using System.IO;
using System.Threading.Tasks;
using CraftsApi.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    public class FileController : BaseController
    {
        public FileController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            using (var sr = new StreamReader(this.Request.Body))
            {
                var content = await sr.ReadToEndAsync();
                FileUploadResponse fileUploadResponse =
                    new FileUploadResponse(
                        true,
                        "",
                        "",
                        "Image upload successful",
                        "data:image/jpg;base64," + content
                    );

                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                return Ok(JsonConvert.SerializeObject(fileUploadResponse, serializerSettings));
            }
        }
    }
}
 