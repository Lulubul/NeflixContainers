using History.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace History.Infrastructure
{
    public interface IHistoryRepository
    {
        Task<bool> AddAsync(HistoryEntity historyEntity);
        Task<List<HistoryEntity>> GetAll(string userId, string profileId);
    }

    public class HistoryRepository : IHistoryRepository
    {
        private readonly HistoryContext _historyContext;
        private const string TableName = "history";

        public HistoryRepository(HistoryContext context)
        {
            _historyContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddAsync(HistoryEntity historyEntity)
        {
            _historyContext.History.Add(historyEntity);
            return await _historyContext.SaveChangesAsync() > 0;
        }

        public async Task<HistoryEntity> GetByUserIdAndMovieId(string userId, string movieId)
        {
            return await _historyContext
                .History
                .FirstOrDefaultAsync(x => x.UserId == userId && x.WatchingItemId == movieId);
        }

        public async Task<List<HistoryEntity>> GetAll(string userId, string profileId)
        {
            return await _historyContext
                .History
                .Where(x => x.UserId == userId && x.ProfileId == profileId)
                .ToListAsync();
        }
    }
}