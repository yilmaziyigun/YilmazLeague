using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YilmazLeague.DataAccessLayer.Context;

namespace YilmazLeague.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly YilmazLeagueDB _context;

        public SeasonsController(YilmazLeagueDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var seasons = await _context.Seasons
                .OrderByDescending(x => x.StartYear)
                .Select(x => new
                {
                    x.SeasonId,
                    x.Name,
                    x.StartYear,
                    x.EndYear,
                    x.IsActive
                })
                .ToListAsync();

            return Ok(seasons);
        }
    }
}
