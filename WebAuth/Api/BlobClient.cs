using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAuth.Api
{
    public class BlobClient
    {
        public CloudBlobClient _blobClient;
        public CloudBlobContainer _blobContainer;
        public CloudBlockBlob _cloudBlockBlob;
        private const string _blobContainerName = @"blobstorage";

        public async Task SetupCloudBlob()
        {
            var connectionString = CloudConfigurationManager.GetSetting("petshopauthblob");
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            _blobClient = storageAccount.CreateCloudBlobClient();
            _blobContainer = _blobClient.GetContainerReference(_blobContainerName);

            await _blobContainer.CreateIfNotExistsAsync();

            var permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            await _blobContainer.SetPermissionsAsync(permissions);
        }

        public string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }

        public async Task ReadCloudBlob(string _ImagePathBlob)
        {

            var connectionString = CloudConfigurationManager.GetSetting("petshopauthblob");
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();

            _blobContainer = _blobClient.GetContainerReference(_blobContainerName);
            _cloudBlockBlob = _blobContainer.GetBlockBlobReference(_ImagePathBlob);

            await _blobContainer.ExistsAsync();

            var permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            await _blobContainer.SetPermissionsAsync(permissions);

        }

    }
}