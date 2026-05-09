namespace YilmazLeague.EntityLayer.Entities
{
    public class MatchEvent
    {
        public int MatchEventId { get; set; }

        public int LeagueMatchId { get; set; }
        public LeagueMatch LeagueMatch { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int? PlayerId { get; set; }
        public Player? Player { get; set; }

        public int Minute { get; set; }

        public string EventType { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int? PlayerInId { get; set; }
        public Player? PlayerIn { get; set; }

        public int? PlayerOutId { get; set; }
        public Player? PlayerOut { get; set; }
    }
}
