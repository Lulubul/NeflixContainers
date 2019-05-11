using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace History.API.Controllers
{
    public interface IRecommendationsService
    {
        Task<List<Movie>> GetVideoRecommendationsByUser(Guid userId, Guid profileId);
    }
}