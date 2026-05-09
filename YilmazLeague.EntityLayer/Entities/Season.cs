using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YilmazLeague.EntityLayer.Entities
{
    public class Season
    {
        public int SeasonId { get; set; }
        public string Name { get; set; } // 2024-2025
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public bool IsActive { get; set; }

        public List<LeagueMatch> Matches { get; set; } = new List<LeagueMatch>();
    }

}
