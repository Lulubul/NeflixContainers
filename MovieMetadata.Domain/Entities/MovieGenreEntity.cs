using Microsoft.WindowsAzure.Storage.Table;

namespace MovieMetadata.Infrastructure.Entities
{
    public class MovieGenreEntity: TableEntity
    {
        public string Name { get; set; }
    }
}
