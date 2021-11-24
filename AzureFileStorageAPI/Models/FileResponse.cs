using System;

namespace AzureFileStorage.API.Models
{
    public abstract class FileResponse
    {
        public string Response { get; set; }
        public DateTime DateUploaded => DateTime.UtcNow;
    }
}