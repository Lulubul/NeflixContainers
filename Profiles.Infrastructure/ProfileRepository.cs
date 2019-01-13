using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Profiles.Infrastructure
{

    public interface IProfileRepository
    {
        Task<IList<ProfileEntity>> GetProfiles(Guid userId);
        Task<bool> UpdateUserProfile(Guid userId, ProfileEntity profile);
        Task<bool> AddUserProfile(Guid userId, ProfileEntity profile);
    }

    public class ProfileRepository : IProfileRepository
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

        public Task<bool> UpdateUserProfile(Guid userId, ProfileEntity profile)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserProfile(Guid userId, ProfileEntity profile)
        {
            throw new NotImplementedException();
        }

        private CloudTable GetTable(string table, string storageConnectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(table);
        }
    }
}
