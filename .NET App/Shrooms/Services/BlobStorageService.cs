using Microsoft.AspNetCore.Components.Forms;
using Shrooms.Authentication;
using Azure.Storage.Blobs;
using System.IO;

namespace Shrooms.Services
{
    public class BlobStorageService
    {
        private readonly IConfiguration _configuration;
        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> SaveFileToBlobStorage(IBrowserFile file)
        {
            var container = StorageBlobAuthenticator.GetBlobContainerClient(_configuration["BlobStorage:ConnectionString"], _configuration["BlobStorage:ContainerName"]);
            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.Name);
            var newBlob = container.GetBlobClient(blobName);
            await newBlob.UploadAsync(file.OpenReadStream());

            return blobName;
        }
    }
}
