using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace History.Infrastructure
{
    public interface IHistoryRepository
    {
        Task<bool> AddAsync(HistoryEntity historyEntity);
        Task<List<HistoryEntity>> GetAll(string userId, string profileId);
    }

    public class HistoryRepository : IHistoryRepository
    {
        private const string TableName = "history";
        private readonly string _storageConnectionString;

        public HistoryRepository(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public async Task<bool> AddAsync(HistoryEntity historyEntity)
        {
            var dbHistoryEntity = await GetByUserIdAndMovieId(historyEntity.PartitionKey, historyEntity.RowKey);
            if (dbHistoryEntity != null)
            {
                return await Task.FromResult(false);
            }
            var insertOperation = TableOperation.Insert(historyEntity);
            var table = GetTable(TableName, _storageConnectionString);
            await table.ExecuteAsync(insertOperation);
            return await Task.FromResult(true);
        }


        public async Task<HistoryEntity> GetByUserIdAndMovieId(string userId, string movieId)
        {
            var query = new TableQuery<HistoryEntity>()
                .Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, movieId)))
                .Take(1);
            var table = GetTable(TableName, _storageConnectionString);
            var result = await table.ExecuteQuerySegmentedAsync(query, null);
            return result?.Results.Count > 0 ? result.Results[0] : null;
        }

        public async Task<List<HistoryEntity>> GetAll(string userId, string profileId)
        {
            var table = GetTable(TableName, _storageConnectionString);
            var query = new TableQuery<HistoryEntity>()
                .Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition("ProfileId", QueryComparisons.Equal, profileId)));

            var history = new List<HistoryEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var querySegment = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = querySegment.ContinuationToken;
                history.AddRange(querySegment.Results);
            }
            while (continuationToken != null);
            return history;
        }

        private static CloudTable GetTable(string table, string storageConnectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(table);
        }
    }
}