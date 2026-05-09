using Microsoft.AspNetCore.Mvc;
using YilmazLeague.WebUI.Services;

namespace YilmazLeague.WebUI.Controllers
{
    public class LiveController : Controller
    {
        private readonly ApiService _apiService;

        public LiveController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var liveMatches = await _apiService.GetLiveMatchesAsync();
            return View(liveMatches);
        }
    }
}