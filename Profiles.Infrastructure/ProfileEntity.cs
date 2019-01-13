using Microsoft.WindowsAzure.Storage.Table;

namespace Profiles.Infrastructure
{
    public class ProfileEntity: TableEntity
    {
        public string AvatarUrl { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string MaturityLevel { get; set; }
    }
}
