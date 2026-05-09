using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YilmazLeague.EntityLayer.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
        public List<MatchEvent> PlayerInEvents { get; set; } = new List<MatchEvent>();
        public List<MatchEvent> PlayerOutEvents { get; set; } = new List<MatchEvent>();

        public List<MatchEvent> MatchEvents { get; set; } = new List<MatchEvent>();
    }
}
