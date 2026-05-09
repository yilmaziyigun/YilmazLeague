namespace YilmazLeague.WebUI.Dtos
{
    public class MatchDto
    {
        public int LeagueMatchId { get; set; }
        public int SeasonId { get; set; }
        public int WeekNumber { get; set; }
        public DateTime MatchDate { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public string Status { get; set; }
        public int? LiveMinute { get; set; }

        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamLogo { get; set; }

        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamLogo { get; set; }
    }
}

