using AzureFileStorage.API.Models;
using AzureFileStorage.API.Models.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AzureFileStorage.API
{
    public class AzureBlobStorageProvider
    {

        private readonly CloudStorageAccount _cloudStorageAccount;
        private readonly CloudBlobClient _cloudBlobClient;

        private readonly CloudBlobContainer _cloudBlobContainer;
        public AzureBlobStorageProvider(IAzureConnectionSettings azureConnectionSettings)
        {

            _cloudStorageAccount = CloudStorageAccount.Parse(azureConnectionSettings.ConnectionString);
            _cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
            _cloudBlobContainer = _cloudBlobClient.GetContainerReference("uploads");
        }

        public string Randomize(string prefix = "Item") =>
            $"{prefix}-{Guid.NewGuid()}";

        public async Task DeleteBlobData(string fileUrl)
        {
            Uri uri = new(fileUrl);
            CloudBlobDirectory cloudBlobDirectory = _cloudBlobContainer.GetDirectoryReference(uri.LocalPath);
            var cloudBlockBlob = cloudBlobDirectory.GetBlockBlobReference(uri.LocalPath);

            await cloudBlockBlob.DeleteAsync();
        }

        public async Task<FileResponse> UploadFileAsync(IFormFile file)
        {
            
            try
            {
                if (await _cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await _cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }

                if (file is not null)
                {
                    return await ProcessUpload(file);
                }
            }
            catch(Exception e)
            {
                return new ErrorResponse($"The file upload of {file.FileName} failed. Additional info: {e.Message}");
            }

            return new AzureFileResponse($"Nothing was uploaded.");

        }

        public async Task<List<FileResponse>> UploadManyAsync(IFormFile[] files)
        {
            return await ProcessUploadMany(files);
        }

        private async Task<AzureFileResponse> ProcessUpload(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            var blockBlob = _cloudBlobContainer.GetBlockBlobReference(file.FileName);
            blockBlob.Properties.ContentType = file.ContentType;
            await blockBlob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);

            return new AzureFileResponse(blockBlob.Uri.AbsoluteUri);
        }

        private async Task<List<FileResponse>> ProcessUploadMany(IFormFile[] files)
        {
            List<FileResponse> fileResponses = new();

            try
            {
                if (await _cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await _cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }

                using var ms = new MemoryStream();

                foreach (var file in files)
                {
                    await file.CopyToAsync(ms);

                    var fileBytes = ms.ToArray();
                    var blockBlob = _cloudBlobContainer.GetBlockBlobReference(file.FileName);

                    blockBlob.Properties.ContentType = file.ContentType;
                    await blockBlob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);
                    fileResponses.Add(new AzureFileResponse(blockBlob.Uri.AbsoluteUri));
                    
                }
                return fileResponses;
            }
            catch (Exception e)
            {
                fileResponses.Add(new ErrorResponse($"The {files.Length} files you attempted to upload failed. More info: {e?.Message}"));
                return fileResponses;
            }
        }

    }
}
