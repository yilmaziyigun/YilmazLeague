namespace YilmazLeague.WebUI.Dtos
{
    public class MatchEventDto
    {
            public int MatchEventId { get; set; }
            public int LeagueMatchId { get; set; }
            public int Minute { get; set; }
            public string EventType { get; set; }
            public string Description { get; set; }
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public int? PlayerId { get; set; }
            public string PlayerName { get; set; }
            public int? PlayerInId { get; set; }
            public string PlayerInName { get; set; }
            public int? PlayerOutId { get; set; }
            public string PlayerOutName { get; set; }
            public DateTime CreatedDate { get; set; }
        
    }
}
