using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YilmazLeague.DataAccessLayer.Context;

namespace YilmazLeague.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly YilmazLeagueDB _context;

        public StatisticsController(YilmazLeagueDB context)
        {
            _context = context;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard([FromQuery] int? seasonId)
        {
            var selectedSeasonId = seasonId ?? 4;

            var season = await _context.Seasons.FirstOrDefaultAsync(x => x.SeasonId == selectedSeasonId);
            if (season == null)
                return NotFound("Sezon bulunamadı.");

            var matches = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Where(x => x.SeasonId == selectedSeasonId)
                .OrderBy(x => x.WeekNumber)
                .ThenBy(x => x.MatchDate)
                .ToListAsync();

            var completedMatches = matches.Where(x => x.Status == "Tamamlandı").ToList();
            var activeWeek = matches
                .Where(x => x.Status == "Tamamlandı" || x.Status == "Canlı")
                .Select(x => x.WeekNumber)
                .DefaultIfEmpty(1)
                .Max();
            var nextWeek = matches.Any(x => x.WeekNumber == activeWeek + 1) ? activeWeek + 1 : activeWeek;

            var goalEvents = await _context.MatchEvents
                .Include(x => x.Player)
                    .ThenInclude(x => x.Team)
                .Include(x => x.LeagueMatch)
                .Where(x => x.LeagueMatch.SeasonId == selectedSeasonId && x.EventType == "Gol" && x.PlayerId != null)
                .ToListAsync();

            var cardEvents = await _context.MatchEvents
                .Include(x => x.Player)
                    .ThenInclude(x => x.Team)
                .Include(x => x.LeagueMatch)
                .Where(x => x.LeagueMatch.SeasonId == selectedSeasonId &&
                            (x.EventType == "Sarı Kart" || x.EventType == "Kırmızı Kart") &&
                            x.PlayerId != null)
                .ToListAsync();

            var topScorer = goalEvents
                .GroupBy(x => new
                {
                    x.PlayerId,
                    PlayerName = x.Player.Name,
                    TeamName = x.Player.Team.Name,
                    LogoUrl = x.Player.Team.LogoUrl
                })
                .Select(x => new
                {
                    Name = x.Key.PlayerName,
                    Secondary = x.Key.TeamName,
                    LogoUrl = x.Key.LogoUrl,
                    Value = x.Count(),
                    Unit = "gol"
                })
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Name)
                .FirstOrDefault();

            var mostCardedPlayer = cardEvents
                .GroupBy(x => new
                {
                    x.PlayerId,
                    PlayerName = x.Player.Name,
                    TeamName = x.Player.Team.Name,
                    LogoUrl = x.Player.Team.LogoUrl
                })
                .Select(x => new
                {
                    Name = x.Key.PlayerName,
                    Secondary = x.Key.TeamName,
                    LogoUrl = x.Key.LogoUrl,
                    Value = x.Count(),
                    Unit = "kart"
                })
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Name)
                .FirstOrDefault();

            var teamRows = completedMatches
                .SelectMany(match => new[]
                {
                    new
                    {
                        TeamId = match.HomeTeamId,
                        TeamName = match.HomeTeam.Name,
                        LogoUrl = match.HomeTeam.LogoUrl,
                        GoalsFor = match.HomeScore ?? 0,
                        GoalsAgainst = match.AwayScore ?? 0,
                        Loss = (match.HomeScore ?? 0) < (match.AwayScore ?? 0) ? 1 : 0
                    },
                    new
                    {
                        TeamId = match.AwayTeamId,
                        TeamName = match.AwayTeam.Name,
                        LogoUrl = match.AwayTeam.LogoUrl,
                        GoalsFor = match.AwayScore ?? 0,
                        GoalsAgainst = match.HomeScore ?? 0,
                        Loss = (match.AwayScore ?? 0) < (match.HomeScore ?? 0) ? 1 : 0
                    }
                })
                .GroupBy(x => new { x.TeamId, x.TeamName, x.LogoUrl })
                .Select(x => new
                {
                    x.Key.TeamName,
                    x.Key.LogoUrl,
                    Played = x.Count(),
                    GoalsFor = x.Sum(y => y.GoalsFor),
                    GoalsAgainst = x.Sum(y => y.GoalsAgainst),
                    Losses = x.Sum(y => y.Loss)
                })
                .ToList();

            var topScoringTeam = teamRows
                .OrderByDescending(x => x.GoalsFor)
                .Select(x => new
                {
                    Name = x.TeamName,
                    Secondary = $"{x.Played} maçta",
                    LogoUrl = x.LogoUrl,
                    Value = x.GoalsFor,
                    Unit = "gol"
                })
                .FirstOrDefault();

            var mostConcededTeam = teamRows
                .OrderByDescending(x => x.GoalsAgainst)
                .Select(x => new
                {
                    Name = x.TeamName,
                    Secondary = $"{x.Played} maçta",
                    LogoUrl = x.LogoUrl,
                    Value = x.GoalsAgainst,
                    Unit = "gol"
                })
                .FirstOrDefault();

            var worstFormTeam = teamRows
                .OrderByDescending(x => x.Losses)
                .ThenByDescending(x => x.GoalsAgainst)
                .Select(x => new
                {
                    Name = x.TeamName,
                    Secondary = $"{x.Played} maçta",
                    LogoUrl = x.LogoUrl,
                    Value = x.Losses,
                    Unit = "mağlubiyet"
                })
                .FirstOrDefault();

            var nextWeekMatches = matches
                .Where(x => x.WeekNumber == nextWeek)
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
                .ToList();

            return Ok(new
            {
                SeasonId = season.SeasonId,
                SeasonName = season.Name,
                CompletedMatches = matches.Count(x => x.Status == "Tamamlandı"),
                PlannedMatches = matches.Count(x => x.Status == "Planlandı"),
                LiveMatches = matches.Count(x => x.Status == "Canlı"),
                TopScorer = topScorer,
                MostCardedPlayer = mostCardedPlayer,
                TopScoringTeam = topScoringTeam,
                MostConcededTeam = mostConcededTeam,
                WorstFormTeam = worstFormTeam,
                NextWeekMatches = nextWeekMatches
            });
        }
    }
}