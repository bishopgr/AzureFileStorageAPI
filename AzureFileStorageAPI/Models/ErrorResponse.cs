using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFileStorage.API.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public DateTime TimeStamp => DateTime.Now;

        public ErrorResponse(string message)
        {
            Message = message;
        }
    }
}
