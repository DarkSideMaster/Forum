using Forum.Data.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Forum.ForumServises
{
    public class UploadService : IUpload
    {
        public CloudBlobContainer GetBlobContainer(string connectionString) 
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();

            //имя не менять, так называется контейнер в Azure
            return blobClient.GetContainerReference("profile-images");
        }
    }
}
