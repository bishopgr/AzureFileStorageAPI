using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
