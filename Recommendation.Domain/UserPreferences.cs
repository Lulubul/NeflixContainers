using System;
using System.Collections.Generic;
using System.Linq;

namespace Recommendation.Domain
{
    public class UserPreferences
    {
        public IEnumerable<KeyValuePair<string, List<Movie>>> MoviesByGenre { get; set; }
        public IEnumerable<KeyValuePair<string, List<Movie>>> MoviesByReleaseYear { get; set; }

        public UserPreferences(Dictionary<string, List<Movie>> moviesByGenre, Dictionary<string, List<Movie>> moviesByReleaseYear)
        {
            MoviesByGenre = GetTop(moviesByGenre);
            MoviesByReleaseYear = GetTop(moviesByReleaseYear);
        }

        private IEnumerable<KeyValuePair<string, List<Movie>>> GetTop(Dictionary<string, List<Movie>> keyValues)
        {
            return keyValues.ToArray().OrderByDescending(x => x.Value.Count);
        }
    }
}
