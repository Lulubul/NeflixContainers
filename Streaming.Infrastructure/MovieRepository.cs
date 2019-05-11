using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace Streaming.Infrasturcture
{
    public interface IMovieRepository
    {
        Task<Stream> GetMovieByNameAsync(string name);
    }

    public class MovieRepository : IMovieRepository
    {
        private const string MoviesContainer = "movies";
        private readonly string _connectionString;

        public MovieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Stream> GetMovieByNameAsync(string name)
        {
            return await GetContainer(_connectionString, MoviesContainer)
                        .GetBlobReference(name)
                        .OpenReadAsync();
        }

        private CloudBlobContainer GetContainer(string storageConnectionString, string container)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(container);
        }
    }
}