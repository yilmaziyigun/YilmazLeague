using YilmazLeague.WebUI.Dtos;

namespace YilmazLeague.WebUI.ViewModels
{
    public class MatchDetailViewModel
    {
        public MatchDto Match { get; set; }
        public List<MatchEventDto> Events { get; set; } = new List<MatchEventDto>();
    }
}
