using Microsoft.AspNetCore.Mvc;
using YilmazLeague.WebUI.Services;
using YilmazLeague.WebUI.ViewModels;

namespace YilmazLeague.WebUI.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApiService _apiService;

        public MatchesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(int? seasonId)
        {
            var seasons = await _apiService.GetSeasonsAsync();
            var selectedSeasonId = seasonId
                ?? seasons.FirstOrDefault(x => x.IsActive)?.SeasonId
                ?? seasons.FirstOrDefault()?.SeasonId;

            var dashboard = await _apiService.GetDashboardAsync(selectedSeasonId);

            ViewBag.Seasons = seasons;
            ViewBag.SelectedSeasonId = selectedSeasonId ?? dashboard?.SeasonId ?? 4;

            return View(dashboard);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var match = await _apiService.GetMatchByIdAsync(id);

            if (match == null)
                return NotFound("Maç bulunamadı.");

            var events = await _apiService.GetEventsByMatchIdAsync(id);

            var model = new MatchDetailViewModel
            {
                Match = match,
                Events = events
            };

            return View(model);
        }
    }
}