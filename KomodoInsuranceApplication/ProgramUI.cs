using KomodoInsurance_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsuranceApplication
{
    class ProgramUI
    {
        private DevTeamRepo _devTeamRepo = new DevTeamRepo();
        private DeveloperRepo _devRepo = new DeveloperRepo();
        //List<int> _hasAccess = new List<int>();
        public void Run()
        {
            SeedTeamList();
            SeedDeveloperList();
            TeamMenu();
        }

        // Menu for teams
        private void TeamMenu()
        {
            Console.Clear();

            bool keepRunning = true;

            while (keepRunning)
            {
                // Display options
                Console.WriteLine("TEAM DIRECTORY\n\n" +
                    "Welcome! What would you like to do?\n\n" +
                    "1. Create a new team\n" +
                    "2. View all teams\n" +
                    "3. Find team by ID number\n" +
                    "4. Update an existing team\n" +
                    "5. Remove a team\n" +
                    "6. Developer Directory\n" +
                    "7. EXIT");

                // Prompt for input
                string userInput = Console.ReadLine();

                // Evaluate input... what did they choose?
                switch (userInput)
                {
                    case "1":
                        //create
                        CreateNewTeam();
                        break;
                    case "2":
                        //View
                        DisplayTeams();
                        break;
                    case "3":
                        //view by id 
                        DisplayTeamById();
                        break;
                    case "4":
                        //update
                        UpdateTeam();
                        break;
                    case "5":
                        //remove/delete
                        DeleteTeam();
                        break;
                    case "6":
                        //Developer Directory
                        DeveloperDirectory();
                        break;
                    case "7":
                        Console.WriteLine("Goodbye!");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("You have entered an invalid number. Please try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        //create new team
        private void CreateNewTeam()
        {
            Console.Clear();

            DevTeam newDevTeam = new DevTeam();

            //team name
            Console.WriteLine("Enter a team name:");
            newDevTeam.TeamName = Console.ReadLine();

            //team id
            Console.WriteLine("Enter an ID number for the team:");
            string idNumber = Console.ReadLine();
            newDevTeam.TeamId = int.Parse(idNumber);

            //add developers
            Console.WriteLine("Would you like to add a developer to the team? (y/n):");
            string addDeveloper = Console.ReadLine().ToLower();

            if (addDeveloper == "y")
            {
                Console.Clear();
                CreateDeveloper();
            }
            else
            {
                Console.WriteLine("What would you like to do next? (1/2):\n\n" +
                    "1. Add another team\n" +
                    "2. Return to main menu");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        CreateNewTeam();
                        break;
                    case "2":
                        TeamMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }

            _devTeamRepo.AddTeamToList(newDevTeam);

        }

        //view teams
        private void DisplayTeams()
        {
            Console.Clear();

            List<DevTeam> listOfTeams = _devTeamRepo.ViewTeams();

            foreach (DevTeam devTeam in listOfTeams)
            {
                Console.WriteLine($"Team ID: {devTeam.TeamId}\n");
            }
        }

        //view team by id
        private void DisplayTeamById()
        {
            Console.Clear();

            //Ask user for ID
            Console.WriteLine("Enter the ID number for the team you would like to view:");

            //Get input
            string userInput = Console.ReadLine();
            int teamId = int.Parse(userInput);

            //Find team w/ that ID
            DevTeam devTeam = _devTeamRepo.GetTeamById(teamId);

            //Display team info
            if (devTeam != null)
            {
                Console.WriteLine($"Team Name: {devTeam.TeamName}\n" +
                    $"Team ID: {devTeam.TeamId}\n" +
                    $"Members: {devTeam.Members}\n\n\n");
            }
            else
            {
                Console.WriteLine("Could not find a team by that ID...");
            }
        }

        //update team
        private void UpdateTeam()
        {
            //display
            DisplayTeams();
            //prompt for id number
            Console.WriteLine("\nEnter the ID number for the team you would like to update:");
            //get input
            string input = Console.ReadLine();
            int teamId = int.Parse(input);
            //build new object
            DevTeam newDevTeam = new DevTeam();

            //team name
            Console.WriteLine("Enter a team name:");
            newDevTeam.TeamName = Console.ReadLine();

            //team id
            Console.WriteLine("Enter an ID number for the team:");
            string idNumber = Console.ReadLine();
            newDevTeam.TeamId = int.Parse(idNumber);

            //add developers
            Console.WriteLine("Would you like to add a developer to the team? (y/n):");
            string addDeveloper = Console.ReadLine().ToLower();

            if (addDeveloper == "y")
            {                
                CreateDeveloper();
            }
            else
            {
                Console.WriteLine("That's okay! You can add developers to your team later.");
            }

            bool wasUpdated = _devTeamRepo.UpdateListOfTeams(teamId, newDevTeam);

            if (wasUpdated)
            {
                Console.WriteLine("Team was updated!");
            }
            else
            {
                Console.WriteLine("Could not update team.");
            }

        }
        //remove team
        private void DeleteTeam()
        {
            DisplayTeams();

            //Ask for ID of team to remove
            Console.WriteLine("\nEnter the ID of the team you would like to remove:");

            string userInput = Console.ReadLine();
            int teamId = int.Parse(userInput);

            //delete method
            bool wasRemoved = _devTeamRepo.RemoveTeamFromList(teamId);

            //was content deleted?
            if (wasRemoved)
            {
                Console.WriteLine("The team was removed!");
            }
            else
            {
                Console.WriteLine("Error! Team could not be removed.");
            }
        }

        //see team list 
        private void SeedTeamList()
        {
            DevTeam teamOne = new DevTeam("Panthers", 01, 8);
            DevTeam teamTwo = new DevTeam("Jaguars", 02, 5);
            DevTeam teamThree = new DevTeam("Cheetahs", 03, 3);

            _devTeamRepo.AddTeamToList(teamOne);
            _devTeamRepo.AddTeamToList(teamTwo);
            _devTeamRepo.AddTeamToList(teamThree);
        }

        //method to display all developers in a team by name
        private void DeveloperDirectory()
        {
            Console.Clear();

            bool keepRunning = true;
            while (keepRunning)
            {
                // Display options
                Console.WriteLine("DEVELOPER DIRECTORY\n\n" +
                    "Choose an option from the menu below...\n\n" +
                    "1. Add a new developer\n" +
                    "2. View all developers\n" +
                    "3. Find developer by ID number\n" +
                    "4. Update an existing developer\n" +
                    "5. Remove a developer\n" +
                    "6. Developers that have Pluralsight access\n" +
                    "7. Team Directory\n" +
                    "8. EXIT");

                // Prompt for input
                string userInput = Console.ReadLine();

                // Evaluate input... what did they choose?
                switch (userInput)
                {
                    case "1":
                        //create
                        CreateDeveloper();
                        break;
                    case "2":
                        //View
                        ViewDevelopers();
                        break;
                    case "3":
                        //view by id 
                        FindDeveloperById();
                        break;
                    case "4":
                        //update
                        UpdateDeveloper();
                        break;
                    case "5":
                        //remove/delete
                        RemoveDeveloper();
                        break;
                    case "6":
                        //team directory
                        TeamMenu();
                        break;
                    case "7":
                        Console.WriteLine("Goodbye!");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("You have entered an invalid number. Please try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }


        //create/add new developer
        private void CreateDeveloper()
        {
            Console.Clear();

            Developer newDeveloper = new Developer();

            //first name
            Console.WriteLine("Enter the developer's first name:");
            newDeveloper.FirstName = Console.ReadLine();

            //last name
            Console.WriteLine("Enter the developer's last name:");
            newDeveloper.LastName = Console.ReadLine();

            //id number
            Console.WriteLine("Enter an ID number for the developer:");
            string idNumber = Console.ReadLine();
            newDeveloper.IdNumber = int.Parse(idNumber);

            //has access
            Console.WriteLine("Does this developer have access to Pluralsight? (y/n)");
            string access = Console.ReadLine().ToLower();           

            if (access == "n")
            {
                newDeveloper.HasAccess = false;
            }
            else
            {
                newDeveloper.HasAccess = true;
                //_hasAccess.Add(newDeveloper.IdNumber);
            }

            //is manager
            Console.WriteLine("Is this developer a manager? (y/n)");
            string manager = Console.ReadLine().ToLower();

            if (manager == "y")
            {
                newDeveloper.IsManager = true;
            }
            else
            {
                newDeveloper.IsManager = false;
            }

            //team name
            Console.WriteLine("Which team doees the developer belong to?");
            newDeveloper.NameOfTeam = Console.ReadLine();

            _devRepo.AddDeveloperToList(newDeveloper);
        }

        //view all developers
        private void ViewDevelopers()
        {
            Console.Clear();
            List<Developer> listOfDevelopers = _devRepo.ViewDevelopers();
            foreach (Developer developer in listOfDevelopers)
            {
                Console.WriteLine($"Developer Name: {developer.FirstName} {developer.LastName}\n" +
                    $"Developer ID: {developer.IdNumber}");
            }
        }

        //find developer by ID number
        private void FindDeveloperById()
        {
            Console.Clear();

            //Ask user for ID
            Console.WriteLine("Enter the ID number for the developer you would like to view:");

            //Get input
            string userInput = Console.ReadLine();
            int devId = int.Parse(userInput);

            //Find team w/ that ID
            Developer developer = _devRepo.GetDeveloperById(devId);


            //Display team info
            if (userInput != null)
            {
                Console.WriteLine($"First Name: {developer.FirstName}\n" +
                    $"Last name: {developer.LastName}\n" +
                    $"ID number: {developer.IdNumber}\n" +
                    $"Has Access to Pluralsight: {developer.HasAccess}\n" +
                    $"Is A Manager: {developer.IsManager}");
            }
            else
            {
                Console.WriteLine("Could not find a developer by that ID...");
            }
        }

        //update developer
        private void UpdateDeveloper()
        {
            //display
            ViewDevelopers();
            //prompt for id number
            Console.WriteLine("\nEnter the ID for the developer you would like to update:");
            //get input
            string input = Console.ReadLine();
            int id = int.Parse(input);
            //build new object

            Console.Clear();

            Developer newDeveloper = new Developer();

            //first name
            Console.WriteLine("Enter the developer's first name:");
            newDeveloper.FirstName = Console.ReadLine();

            //last name
            Console.WriteLine("Enter the developer's last name:");
            newDeveloper.LastName = Console.ReadLine();

            //id number
            Console.WriteLine("Enter an ID number for the developer:");
            string idNumber = Console.ReadLine();
            newDeveloper.IdNumber = int.Parse(idNumber);

            //has access
            Console.WriteLine("Does this developer have access to Pluralsight? (y/n)");
            string access = Console.ReadLine().ToLower();

            if (access == "n")
            {
                newDeveloper.HasAccess = false;
            }
            else
            {
                newDeveloper.HasAccess = true;
                //_hasAccess.Add(newDeveloper.IdNumber);
            }

            //is manager
            Console.WriteLine("Is this developer a manager? (y/n)");
            string manager = Console.ReadLine().ToLower();

            if (manager == "y")
            {
                newDeveloper.IsManager = true;
            }
            else
            {
                newDeveloper.IsManager = false;
            }

            bool wasUpdated = _devRepo.UpdateListOfDevelopers(id, newDeveloper);

            if(wasUpdated)
            {
                Console.WriteLine("Your developer was successfully updated!");
            }
            else
            {
                Console.WriteLine("Error! Please try again.");
            }

        }

        //remove developer
        private void RemoveDeveloper()
        {
            ViewDevelopers();

            //Ask for ID of dev to remove
            Console.WriteLine("\nEnter the ID of the developer you would like to remove:");

            string userInput = Console.ReadLine();
            int id = int.Parse(userInput);

            //delete method
            bool wasRemoved = _devRepo.RemoveDeveloperFromList(id);

            //was content deleted?
            if (wasRemoved)
            {
                Console.WriteLine("The developer was removed!");
            }
            else
            {
                Console.WriteLine("Error! Developer could not be removed.");
            }
        }

        //see list for developers
        private void SeedDeveloperList()
        {
            Developer devOne = new Developer("Andrew", "Johnson", 29, true, false, "Jaguars");
            Developer devTwo = new Developer("Katie", "Smith", 64, false, false, "Panthers");
            Developer devThree = new Developer("Amber", "Douglas", 12, true, true, "Cheetahs");

            _devRepo.AddDeveloperToList(devOne);
            _devRepo.AddDeveloperToList(devTwo);
            _devRepo.AddDeveloperToList(devThree);
        }

        //list of developers w/ access to pluralsight
        //private void ViewDevsWithAccess()
        //{
        //    Console.Clear();
        //    int id;
            
        //    if(id != _hasAccess.Contains)
        //    {

        //    }
        //}

        ////add multiple developers at once
        //private void AddMultipleDevs()
        //{
           
            

        //}

    }
}
