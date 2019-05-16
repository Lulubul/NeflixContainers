using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketing.Domain;

namespace Marketing.Infrastructure
{
    public class NewsRepository: INewsRepository
    {
        public Task<IEnumerable<News>> GetNewsAsync(string userId, string profileId)
        {
            throw new NotImplementedException();
        }
    }
}