using System.Collections.Generic;
using System.Threading.Tasks;
using Marketing.Domain;

namespace Marketing.Infrastructure
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetNewsAsync(string userId, string profileId);
    }
}
