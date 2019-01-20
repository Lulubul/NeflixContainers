using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Subscription.API.Application;
using Subscription.API.Application.Model;

namespace Subscription.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController
    {
        private readonly IPlanQueries _planQueries;

        public PlansController(IPlanQueries planQueries)
        {
            _planQueries = planQueries;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Plan>), 200)]
        public async Task<IEnumerable<Plan>> GetPlans()
        {
            return await _planQueries.GetAllPlans();
        }
    }
}
