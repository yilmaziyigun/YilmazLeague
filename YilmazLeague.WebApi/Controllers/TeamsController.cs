using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YilmazLeague.DataAccessLayer.Context;
using YilmazLeague.EntityLayer.Entities;
using YilmazLeague.WebApi.Dtos;

namespace YilmazLeague.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly YilmazLeagueDB _context;

        public TeamsController(YilmazLeagueDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _context.Teams
                .Select(x => new
                {
                    x.TeamId,
                    x.Name,
                    x.LogoUrl,
                    PlayerCount = x.Players.Count
                })
                .ToListAsync();

            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var team = await _context.Teams
                .Include(x => x.Players)
                .FirstOrDefaultAsync(x => x.TeamId == id);

            if (team == null)
                return NotFound("Takım bulunamadı.");

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam(CreateTeamDto dto)
        {
            var team = new Team
            {
                Name = dto.Name,
                LogoUrl = dto.LogoUrl
            };

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            return Ok("Takım başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, UpdateTeamDto dto)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
                return NotFound("Takım bulunamadı.");

            team.Name = dto.Name;
            team.LogoUrl = dto.LogoUrl;

            await _context.SaveChangesAsync();

            return Ok("Takım başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
                return NotFound("Takım bulunamadı.");

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return Ok("Takım başarıyla silindi.");
        }
    }
}
