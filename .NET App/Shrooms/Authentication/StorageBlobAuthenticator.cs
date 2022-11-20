using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

namespace Shrooms.Authentication
{
    public class StorageBlobAuthenticator
    {
        public static BlobServiceClient AutencitateBlobStorage(string connectionString)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            return blobServiceClient;
        }

        public static BlobContainerClient GetBlobContainerClient(string connectionString, string containerName)
        {
            var container = new BlobContainerClient(connectionString, containerName);

            return container;
        }

    }
}
