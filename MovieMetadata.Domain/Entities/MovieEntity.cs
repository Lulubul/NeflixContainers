using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

namespace MovieMetadata.Infrastructure.Entities
{
    public class MovieEntity: TableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Image { get; set; }
    }
}
