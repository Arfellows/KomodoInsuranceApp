using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance_Repository
{       //POCO
    public class Developer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdNumber { get; set; }
        public bool HasAccess { get; set; }
        public bool IsManager { get; set; }
        public string NameOfTeam { get; set; }

        public Developer() { }

        public Developer(string firstName, string lastName, int idNumber, bool hasAccess, bool isManager, string nameOfTeam)
        {
            FirstName = firstName;
            LastName = lastName;
            IdNumber = idNumber;
            HasAccess = hasAccess;
            IsManager = isManager;
            NameOfTeam = nameOfTeam;
        }
    }
}
