using Microsoft.AspNetCore.Mvc;
using YilmazLeague.WebUI.Services;

namespace YilmazLeague.WebUI.Controllers
{
    public class StandingsController : Controller
    {
        private readonly ApiService _apiService;

        public StandingsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var standings = await _apiService.GetStandingsAsync();
            return View(standings);
        }
    }
}
