using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YilmazLeague.EntityLayer.Entities
{
    public class PlayerSeasonStatistic
    {
        public int PlayerSeasonStatisticId { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int Goals { get; set; }
        public int Assists { get; set; }
    }

}
