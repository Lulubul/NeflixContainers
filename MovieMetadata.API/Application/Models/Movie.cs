using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMetadata.API.Application.Models
{
    public class Movie
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Image { get; set; }
    }
}
