using TimeTracking.Models;
using TimeTracking.Models.Database;
using TimeTracking.Services;
using TimeTracking.Services.Seeder;
using TimeTracking.Services.Helpers;
using TimeTracking.Services.Interface;
using TimeTracking.Models.Enums;
using System.Globalization;

namespace TimeTrackingApp
{
    internal class Program
    {

        public int userId { get; set; }

        static void Main(string[] args)
        {
            bool runSeeder = false;

            if (!FileHelper.FileExists(@"..\..\..\Database\ActiveUser.json")
                && !FileHelper.FileExists(@"..\..\..\Database\Activity.json"))
            {
                runSeeder = true;
            }

            IDatabase<ActiveUser> _userDatabase = new Database<ActiveUser>();
            IDatabase<Activity> _activitiesDatabase = new Database<Activity>();
            IDatabase<Reading> _readingDatabase = new Database<Reading>("Activity");
            IDatabase<Working> _workingDatabase = new Database<Working>("Activity");
            IDatabase<Exercising> _exerciseDatabase = new Database<Exercising>("Activity");
            IDatabase<OtherHobbies> _hobbiesDatabase = new Database<OtherHobbies>("Activity");


            DatabaseSeeder seeder = new DatabaseSeeder(_userDatabase, _activitiesDatabase);

            if (runSeeder)
            {
                seeder.Seed();
            }

            IUserService _userService = new UserService(_userDatabase);
            IActivitiesService _activitiesService = new ActivitiesService(_activitiesDatabase);
            IUserActivity _userActivityService = new UserActivity(_userDatabase, _activitiesDatabase, _readingDatabase, _exerciseDatabase, _workingDatabase, _hobbiesDatabase);


            while (true)
            {
                try
                {
                    Console.WriteLine("Choose an option: ");
                    Console.WriteLine("1. Log-in");
                    Console.WriteLine("2. Register");

                    int selection = InputHelper.InputNumber();

                    switch (selection)
                    {
                        case 1: 
                            LoginChoice(_userService, _activitiesService, _userActivityService);
                            break;
                        case 2:
                            RegisterChoice(_userService);
                            break;
                        default:
                            throw new Exception("Invalid choice!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error happened: {ex.Message}");
                }
            }

        }
        static void LoginChoice(IUserService userService, IActivitiesService activitiesService, IUserActivity userActivityService)
        {
            int notValidLogins = 0;
            while(true)
            {
                if(notValidLogins >= 3)
                {
                    Console.WriteLine("You have entered wrong user/password 3 times. Goodbuy until next time!");
                    Environment.Exit(0);
                }
                else
                {
                    Console.Write("Please enter your username: ");
                    string username = Console.ReadLine();

                    Console.Write("Please enter your password: ");
                    string password = Console.ReadLine();

                    bool isValidLogin = userService.ValidLogin(username, password);

                    if(!isValidLogin)
                    {
                        notValidLogins++;
                        continue;
                    }

                    ActiveUser loginUser = userService.Login(username, password);                    

                    if (loginUser.UserRole == TimeTracking.Models.Enums.UserRole.Deactive)
                    {
                        Console.WriteLine("Your account is deactivated and are not able to log in. Would you like to activate your account again?");
                        Console.WriteLine("Choose Y or N");

                        string optionReactivateAccount = Console.ReadLine();

                        if (optionReactivateAccount.ToLower() == "Y".ToLower())
                        {
                            userService.ActivateAccount(username, password);
                            Console.WriteLine("Please proceed with log in");
                        }
                        if (optionReactivateAccount.ToLower() == "N".ToLower())
                        {
                            Console.WriteLine("Your account is deactivated. You will not be able to log in.");
                        }
                        else
                        {
                            throw new Exception("Invalid choice!");  //zasto frla greska pri Y?????????????
                        }
                        return;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Hello user {loginUser.FullName}, great to see you.");
                    Console.ResetColor();
                    Console.WriteLine("Choose an option: ");
                    Console.WriteLine("1. Start new activity");
                    Console.WriteLine("2. Statistics");
                    Console.WriteLine("3. Account Management");
                    Console.WriteLine("4. Log out");

                    int option = InputHelper.InputNumber();

                    switch (option)
                    {
                        case 1:
                            {
                                Console.ResetColor();
                                Console.WriteLine("Please choose activity to be tracked: ");
                                Console.WriteLine("1. Reading");
                                Console.WriteLine("2. Exercising");
                                Console.WriteLine("3. Working");
                                Console.WriteLine("4. Other Hobbies");

                                int trackingOption = InputHelper.InputNumber();

                                switch(trackingOption)
                                {
                                    case 1:
                                        {
                                            Reading r1 = new Reading(); 
                                            double interval = TimeTrack<Reading>.ActivityDuration(r1);
                                            Console.WriteLine("Please enter number of pages you have read");
                                            int pages = InputHelper.InputNumber();
                                            Console.WriteLine("Please enter book Title");
                                            string title = Console.ReadLine();
                                            Console.WriteLine("Please choose the type of the book: \n\t1. Belles_Lettres\n\t2. Fiction\n\t3. Proffesional Literature");
                                            int bookType = InputHelper.InputNumber();
                                            
                                            bool isValidInput = false;

                                            if (bookType == 1 || bookType == 2 || bookType == 3)
                                            {
                                                isValidInput = true;
                                            }

                                            if(!isValidInput)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                throw new Exception("Wrong Input");
                                            }

                                            ReadingTypeEnum type = (ReadingTypeEnum)bookType;
                                            r1.Name = r1.GetType().Name;
                                            r1.Title = title;
                                            r1.Duration = interval;
                                            r1.NbOfPages = pages;
                                            r1.ReadingType = type;

                                            
                                            activitiesService.StartNewActivity(r1);
                                            userActivityService.AddActivityToUser(loginUser.Id, r1.Id);
                                            return;
                                        }
                                    case 2:
                                        {
                                            Exercising e = new Exercising();
                                            double interval = TimeTrack<Exercising>.ActivityDuration(e);
                                            Console.WriteLine("Please enter execise Title");
                                            string title = Console.ReadLine();
                                            Console.WriteLine("Please choose the type of exercising: \n\t1. General\n\t2. Running\n\t3. Sport");
                                            int exerciseType = InputHelper.InputNumber();

                                            bool isValidInput = false;

                                            if (exerciseType == 1 || exerciseType == 2 || exerciseType == 3)
                                            {
                                                isValidInput = true;
                                            }

                                            if (!isValidInput)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                throw new Exception("Wrong Input");
                                            }

                                            ExercisingTypeEnum type = (ExercisingTypeEnum)exerciseType;
                                            e.Name = e.GetType().Name;
                                            e.Title = title;
                                            e.Duration = interval;
                                            e.ExercisingType = type;
                                            activitiesService.StartNewActivity(e);
                                            userActivityService.AddActivityToUser(loginUser.Id, e.Id);
                                            return;
                                        }
                                    case 3:
                                        {
                                            Working w = new Working();
                                            double interval = TimeTrack<Working>.ActivityDuration(w);
                                            Console.WriteLine("Please enter execise Title");
                                            string title = Console.ReadLine();
                                            Console.WriteLine("Please choose the type of working: \n\t1. Office\n\t2. Home");
                                            int workingType = InputHelper.InputNumber();

                                            bool isValidInput = false;

                                            if (workingType == 1 || workingType == 2 || workingType == 3)
                                            {
                                                isValidInput = true;
                                            }

                                            if (!isValidInput)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                throw new Exception("Wrong Input");
                                            }

                                            WorkingTypeEnum type = (WorkingTypeEnum)workingType;
                                            w.Name = w.GetType().Name;
                                            w.Title = title;
                                            w.Duration = interval;
                                            w.WorkingType = type;
                                            activitiesService.StartNewActivity(w);
                                            userActivityService.AddActivityToUser(loginUser.Id, w.Id);
                                            return;
                                        }
                                    case 4:
                                        {
                                            OtherHobbies o = new OtherHobbies();
                                            double interval = TimeTrack<OtherHobbies>.ActivityDuration(o);
                                            Console.WriteLine("Please enter execise Title");
                                            string title = Console.ReadLine();
                                            o.Name = o.GetType().Name;
                                            o.Title = title;
                                            o.Duration = interval;
                                            activitiesService.StartNewActivity(o);
                                            userActivityService.AddActivityToUser(loginUser.Id, o.Id);
                                            break;
                                        }
                                    default:
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            throw new Exception("Invalid choice!");
                                            Console.ResetColor();
                                        }
                                }
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Please choose for which activity would you like to check statistics: ");
                                Console.WriteLine("1. Reading");
                                Console.WriteLine("2. Exercising");
                                Console.WriteLine("3. Working");
                                Console.WriteLine("4. Other Hobbies");
                                Console.WriteLine("5. Global");

                                ActiveUser au = (ActiveUser)loginUser;
                                int trackingOption = InputHelper.InputNumber();

                                switch(trackingOption)
                                {
                                    case 1:
                                        {
                                            double totalTimeReading = userActivityService.GetTotalTimeReading(au.Id);
                                            Console.WriteLine($"You have spent {totalTimeReading/60} hours total time reading");
                                            userActivityService.GetAveragePerReading(au.Id);
                                            Console.WriteLine($"Nb of pages read in total {userActivityService.TotalNbOfPages(au.Id)}");
                                            userActivityService.FavouriteTypeReading(au.Id);
                                            return;
                                        }
                                    case 2:
                                        {
                                            double totalTimeExercising = userActivityService.GetTotalTimeExercising(au.Id);
                                            Console.WriteLine($"You have spent {totalTimeExercising}/60 hours total time exercising");
                                            userActivityService.GetAveragePerExercise(au.Id);
                                            userActivityService.FavouriteTypeExercise(au.Id);
                                            return;
                                        }
                                    case 3:
                                        {
                                            double totalTimeWorking = userActivityService.GetTotalTimeWorking(au.Id);
                                            Console.WriteLine($"You have spent {totalTimeWorking/60} hours total time working");
                                            userActivityService.GetAveragePerWorking(au.Id);
                                            return;
                                        }
                                    case 4:
                                        {
                                            double totalTimeHobbies = userActivityService.GetTotalTimeHobbies(au.Id);
                                            Console.WriteLine($"You have spent {totalTimeHobbies/60} hours total time in hobbies");
                                            userActivityService.GetAllHobbies(au.Id);
                                            return;
                                        }
                                    case 5:
                                        {
                                            double totalTimeGlobal = userActivityService.GetTotalTimeGlobal(au.Id);
                                            Console.WriteLine($"Total time of all activities is {totalTimeGlobal/60} hours");
                                            userActivityService.FavouriteActivity(au.Id);
                                            return;
                                        }
                                    default:
                                        {
                                            throw new Exception("Invalid choice!");
                                        }
                                }
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Please choose an option:");
                                Console.WriteLine("1. Change Password");
                                Console.WriteLine("2. Change FirstName");
                                Console.WriteLine("3. Change LastName");
                                Console.WriteLine("4. Deactivate account");

                                int accountOption = InputHelper.InputNumber();
                                switch (accountOption)
                                {
                                    case 1:
                                        {
                                            Console.WriteLine("Please enter a new Password");
                                            string passwordAfterChange = Console.ReadLine();
                                            userService.ChangePassword(username, password, passwordAfterChange);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Successfully changed password, please proceed with login.");
                                            Console.ResetColor();
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("Please enter a new First Name");
                                            string firstNameToBeChanged = Console.ReadLine();
                                            userService.ChangeFirstName(username, password, firstNameToBeChanged);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Successfully changed First Name");
                                            Console.ResetColor();
                                            break;
                                        }
                                    case 3:
                                        {
                                            Console.WriteLine("Please enter a new Last Name");
                                            string lastNameToBeChanged = Console.ReadLine();
                                            userService.ChangeLastName(username, password, lastNameToBeChanged);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Successfully changed Last Name");
                                            Console.ResetColor();
                                            break;
                                        }
                                    case 4:
                                        {
                                            userService.DeactivateAccount(username, password);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Your account is deactivated. You will not be able to log in.");
                                            Console.ResetColor();
                                            break;
                                        }
                                    default:
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            throw new Exception("Invalid choice!");
                                        }
                                }
                                break;
                            }

                        case 4:
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Thank you {loginUser.FullName}. See you again.");
                                Console.ResetColor();
                                return;
                            }
                        default:
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                throw new Exception("Invalid choice!");
                            }
                    }
                }
            }


        }
        static void RegisterChoice(IUserService userService)
        {
            Console.Write("Please insert you first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Please insert you last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Please insert you username: ");
            string userName = Console.ReadLine();

            Console.Write("Please insert you password: ");
            string password = Console.ReadLine();

            Console.Write("Please insert you email: ");
            string email = Console.ReadLine();

            userService.Register(firstName, lastName, userName, password, email);
            Console.WriteLine($"Thank you for registration. Please proceed on to log-in");
        }
    }
}