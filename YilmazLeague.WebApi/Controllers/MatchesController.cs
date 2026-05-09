using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YilmazLeague.DataAccessLayer.Context;
using YilmazLeague.EntityLayer.Entities;
using YilmazLeague.WebApi.Dtos.Match;
using YilmazLeague.WebApi.Dtos.MatchScore;

namespace YilmazLeague.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly YilmazLeagueDB _context;

        public MatchesController(YilmazLeagueDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matches = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .OrderByDescending(x => x.SeasonId)
                .ThenByDescending(x => x.WeekNumber)
                .ThenBy(x => x.MatchDate)
                .Select(x => new
                {
                    x.LeagueMatchId,
                    x.SeasonId,
                    x.WeekNumber,
                    x.MatchDate,
                    x.HomeScore,
                    x.AwayScore,
                    x.Status,
                    x.LiveMinute,
                    HomeTeamId = x.HomeTeamId,
                    HomeTeamName = x.HomeTeam.Name,
                    HomeTeamLogo = x.HomeTeam.LogoUrl,
                    AwayTeamId = x.AwayTeamId,
                    AwayTeamName = x.AwayTeam.Name,
                    AwayTeamLogo = x.AwayTeam.LogoUrl
                })
                .ToListAsync();

            return Ok(matches);
        }

        [HttpGet("season/{seasonId}/week/{weekNumber}")]
        public async Task<IActionResult> GetBySeasonAndWeek(int seasonId, int weekNumber)
        {
            var matches = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Where(x => x.SeasonId == seasonId && x.WeekNumber == weekNumber)
                .OrderBy(x => x.MatchDate)
                .Select(x => new
                {
                    x.LeagueMatchId,
                    x.SeasonId,
                    x.WeekNumber,
                    x.MatchDate,
                    x.HomeScore,
                    x.AwayScore,
                    x.Status,
                    x.LiveMinute,
                    HomeTeamId = x.HomeTeamId,
                    HomeTeamName = x.HomeTeam.Name,
                    HomeTeamLogo = x.HomeTeam.LogoUrl,
                    AwayTeamId = x.AwayTeamId,
                    AwayTeamName = x.AwayTeam.Name,
                    AwayTeamLogo = x.AwayTeam.LogoUrl
                })
                .ToListAsync();

            return Ok(matches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var match = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Where(x => x.LeagueMatchId == id)
                .Select(x => new
                {
                    x.LeagueMatchId,
                    x.SeasonId,
                    x.WeekNumber,
                    x.MatchDate,
                    x.HomeScore,
                    x.AwayScore,
                    x.Status,
                    x.LiveMinute,
                    HomeTeamId = x.HomeTeamId,
                    HomeTeamName = x.HomeTeam.Name,
                    HomeTeamLogo = x.HomeTeam.LogoUrl,
                    AwayTeamId = x.AwayTeamId,
                    AwayTeamName = x.AwayTeam.Name,
                    AwayTeamLogo = x.AwayTeam.LogoUrl
                })
                .FirstOrDefaultAsync();

            if (match == null)
                return NotFound("Maç bulunamadı.");

            return Ok(match);
        }

        [HttpPut("{id}/score")]
        public async Task<IActionResult> UpdateScore(int id, UpdateMatchScoreDto dto)
        {
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
                return NotFound("Maç bulunamadı.");

            match.HomeScore = dto.HomeScore;
            match.AwayScore = dto.AwayScore;
            match.Status = "Tamamlandı";
            match.LiveMinute = null;

            await _context.SaveChangesAsync();

            return Ok("Maç skoru güncellendi.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch(CreateMatchDto dto)
        {
            if (dto.HomeTeamId == dto.AwayTeamId)
                return BadRequest("Bir takım kendisiyle maç yapamaz.");

            if (dto.WeekNumber < 1 || dto.WeekNumber > 38)
                return BadRequest("Hafta numarası 1 ile 38 arasında olmalıdır.");

            var homeTeamExists = await _context.Teams.AnyAsync(x => x.TeamId == dto.HomeTeamId);
            var awayTeamExists = await _context.Teams.AnyAsync(x => x.TeamId == dto.AwayTeamId);
            var seasonExists = await _context.Seasons.AnyAsync(x => x.SeasonId == dto.SeasonId);

            if (!homeTeamExists || !awayTeamExists)
                return BadRequest("Ev sahibi veya deplasman takımı bulunamadı.");

            if (!seasonExists)
                return BadRequest("Sezon bulunamadı.");

            var match = new LeagueMatch
            {
                HomeTeamId = dto.HomeTeamId,
                AwayTeamId = dto.AwayTeamId,
                MatchDate = dto.MatchDate,
                WeekNumber = dto.WeekNumber,
                SeasonId = dto.SeasonId,
                Status = "Planlandı"
            };

            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();

            return Ok("Maç başarıyla oluşturuldu.");
        }
    }
}

