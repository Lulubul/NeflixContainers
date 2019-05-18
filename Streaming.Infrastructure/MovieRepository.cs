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

       
    }
}