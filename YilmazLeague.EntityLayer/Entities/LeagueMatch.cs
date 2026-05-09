using System;
using System.Collections.Generic;

namespace YilmazLeague.EntityLayer.Entities
{
    public class LeagueMatch
    {
        public int LeagueMatchId { get; set; }

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public DateTime MatchDate { get; set; }
        public int WeekNumber { get; set; }

        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public string Status { get; set; }
        public int? LiveMinute { get; set; }

        public int SeasonId { get; set; } = 4;
        public Season Season { get; set; }

        public List<MatchEvent> MatchEvents { get; set; } = new List<MatchEvent>();
    }
}

