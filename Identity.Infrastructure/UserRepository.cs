using Identity.API.Application.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    public interface IUserRepository
    {
        Task<User> AddUser(UserEntity user);
        Task<UserEntity> Login(UserEntity userLogin);
    }

    public class UserRepository : IUserRepository
    {
        private readonly string _storageConnectionString;
        private const string TableName = "history";

        public UserRepository(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public async Task<UserEntity> Login(UserEntity user)
        {
            var query = new TableQuery<UserEntity>()
                .Where(TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal, user.Email)).Take(1);

            var table = GetTable(TableName, _storageConnectionString);
            TableContinuationToken continuationToken = null;
            var result = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
            return result?.Results[0];
        }

        public async Task<User> AddUser(UserEntity newUser)
        {
            TableOperation insertOperation = TableOperation.Insert(newUser);
            var table = GetTable(TableName, _storageConnectionString);
            await table.ExecuteAsync(insertOperation);
            return new User { Id = newUser.RowKey };
        }

        private static CloudTable GetTable(string table, string storageConnectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(table);
        }
    }
}
