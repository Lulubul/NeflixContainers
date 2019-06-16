using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using MoreLinq;
using MovieMetadata.Infrastructure.Entities;
using static MoreLinq.Extensions.LeadExtension;

namespace MovieMetadata.Infrastructure
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieEntity>> GetTopMoviesInCategoriesAsync();
        Task<IEnumerable<MovieGenreEntity>> GetGenresAsync();
        Task<Stream> GetMovieByNameAsync(string name);
        Task<List<MovieEntity>> GetMoviesByIdsAsync(string[] ids);
        Task<List<MovieEntity>> GetMoviesByRelaseYearAndGenreAsync(string releaseYear, string genre);
    }

    public class MovieRepository : IMovieRepository
    {
        private const string MoviesContainer = "movies";
        private const string MoviesGenresContainer = "genres";
        private readonly string _storageConnectionString;
        private readonly string _blobConnectionString;

        public MovieRepository(string storageConnectionString, string blobConnectionString)
        {
            _storageConnectionString = storageConnectionString;
            _blobConnectionString = blobConnectionString;
        }

        public async Task<IEnumerable<MovieEntity>> GetTopMoviesInCategoriesAsync()
        {
            var table = GetTable(MoviesContainer, _storageConnectionString);
            var movies = new List<MovieEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var querySegment = await table.ExecuteQuerySegmentedAsync(new TableQuery<MovieEntity>(), continuationToken);
                continuationToken = querySegment.ContinuationToken;
                movies.AddRange(querySegment.Results);
            }
            while (continuationToken != null);
            return movies;
        }

        public async Task<IEnumerable<MovieGenreEntity>> GetGenresAsync()
        {
            var table = GetTable(MoviesGenresContainer, _storageConnectionString);
            var movies = new List<MovieGenreEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var querySegment = await table.ExecuteQuerySegmentedAsync(new TableQuery<MovieGenreEntity>(), continuationToken);
                continuationToken = querySegment.ContinuationToken;
                movies.AddRange(querySegment.Results);
            }
            while (continuationToken != null);
            return movies;
        }

        public async Task<Stream> GetMovieByNameAsync(string name)
        {
            return await GetContainer(_blobConnectionString, MoviesContainer)
                        .GetBlobReference(name)
                        .OpenReadAsync();
        }

        private static CloudTable GetTable(string table, string storageConnectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(table);
        }

        private CloudBlobContainer GetContainer(string storageConnectionString, string container)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(container);
        }

        public async Task<List<MovieEntity>> GetMovies()
        {
            var table = GetTable(MoviesContainer, _storageConnectionString);
            var movies = new List<MovieEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var querySegment = await table.ExecuteQuerySegmentedAsync(new TableQuery<MovieEntity>(), continuationToken);
                continuationToken = querySegment.ContinuationToken;
                movies.AddRange(querySegment.Results);
            }
            while (continuationToken != null);
            return movies;
        }

        public async Task<List<MovieEntity>> GetMoviesByIdsAsync(string[] ids)
        {
            var movies = await GetMovies();
            return movies.Where((movie) => ids.Contains(movie.RowKey)).ToList();
        }


        public async Task<List<MovieEntity>> GetMoviesByFilter(string filterType, string filterValue) {
            var query = new TableQuery<MovieEntity>().Where(TableQuery.GenerateFilterCondition(filterType, QueryComparisons.Equal, filterValue));
            var table = GetTable(MoviesContainer, _storageConnectionString);
            var movies = new List<MovieEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var querySegment = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                    continuationToken = querySegment.ContinuationToken;
                movies.AddRange(querySegment.Results);
            }
            while (continuationToken != null);
            return movies;
        }

        public async Task<List<MovieEntity>> GetMoviesByRelaseYearAndGenreAsync(string releaseYear, string genre)
        {
            var moviesByReleaseYear = await GetMoviesByFilter("ReleaseYear", releaseYear);
            var moviesByGenres = await GetMoviesByFilter("Genres", genre);
            return moviesByGenres.Union(moviesByReleaseYear).DistinctBy(i => i.RowKey).ToList();
        }
    }
}
