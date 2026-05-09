namespace YilmazLeague.WebUI.Dtos
{
    public class CreateTeamDto
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
    }

    public class CreatePlayerDto
    {
        public string Name { get; set; }
        public int TeamId { get; set; }
    }

    public class CreateMatchDto
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public int WeekNumber { get; set; }
        public int SeasonId { get; set; } = 4;
    }

    public class UpdateMatchScoreDto
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }

    public class CreateMatchEventDto
    {
        public int LeagueMatchId { get; set; }
        public int TeamId { get; set; }
        public int? PlayerId { get; set; }
        public int Minute { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
        public int? PlayerInId { get; set; }
        public int? PlayerOutId { get; set; }
    }
}

