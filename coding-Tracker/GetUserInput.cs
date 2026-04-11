using System.Globalization;

namespace coding_Tracker
{
    internal class GetUserInput
    {
        CodingController codingController = new();
        internal void Mainmenu()// this is the main menu of the app where user can choose what to do
        {
            bool appClose = false;
            while (!appClose)
            {
                Console.WriteLine("\n\n MAIN MENU");
                Console.WriteLine("\n what do u like to do?)");
                Console.WriteLine("\n TYPE 0. Close the app");
                Console.WriteLine("\n TYPE 1. To view records");
                Console.WriteLine("\n TYPE 2. To add a record");
                Console.WriteLine("\n TYPE 3. To delete a record");
                Console.WriteLine("\n TYPE 4. To update a record");

                string commandInput = Console.ReadLine();

                while(string.IsNullOrEmpty(commandInput))
                {
                    Console.WriteLine("\n Invalid input, please enter a valid command");
                    commandInput = Console.ReadLine();
                }

                switch (commandInput)
                {
                    case "0":
                        appClose = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        codingController.Get();
                        break;
                    case "2":
                        ProcessAdd();
                        break;
                    case "3":
                        ProcessDelete();
                        break;
                    case "4":
                        ProcessUpdate();
                        break;
                    default:
                        Console.WriteLine("\n Invalid input, please enter a valid command");
                        break;
                }
            }
        }
        
        private void ProcessDelete()// this is to process the delete command, it will ask user for the id of the record to be deleted and then delete it from the database
        {
            codingController.Get();
            Console.WriteLine("\n Please enter the id of the record to be deleted. Type 0 to return to main menu");

            string idInput = Console.ReadLine();
            if (idInput == "0") Mainmenu();

            while(!int.TryParse(idInput,out _) || string.IsNullOrEmpty(idInput)||Int32.Parse(idInput) < 0)
            {
                Console.WriteLine("\n Invalid input, please enter a valid id of the record to be deleted");
                idInput = Console.ReadLine();

                if (idInput == "0") Mainmenu();
            }

            var id = Int32.Parse(idInput);

            var coding = codingController.GetById(id);

            while(coding.Id == 0)
            {
                Console.WriteLine($"\n Record with id {id} does not exist");
                ProcessDelete();
            }

            codingController.Delete(id);
        }

        private void ProcessAdd()// this is to process the add command, it will ask user for the date and duration of the coding session and then add it to the database
        {
            var date = getDateInput();
            var duration = getDurationInput();

            Coding coding =  new();

            coding.Date = date;
            coding.Duration = duration;

            codingController.Post(coding);
        }

        private void ProcessUpdate()// this is to process the update command, it will ask user for the id of the record to be updated and then ask for the new date and duration of the coding session and then update it in the database
        {
            codingController.Get();
            Console.WriteLine("|n enter the id of the record to be updates . Type 0 to return to main menu");
            string idInput = Console.ReadLine();
            while(!int.TryParse(idInput,out _) || string.IsNullOrEmpty(idInput)||Int32.Parse(idInput) < 0)
            {
                Console.WriteLine("\n Invalid input, please enter a valid id of the record to be updated");
                idInput = Console.ReadLine();
            }

            var id = Int32.Parse(idInput);

            if(id == 0) Mainmenu();

            var coding = codingController.GetById(id);

            while(coding.Id == 0)
            {
                Console.WriteLine($"\n Record with id {id} does not exist");
                ProcessUpdate();
            }
            var updateInput = "";

            bool updating = true;
            while (updating == true)
            {
                Console.WriteLine($"\nType 'd' for Date \n");
                Console.WriteLine($"\nType 't' for Duration \n");
                Console.WriteLine($"\nType 's' to save update \n");
                Console.WriteLine($"\nType '0' to Go Back to Main Menu \n");

                updateInput = Console.ReadLine();

                switch (updateInput)
                {
                    case "d":
                        coding.Date = getDateInput();
                        break;

                    case "t":
                        coding.Duration = getDurationInput();
                        break;

                    case "0":
                        Mainmenu();
                        updating = false;
                        break;

                    case "s":
                        updating = false;
                        break;

                    default:
                        Console.WriteLine($"\nType '0' to Go Back to Main Menu \n");
                        break;
                }
            }
            codingController.Update(coding);
            Mainmenu();

        }

        internal string getDateInput()// this is to get the date input from the user and validate it, it will return the date in the correct format
        {
            Console.WriteLine("\n Please enter the date of the coding session (Forat: dd-mm-yy). Type 0 to return to main menu");
            string dateInput = Console.ReadLine();

            if (dateInput == "0") Mainmenu();

            while(!DateTime.TryParseExact(dateInput,"dd-MM-yy",new CultureInfo("en-US"),DateTimeStyles.None,out _))
            {
                Console.WriteLine("\n Invalid date format, please enter the date in the correct format (dd-mm-yy)");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }

        internal string getDurationInput()// this is to get the duration input from the user and validate it, it will return the duration in the correct format
        {
            Console.WriteLine("\n Please enter the duration of the coding session in format(hh:mm). Type 0 to return to main menu");
            string durationInput = Console.ReadLine();

            if (durationInput == "0") Mainmenu();

            while(!TimeSpan.TryParseExact(durationInput,"hh\\:mm", CultureInfo.InvariantCulture, out _))
            {
                Console.WriteLine("\n Invalid input, please enter a valid time span for the duration");
                durationInput = Console.ReadLine();

                if (durationInput == "0") Mainmenu();
            }

            return durationInput;
        }
    
    }
}