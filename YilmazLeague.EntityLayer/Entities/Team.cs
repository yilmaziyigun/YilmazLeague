using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YilmazLeague.EntityLayer.Entities
{
    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();

        public List<LeagueMatch> HomeMatches { get; set; } = new List<LeagueMatch>();
        public List<LeagueMatch> AwayMatches { get; set; } = new List<LeagueMatch>();
    }
}
