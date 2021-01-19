namespace AzureFileStorage.API.Models
{
    public class AzureFileResponse : FileResponse
    {
      
        public AzureFileResponse(string response)
        {
            HttpStatusCode = 200; //assuming all AzureFileResponses are ok, as ErrorResponse should be returned on a real error.
            Response = response;
        }
    }
}
