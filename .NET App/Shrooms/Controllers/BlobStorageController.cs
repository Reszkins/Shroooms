using Microsoft.AspNetCore.Components.Forms;
using System.IO;

namespace Shrooms.Controllers
{
    public class BlobStorageController
    {
        public async Task SaveFileToStorage(IBrowserFile file)
        {
            //zapis obrazu do blob storage
            //await blobContainerClient.UploadBlobAsync(trustedFilename, browserFile.OpenReadStream());
        }
    }
}
