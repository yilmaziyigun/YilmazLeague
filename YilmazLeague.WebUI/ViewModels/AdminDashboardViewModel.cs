using YilmazLeague.WebUI.Dtos;

namespace YilmazLeague.WebUI.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<TeamDto> Teams { get; set; } = new List<TeamDto>();
        public List<PlayerDto> Players { get; set; } = new List<PlayerDto>();
        public List<MatchDto> Matches { get; set; } = new List<MatchDto>();
        public List<SeasonDto> Seasons { get; set; } = new List<SeasonDto>();
    }
}