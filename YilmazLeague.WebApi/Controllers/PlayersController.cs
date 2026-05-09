using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YilmazLeague.DataAccessLayer.Context;
using YilmazLeague.EntityLayer.Entities;
using YilmazLeague.WebApi.Dtos;
using YilmazLeague.WebApi.Dtos.Player;

namespace YilmazLeague.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly YilmazLeagueDB _context;

        public PlayersController(YilmazLeagueDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _context.Players
                .Include(x => x.Team)
                .Select(x => new
                {
                    x.PlayerId,
                    x.Name,
                    x.TeamId,
                    TeamName = x.Team.Name
                })
                .ToListAsync();

            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(int id)
        {
            var player = await _context.Players
                .Include(x => x.Team)
                .Where(x => x.PlayerId == id)
                .Select(x => new
                {
                    x.PlayerId,
                    x.Name,
                    x.TeamId,
                    TeamName = x.Team.Name
                })
                .FirstOrDefaultAsync();

            if (player == null)
                return NotFound("Oyuncu bulunamadı.");

            return Ok(player);
        }

        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetPlayersByTeam(int teamId)
        {
            var players = await _context.Players
                .Where(x => x.TeamId == teamId)
                .Select(x => new
                {
                    x.PlayerId,
                    x.Name,
                    x.TeamId
                })
                .ToListAsync();

            return Ok(players);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer(CreatePlayerDto dto)
        {
            var teamExists = await _context.Teams.AnyAsync(x => x.TeamId == dto.TeamId);

            if (!teamExists)
                return BadRequest("Böyle bir takım yok.");

            var player = new Player
            {
                Name = dto.Name,
                TeamId = dto.TeamId
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            return Ok("Oyuncu başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, UpdatePlayerDto dto)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
                return NotFound("Oyuncu bulunamadı.");

            var teamExists = await _context.Teams.AnyAsync(x => x.TeamId == dto.TeamId);

            if (!teamExists)
                return BadRequest("Böyle bir takım yok.");

            player.Name = dto.Name;
            player.TeamId = dto.TeamId;

            await _context.SaveChangesAsync();

            return Ok("Oyuncu başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
                return NotFound("Oyuncu bulunamadı.");

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return Ok("Oyuncu başarıyla silindi.");
        }
    }
}
