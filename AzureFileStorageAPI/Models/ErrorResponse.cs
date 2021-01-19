using System;

namespace AzureFileStorage.API.Models
{
    public class ErrorResponse : FileResponse
    {
        public DateTime TimeStamp => DateTime.Now;

        public ErrorResponse(string response)
        {
            Response = response;
        }
    }
}
