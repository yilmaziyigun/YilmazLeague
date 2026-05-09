namespace YilmazLeague.WebUI.Dtos
{
    public class DashboardStatsDto
    {
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public int CompletedMatches { get; set; }
        public int PlannedMatches { get; set; }
        public int LiveMatches { get; set; }
        public LeaderDto TopScorer { get; set; }
        public LeaderDto MostCardedPlayer { get; set; }
        public LeaderDto TopScoringTeam { get; set; }
        public LeaderDto MostConcededTeam { get; set; }
        public LeaderDto WorstFormTeam { get; set; }
        public List<MatchDto> NextWeekMatches { get; set; } = new List<MatchDto>();
    }

    public class LeaderDto
    {
        public string Name { get; set; }
        public string Secondary { get; set; }
        public string LogoUrl { get; set; }
        public int Value { get; set; }
        public string Unit { get; set; }
    }
}

