using System;

namespace AzureFileStorage.API.Models
{
    public class AzureFileResponse : FileResponse
    {
        
        public AzureFileResponse(string response)
        {
            Response = response;
        }
    }
}
