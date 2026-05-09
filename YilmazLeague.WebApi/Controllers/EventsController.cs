using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YilmazLeague.DataAccessLayer.Context;
using YilmazLeague.EntityLayer.Entities;
using YilmazLeague.WebApi.Dtos.MatchEvent;

namespace YilmazLeague.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly YilmazLeagueDB _context;

        public EventsController(YilmazLeagueDB context)
        {
            _context = context;
        }

        [HttpGet("{matchId}")]
        public async Task<IActionResult> GetEvents(int matchId)
        {
            var events = await _context.MatchEvents
                .Include(x => x.Team)
                .Include(x => x.Player)
                .Include(x => x.PlayerIn)
                .Include(x => x.PlayerOut)
                .Where(x => x.LeagueMatchId == matchId)
                .OrderBy(x => x.Minute)
                .Select(x => new
                {
                    x.MatchEventId,
                    x.LeagueMatchId,
                    x.Minute,
                    x.EventType,
                    x.Description,
                    TeamId = x.TeamId,
                    TeamName = x.Team.Name,
                    PlayerId = x.PlayerId,
                    PlayerName = x.Player != null ? x.Player.Name : null,
                    PlayerInId = x.PlayerInId,
                    PlayerInName = x.PlayerIn != null ? x.PlayerIn.Name : null,
                    PlayerOutId = x.PlayerOutId,
                    PlayerOutName = x.PlayerOut != null ? x.PlayerOut.Name : null,
                    x.CreatedDate
                })
                .ToListAsync();

            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(CreateMatchEventDto dto)
        {
            var match = await _context.Matches
                .FirstOrDefaultAsync(x => x.LeagueMatchId == dto.LeagueMatchId);

            if (match == null)
                return BadRequest("Maç bulunamadı.");

            var teamExists = await _context.Teams
                .AnyAsync(x => x.TeamId == dto.TeamId);

            if (!teamExists)
                return BadRequest("Takım bulunamadı.");

            if (dto.PlayerId != null)
            {
                var playerExists = await _context.Players
                    .AnyAsync(x => x.PlayerId == dto.PlayerId && x.TeamId == dto.TeamId);

                if (!playerExists)
                    return BadRequest("Seçilen oyuncu bu takıma ait değil.");
            }

            if (dto.PlayerInId != null)
            {
                var playerInExists = await _context.Players
                    .AnyAsync(x => x.PlayerId == dto.PlayerInId && x.TeamId == dto.TeamId);

                if (!playerInExists)
                    return BadRequest("Giren oyuncu bu takıma ait değil.");
            }

            if (dto.PlayerOutId != null)
            {
                var playerOutExists = await _context.Players
                    .AnyAsync(x => x.PlayerId == dto.PlayerOutId && x.TeamId == dto.TeamId);

                if (!playerOutExists)
                    return BadRequest("Çıkan oyuncu bu takıma ait değil.");
            }

            if (dto.EventType == "Oyuncu Değişikliği" && (dto.PlayerInId == null || dto.PlayerOutId == null))
                return BadRequest("Oyuncu değişikliği için giren ve çıkan oyuncu seçilmelidir.");

            var matchEvent = new MatchEvent
            {
                LeagueMatchId = dto.LeagueMatchId,
                TeamId = dto.TeamId,
                PlayerId = dto.EventType == "Oyuncu Değişikliği" ? null : dto.PlayerId,
                Minute = dto.Minute,
                EventType = dto.EventType,
                Description = string.IsNullOrWhiteSpace(dto.Description) ? dto.EventType : dto.Description,
                PlayerInId = dto.PlayerInId,
                PlayerOutId = dto.PlayerOutId
            };

            await _context.MatchEvents.AddAsync(matchEvent);

            if (dto.EventType == "Gol")
            {
                match.HomeScore ??= 0;
                match.AwayScore ??= 0;

                if (dto.TeamId == match.HomeTeamId)
                    match.HomeScore++;
                else if (dto.TeamId == match.AwayTeamId)
                    match.AwayScore++;
            }

            await _context.SaveChangesAsync();

            return Ok("Maç olayı başarıyla eklendi.");
        }
    }
}
