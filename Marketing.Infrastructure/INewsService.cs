using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketing.Domain;

namespace Marketing.Infrastructure
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetNewsAsync();
    }

    public class NewsService
    {
        public Task<IEnumerable<News>> GetNewsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
