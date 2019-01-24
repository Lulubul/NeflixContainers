using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Profiles.Infrastructure.Entities;

namespace Profiles.Infrastructure
{
    public interface IProfileRepository
    {
        Task<IList<ProfileEntity>> GetProfiles(Guid userId);
        Task<bool> UpdateUserProfile(Guid userId, ProfileEntity profile);
        Task<bool> AddUserProfile(Guid userId, ProfileEntity profile);
    }

    public class ProfileRepository: IProfileRepository
    {
        private const string ProfilesTable = "profiles";
        private readonly string _storageConnectionString;

        public ProfileRepository(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public async Task<IList<ProfileEntity>> GetProfiles(Guid userId)
        {
            var query = new TableQuery<ProfileEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId.ToString())
            );
            var profiles = new List<ProfileEntity>();
            var table = GetTable(ProfilesTable, _storageConnectionString);
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<ProfileEntity> querySegment = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = querySegment.ContinuationToken;
                profiles.AddRange(querySegment.Results);
            }
            while (continuationToken != null);
            return profiles;
        }

        public async Task<bool> UpdateUserProfile(Guid userId, ProfileEntity profileEntity)
        {
            var table = GetTable(ProfilesTable, _storageConnectionString);
            var retrieveOperation = TableOperation.Retrieve<ProfileEntity>(profileEntity.PartitionKey, profileEntity.RowKey);
            var updateEntity = await table.ExecuteAsync(retrieveOperation);
            if (updateEntity?.Result is ProfileEntity storedProfileEntity)
            {
                storedProfileEntity = profileEntity;
                var updateOperation = TableOperation.Replace(storedProfileEntity);
                var updateResult = await table.ExecuteAsync(updateOperation);
                return updateResult.HttpStatusCode == (int)HttpStatusCode.OK;
            }
            return false;
        }

        public async Task<bool> AddUserProfile(Guid userId, ProfileEntity profile)
        {
            var table = GetTable(ProfilesTable, _storageConnectionString);
            var insertOperation = TableOperation.Insert(profile);
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
