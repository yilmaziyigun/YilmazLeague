namespace YilmazLeague.WebApi.Dtos.MatchEvent
{
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
