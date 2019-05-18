using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Identity.Infrastructure
{
    public interface IPlanRepository
    {
        Task<IEnumerable<PlanEntity>> GetAllPlans();
    }

    public class PlanRepository : IPlanRepository
    {
        public string TableName = "Plans";
        private readonly string _storageConnectionString;

        public PlanRepository(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public async Task<IEnumerable<PlanEntity>> GetAllPlans()
        {
            var table = GetTable(TableName, _storageConnectionString);
            var plans = new List<PlanEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var querySegment = await table.ExecuteQuerySegmentedAsync(new TableQuery<PlanEntity>(), continuationToken);
                continuationToken = querySegment.ContinuationToken;
                plans.AddRange(querySegment.Results);
            }
            while (continuationToken != null);
            return plans;
        }

        protected CloudTable GetTable(string table, string storageConnectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(table);
        }
    }
}
