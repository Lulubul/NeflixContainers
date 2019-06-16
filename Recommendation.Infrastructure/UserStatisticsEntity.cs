using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;

namespace Recommendation.Infrastructure
{
    public class UserStatisticsEntity : TableEntity
    {
        public string GenresPreferences { get; set; }
        public string RelaseYearPreferences { get; set; }
        public string VideoIdPreferences { get; set; }
    }
}
