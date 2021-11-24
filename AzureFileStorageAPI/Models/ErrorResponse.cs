using System;

namespace AzureFileStorage.API.Models
{
    public class ErrorResponse : FileResponse
    {
        public ErrorResponse(string response)
        {
            Response = response;
        }
    }
}
