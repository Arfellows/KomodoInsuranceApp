using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance_Repository
{
    public class DeveloperRepo
    {
        // Make a list of developers
        private List<Developer> _developer = new List<Developer>();

        //Create a developer
        public void AddDeveloperToList(Developer developer)
        {
            _developer.Add(developer);
        }

        //Read the list of developers
        public List<Developer> ViewDevelopers()
        {
            return _developer;
        }

        //Update a developer
        public bool UpdateListOfDevelopers(int originalId, Developer newDeveloper)
        {
            // Find developer
            Developer oldDeveloper = GetDeveloperById(originalId);

            // Update developer
            if(oldDeveloper != null)
            {
                oldDeveloper.IdNumber = newDeveloper.IdNumber;
                oldDeveloper.FirstName = newDeveloper.FirstName;
                oldDeveloper.LastName = newDeveloper.LastName;
                oldDeveloper.HasAccess = newDeveloper.HasAccess;
                oldDeveloper.IsManager = newDeveloper.IsManager;
                oldDeveloper.NameOfTeam = newDeveloper.NameOfTeam;
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //Delete a developer from the list
       public bool RemoveDeveloperFromList(int id)
        {
            Developer developer = GetDeveloperById(id);

            if(developer == null)
            {
                return false;
            }

            int initialCount = _developer.Count;
            _developer.Remove(developer);

            if(initialCount > _developer.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Helper method
        public Developer GetDeveloperById(int id)
        {            
            foreach(Developer developer in _developer)
            {
                if(developer.IdNumber == id)
                {
                    return developer;
                }
            }
            Console.WriteLine("No developer exists by that ID.");
            return null;
        }

    }
}
