using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Recommendation.Infrastructure
{
    public interface IRecommendationRepository
    {
        Task<UserStatisticsEntity> GetUserStatistics(string userId, string profileId);
        Task<bool> UpdateUserStatistics(UserStatisticsEntity userStatistics);
        Task<bool> AddUserStatistics(UserStatisticsEntity userStatistics);
    }

    public class RecommendationRepository : IRecommendationRepository
    {
        private const string RecommendationTable = "recommendation";
        private readonly string _storageConnectionString;

        public RecommendationRepository(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public async Task<UserStatisticsEntity> GetUserStatistics(string userId, string profileId)
        {
            var retrieveOperation = TableOperation.Retrieve<UserStatisticsEntity>(userId, profileId);
            var table = GetTable(RecommendationTable, _storageConnectionString);
            var userStatistics = await table.ExecuteAsync(retrieveOperation);

            if (userStatistics?.Result is UserStatisticsEntity storedUserStatistics)
            {
                return storedUserStatistics;
            }
            return null;
        }

        public async Task<bool> UpdateUserStatistics(UserStatisticsEntity userStatistics)
        {
            var table = GetTable(RecommendationTable, _storageConnectionString);
            var retrieveOperation = TableOperation.Retrieve<UserStatisticsEntity>(userStatistics.PartitionKey, userStatistics.RowKey);
            var updateEntity = await table.ExecuteAsync(retrieveOperation);
            if (updateEntity?.Result is UserStatisticsEntity storedUserStatistics)
            {
                storedUserStatistics = userStatistics;
                var updateOperation = TableOperation.Replace(storedUserStatistics);
                var updateResult = await table.ExecuteAsync(updateOperation);
                return updateResult.HttpStatusCode == (int)HttpStatusCode.OK;
            }
            return false;
        }

        public async Task<bool> AddUserStatistics(UserStatisticsEntity userStatistics)
        {
            var table = GetTable(RecommendationTable, _storageConnectionString);
            var insertOperation = TableOperation.Insert(userStatistics);
            var insertResult = await table.ExecuteAsync(insertOperation);
            return insertResult.HttpStatusCode == (int)HttpStatusCode.OK;
        }

        private static CloudTable GetTable(string table, string storageConnectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(table);
        }
    }
}
