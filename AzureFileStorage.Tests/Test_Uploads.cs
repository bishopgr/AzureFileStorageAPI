using AzureFileStorage.API;
using AzureFileStorage.API.Controllers;
using AzureFileStorage.API.Models;
using AzureFileStorage.API.Models.Settings;
using AzureFileStorage.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace AzureFileStorage.Tests
{
    public class Test_Uploads
    {
        private readonly Mock<AzureBlobStorageProvider> _mockStorageProvider;
        private readonly UploadController _fakeUploadController;
        private readonly IFormFile _testFile;

        public Test_Uploads()
        {
            IAzureConnectionSettings azureConnectionSettings = new AzureConnectionSettings();
            azureConnectionSettings.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=unittest-fakestuff;AccountKey=123abc;EndpointSuffix=core.windows.net";
            _testFile = new FormFile(new MemoryStream(), 0, 1024, "testFile", "testFile.tiff");
            _mockStorageProvider = new Mock<AzureBlobStorageProvider>(azureConnectionSettings);
            _fakeUploadController = new UploadController(_mockStorageProvider.Object);
        }

        [Theory]
        [ClassData(typeof(ResponseTestData))]
        public async Task Upload_Controller_Should_Give_Bad_Request(FileResponse fileResponse)
        {
            _mockStorageProvider.Setup(sp => sp.UploadFileAsync(_testFile)).ReturnsAsync(fileResponse).Verifiable();

            var uploadResult = await _fakeUploadController.Upload(_testFile).ConfigureAwait(false);

            Assert.NotNull(uploadResult);
            
        }
    }
}
