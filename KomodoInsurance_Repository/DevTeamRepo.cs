using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance_Repository
{
    public class DevTeamRepo
    {
        // Make a list of teams
        private List<DevTeam> _devTeam = new List<DevTeam>();

        // Create = ADD TEAM TO LIST
        public void AddTeamToList(DevTeam devTeam)
        {
            _devTeam.Add(devTeam);
        }

        // Read the list of teams
        public List<DevTeam> ViewTeams()
        {
            return _devTeam;
        }

        // Update a team
        public bool UpdateListOfTeams(int originalTeamId, DevTeam newDevTeam)
        {
            // Find team
            DevTeam oldDevTeam = GetTeamById(originalTeamId);

            // Update team
            if(oldDevTeam != null)
            {
                oldDevTeam.TeamId = newDevTeam.TeamId;
                oldDevTeam.TeamName = newDevTeam.TeamName;
                oldDevTeam.Members = newDevTeam.Members;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete a team from the list
        public bool RemoveTeamFromList(int teamId)
        {
            DevTeam devTeam = GetTeamById(teamId);

            if(devTeam == null)
            {
                return false;
            }

            int initialTeamCount = _devTeam.Count;
            _devTeam.Remove(devTeam);

            if(initialTeamCount > _devTeam.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Helper method
        public DevTeam GetTeamById(int teamId)
        {
            foreach(DevTeam devTeam in _devTeam)
            {
                if(devTeam.TeamId == teamId)
                {
                    return devTeam;
                }
            }
            
            return null;
        }
    }
}
