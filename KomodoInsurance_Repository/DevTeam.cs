using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance_Repository
{       //POCO
    public class DevTeam
    {
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public int Members { get; set; }

        public DevTeam() { }
        public DevTeam(string teamName, int teamId, int members)
        {
            TeamName = teamName;
            TeamId = teamId;
            Members = members;
        }
    }
}
