namespace AzureFileStorage.API.Models
{
    public abstract class FileResponse
    {
        public int HttpStatusCode { get; set; }
        public string Response { get; set; }
    }
}