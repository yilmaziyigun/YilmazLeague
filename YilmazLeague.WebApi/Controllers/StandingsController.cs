using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YilmazLeague.DataAccessLayer.Context;

namespace YilmazLeague.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly YilmazLeagueDB _context;

        public StandingsController(YilmazLeagueDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStandings()
        {
            var activeSeasonId = await _context.Seasons
                .Where(x => x.IsActive)
                .Select(x => x.SeasonId)
                .FirstOrDefaultAsync();

            if (activeSeasonId == 0)
                return BadRequest("Aktif sezon bulunamadı.");

            return await GetStandingsBySeason(activeSeasonId);
        }

        [HttpGet("season/{seasonId}")]
        public async Task<IActionResult> GetStandingsBySeason(int seasonId)
        {
            var teams = await _context.Teams.ToListAsync();

            var completedMatches = await _context.Matches
                .Where(x => x.SeasonId == seasonId
                    && x.Status == "Tamamlandı"
                    && x.HomeScore != null
                    && x.AwayScore != null)
                .ToListAsync();

            var standings = teams.Select(team =>
            {
                var teamMatches = completedMatches
                    .Where(match => match.HomeTeamId == team.TeamId || match.AwayTeamId == team.TeamId)
                    .ToList();

                int played = teamMatches.Count;
                int wins = 0;
                int draws = 0;
                int losses = 0;
                int goalsFor = 0;
                int goalsAgainst = 0;

                foreach (var match in teamMatches)
                {
                    bool isHomeTeam = match.HomeTeamId == team.TeamId;

                    int teamScore = isHomeTeam ? match.HomeScore.Value : match.AwayScore.Value;
                    int opponentScore = isHomeTeam ? match.AwayScore.Value : match.HomeScore.Value;

                    goalsFor += teamScore;
                    goalsAgainst += opponentScore;

                    if (teamScore > opponentScore)
                        wins++;
                    else if (teamScore == opponentScore)
                        draws++;
                    else
                        losses++;
                }

                return new
                {
                    TeamId = team.TeamId,
                    TeamName = team.Name,
                    team.LogoUrl,
                    Played = played,
                    Wins = wins,
                    Draws = draws,
                    Losses = losses,
                    GoalsFor = goalsFor,
                    GoalsAgainst = goalsAgainst,
                    GoalDifference = goalsFor - goalsAgainst,
                    Points = wins * 3 + draws
                };
            })
            .OrderByDescending(x => x.Points)
            .ThenByDescending(x => x.GoalDifference)
            .ThenByDescending(x => x.GoalsFor)
            .ToList();

            return Ok(standings);
        }
    }
}
