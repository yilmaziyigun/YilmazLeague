using Microsoft.AspNetCore.Mvc;
using YilmazLeague.WebUI.Dtos;
using YilmazLeague.WebUI.Services;
using YilmazLeague.WebUI.ViewModels;

namespace YilmazLeague.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private const string AdminUsername = "admin";
        private const string AdminPassword = "1234";
        private readonly ApiService _apiService;

        public AdminController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (IsAdminLoggedIn())
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                TempData["Success"] = "Admin girişi başarılı.";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Kullanıcı adı veya şifre hatalı.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Index", "Matches");
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var model = new AdminDashboardViewModel
            {
                Teams = await _apiService.GetTeamsAsync(),
                Players = await _apiService.GetPlayersAsync(),
                Matches = await _apiService.GetMatchesAsync(),
                Seasons = await _apiService.GetSeasonsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch(CreateMatchDto dto)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            try
            {
                await _apiService.CreateMatchAsync(dto);
                TempData["Success"] = "Maç oluşturuldu.";
            }
            catch
            {
                TempData["Error"] = "Maç oluşturulamadı. Takımlar, sezon ve hafta bilgisini kontrol et.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateScore(int matchId, UpdateMatchScoreDto dto)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            try
            {
                await _apiService.UpdateMatchScoreAsync(matchId, dto);
                TempData["Success"] = "Maç skoru güncellendi ve maç tamamlandı.";
            }
            catch
            {
                TempData["Error"] = "Skor güncellenemedi. Maç seçimini kontrol et.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateMatchEventDto dto)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            try
            {
                await _apiService.CreateMatchEventAsync(dto);
                TempData["Success"] = "Maç olayı eklendi.";
            }
            catch
            {
                TempData["Error"] = "Maç olayı eklenemedi. Oyuncunun seçilen takıma ait olduğundan emin ol.";
            }

            return RedirectToAction("Index");
        }
    }
}