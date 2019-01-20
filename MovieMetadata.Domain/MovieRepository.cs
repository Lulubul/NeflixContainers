using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using MovieMetadata.Infrastructure.Entities;

namespace MovieMetadata.Infrastructure
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieEntity>> GetTopMoviesInCategoriesAsync();
        Task<IEnumerable<MovieGenreEntity>> GetGenresAsync();
    }

    public class MovieRepository : IMovieRepository
    {
        private const string MoviesContainer = "movies";
        private const string MoviesGenresContainer = "genres";
        private readonly string _storageConnectionString;

        public MovieRepository(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
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

        private static CloudTable GetTable(string table, string storageConnectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(table);
        }

    }
}
