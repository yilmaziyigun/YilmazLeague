using System.Net.Http.Json;
using YilmazLeague.WebUI.Dtos;

namespace YilmazLeague.WebUI.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<DashboardStatsDto?> GetDashboardAsync(int? seasonId = null)
        {
            var url = seasonId.HasValue ? $"Statistics/dashboard?seasonId={seasonId.Value}" : "Statistics/dashboard";
            return await _httpClient.GetFromJsonAsync<DashboardStatsDto>(url);
        }

        public async Task<List<MatchDto>> GetLiveMatchesAsync()
        {
            var matches = await GetMatchesAsync();
            return matches
                .Where(x => x.Status == "Canlı")
                .OrderBy(x => x.MatchDate)
                .ToList();
        }
        public async Task<List<SeasonDto>> GetSeasonsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SeasonDto>>("Seasons")
                   ?? new List<SeasonDto>();
        }
        public async Task<List<MatchDto>> GetMatchesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MatchDto>>("Matches")
                   ?? new List<MatchDto>();
        }

        public async Task<List<MatchDto>> GetMatchesBySeasonWeekAsync(int seasonId, int weekNumber)
        {
            return await _httpClient.GetFromJsonAsync<List<MatchDto>>($"Matches/season/{seasonId}/week/{weekNumber}")
                   ?? new List<MatchDto>();
        }

        public async Task<MatchDto?> GetMatchByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<MatchDto>($"Matches/{id}");
        }

        public async Task<List<MatchEventDto>> GetEventsByMatchIdAsync(int matchId)
        {
            return await _httpClient.GetFromJsonAsync<List<MatchEventDto>>($"Events/{matchId}")
                   ?? new List<MatchEventDto>();
        }

        public async Task<List<StandingDto>> GetStandingsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StandingDto>>("Standings")
                   ?? new List<StandingDto>();
        }

        public async Task<List<TeamDto>> GetTeamsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TeamDto>>("Teams")
                   ?? new List<TeamDto>();
        }

        public async Task<List<PlayerDto>> GetPlayersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PlayerDto>>("Players")
                   ?? new List<PlayerDto>();
        }

        public async Task CreateTeamAsync(CreateTeamDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Teams", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreatePlayerAsync(CreatePlayerDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Players", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateMatchAsync(CreateMatchDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Matches", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateMatchScoreAsync(int matchId, UpdateMatchScoreDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"Matches/{matchId}/score", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateMatchEventAsync(CreateMatchEventDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Events", dto);
            response.EnsureSuccessStatusCode();
        }
    }
}


