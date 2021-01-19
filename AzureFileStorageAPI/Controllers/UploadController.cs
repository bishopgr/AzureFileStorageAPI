using AzureFileStorage.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AzureBlobStorageProvider _azureBlobStorageProvider;

        public UploadController(AzureBlobStorageProvider azureBlobStorageProvider)
        {
            _azureBlobStorageProvider = azureBlobStorageProvider;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var upload = await _azureBlobStorageProvider.UploadFileAsync(file);
            if(upload is ErrorResponse)
            {
                return BadRequest(upload.Response);
            }

            return Ok(upload);
        }
    }
}
