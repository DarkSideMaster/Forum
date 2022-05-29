using Microsoft.WindowsAzure.Storage.Blob;

namespace Forum.Data.Interfaces
{
    public interface IUpload
    {

        CloudBlobContainer GetBlobContainer(string connectionString, string containerNameInAzure);

    }
}
