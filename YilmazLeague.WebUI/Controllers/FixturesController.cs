using Microsoft.AspNetCore.Mvc;
using YilmazLeague.WebUI.Services;

namespace YilmazLeague.WebUI.Controllers
{
    public class FixturesController : Controller
    {
        private readonly ApiService _apiService;

        public FixturesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(int? seasonId, int? week)
        {
            var seasons = await _apiService.GetSeasonsAsync();
            var matches = await _apiService.GetMatchesAsync();

            var selectedSeasonId = seasonId
                ?? seasons.FirstOrDefault(x => x.IsActive)?.SeasonId
                ?? seasons.FirstOrDefault()?.SeasonId
                ?? 4;

            var seasonMatches = matches
                .Where(x => x.SeasonId == selectedSeasonId)
                .ToList();

            var weekNumbers = seasonMatches
                .Select(x => x.WeekNumber)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            if (!weekNumbers.Any())
                weekNumbers = Enumerable.Range(1, 38).ToList();

            var selectedWeek = week
                ?? seasonMatches
                    .Where(x => x.Status == "Tamamlandı")
                    .Select(x => x.WeekNumber)
                    .DefaultIfEmpty(weekNumbers.First())
                    .Max();

            if (!weekNumbers.Contains(selectedWeek))
                selectedWeek = weekNumbers.First();

            var selectedIndex = weekNumbers.IndexOf(selectedWeek);
            var previousWeek = selectedIndex > 0 ? weekNumbers[selectedIndex - 1] : (int?)null;
            var nextWeek = selectedIndex < weekNumbers.Count - 1 ? weekNumbers[selectedIndex + 1] : (int?)null;

            var fixtures = seasonMatches
                .Where(x => x.WeekNumber == selectedWeek)
                .OrderBy(x => x.MatchDate)
                .ToList();

            ViewBag.Seasons = seasons;
            ViewBag.SelectedSeasonId = selectedSeasonId;
            ViewBag.SelectedWeek = selectedWeek;
            ViewBag.WeekNumbers = weekNumbers;
            ViewBag.PreviousWeek = previousWeek;
            ViewBag.NextWeek = nextWeek;

            return View(fixtures);
        }
    }
}
