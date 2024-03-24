using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models;
using TimeTracking.Services.Interface;
using TimeTracking.Models.Database;
using TimeTracking.Services.Helpers;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;

namespace TimeTracking.Services
{
    public class UserActivity : IUserActivity
    {
        private IDatabase<ActiveUser> _userDatabase;
        private IDatabase<Activity> _activitiesDatabase;
        private IDatabase<Reading> _readingDa;
        private IDatabase<Exercising> _exercisingDa;
        private IDatabase<Working> _workingDa;
        private IDatabase<OtherHobbies> _otherHobbiesDa;

        public UserActivity(IDatabase<ActiveUser> userDatabase, IDatabase<Activity> activitiesDatabase, IDatabase<Reading> readingDa, IDatabase<Exercising> exercisingDa, IDatabase<Working> workingDa, IDatabase<OtherHobbies> otherHobbiesDa)
        {
            _userDatabase = userDatabase;
            _activitiesDatabase = activitiesDatabase;
            _readingDa = readingDa;
            _exercisingDa = exercisingDa;
            _workingDa = workingDa;
            _otherHobbiesDa = otherHobbiesDa;
        }

        public void AddActivityToUser(int userId, int activityId)
        {
            User u = _userDatabase.GetById(userId);
            Activity activity = _activitiesDatabase.GetById(activityId);

            if (u.UserRole == Models.Enums.UserRole.Deactive)
            {
                throw new Exception("Can't add an activity to a deactivated user");
            }

            ActiveUser activeUser = (ActiveUser)u;

            if (activity.Name == "Reading")
            {

                Reading r = _readingDa.GetById(activityId);
                activeUser.ReadingList.Add(r);
                _userDatabase.Update(activeUser);
            }
            else if(activity.Name == "Exercising")
            {
                Exercising e = _exercisingDa.GetById(activityId);
                activeUser.ExerciseList.Add(e);
                _userDatabase.Update(activeUser);
            }
            else if (activity.Name == "Working")
            {
                Working w = _workingDa.GetById(activityId);
                activeUser.WorkingList.Add(w);
                _userDatabase.Update(activeUser);
            }
            else if(activity.Name == "OtherHobbies")
            {
                OtherHobbies o = _otherHobbiesDa.GetById(activityId);
                activeUser.OtherHobbiesList.Add(o);
                _userDatabase.Update(activeUser);
            }
        }

        public double GetTotalTimeReading(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            double totalTime = au.ReadingList.Select(x => x.Duration).ToList().Sum();

            return totalTime;
        }

        public void GetAveragePerReading(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            double totalDuration = au.ReadingList.Select(x => x.Duration).ToList().Sum();
            double durationBelles = au.ReadingList.Where(x => x.ReadingType == Models.Enums.ReadingTypeEnum.Belles_Lettres).Select(x => x.Duration).ToList().Sum();
            double durationFiction = au.ReadingList.Where(x => x.ReadingType == Models.Enums.ReadingTypeEnum.Fiction).Select(x => x.Duration).ToList().Sum();
            double durationProLit = au.ReadingList.Where(x => x.ReadingType == Models.Enums.ReadingTypeEnum.Professional_Literature).Select(x => x.Duration).ToList().Sum();

            double averageBellesLettres = (durationBelles / totalDuration) * 100;
            double averageFiction = (durationFiction / totalDuration) * 100;
            double averageProfLit = (durationProLit / totalDuration) * 100;

            Console.WriteLine($"You have spent\n\t{averageBellesLettres}% reading Belles Letters\n\t{averageFiction}% reading Fiction\n\t{averageProfLit}% reading Proffesional Literature");
        }
        public int TotalNbOfPages(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            int totalNbOfPages = au.ReadingList.Select(x => x.NbOfPages).ToList().Sum();

            return totalNbOfPages;
        }
        public void FavouriteTypeReading(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            int bellesRecords = au.ReadingList.Where(x => x.ReadingType == Models.Enums.ReadingTypeEnum.Belles_Lettres).Count();
            int fictionRecords = au.ReadingList.Where(x => x.ReadingType == Models.Enums.ReadingTypeEnum.Fiction).Count();
            int proLitRecords = au.ReadingList.Where(x => x.ReadingType == Models.Enums.ReadingTypeEnum.Professional_Literature).Count();

            if (bellesRecords > fictionRecords)
            {
                if (bellesRecords > proLitRecords)
                {
                    Console.WriteLine("Your favourite type is Belles Letters");
                }
                else
                {
                    Console.WriteLine("Your favourite type is Proffesional Literature");
                }
            }
            else if (fictionRecords > proLitRecords)
                Console.WriteLine("Your favourite type is Fiction Literature");
            else
                Console.WriteLine("Your favourite type is Proffesional Literature");
        }

        public double GetTotalTimeExercising(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            double totalTime = au.ExerciseList.Select(x => x.Duration).ToList().Sum();

            return totalTime;
        }
        public void GetAveragePerExercise(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            double totalDuration = au.ExerciseList.Select(x => x.Duration).ToList().Sum();
            double durationGeneral = au.ExerciseList.Where(x => x.ExercisingType == Models.Enums.ExercisingTypeEnum.General).Select(x => x.Duration).ToList().Sum();
            double durationRunning = au.ExerciseList.Where(x => x.ExercisingType == Models.Enums.ExercisingTypeEnum.Running).Select(x => x.Duration).ToList().Sum();
            double durationSport = au.ExerciseList.Where(x => x.ExercisingType == Models.Enums.ExercisingTypeEnum.Sport).Select(x => x.Duration).ToList().Sum();

            double averageGeneral = (durationGeneral / totalDuration) * 100;
            double averageRunning = (durationRunning / totalDuration) * 100;
            double averageSport = (durationSport / totalDuration) * 100;

            Console.WriteLine($"You have spent\n\t{averageGeneral}% in general exercises\n\t{averageRunning}% running\n\t{averageSport}% sports activities");
        }
        public void FavouriteTypeExercise(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            int general = au.ExerciseList.Where(x => x.ExercisingType == Models.Enums.ExercisingTypeEnum.General).Count();
            int running = au.ExerciseList.Where(x => x.ExercisingType == Models.Enums.ExercisingTypeEnum.Running).Count();
            int sports = au.ExerciseList.Where(x => x.ExercisingType == Models.Enums.ExercisingTypeEnum.Sport).Count();

            if (general > running)
            {
                if (general > sports)
                {
                    Console.WriteLine("Your favourite type is General exercises");
                }
                else
                {
                    Console.WriteLine("Your favourite type is Sport activities");
                }
            }
            else if (running > sports)
                Console.WriteLine("Your favourite type is Running");
            else
                Console.WriteLine("Your favourite type is Sport activities");
        }
        public double GetTotalTimeWorking(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            double totalTime = au.WorkingList.Select(x => x.Duration).ToList().Sum();

            return totalTime;
        }
        public void GetAveragePerWorking(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            double totalDuration = au.WorkingList.Select(x => x.Duration).ToList().Sum();
            double durationOffice = au.WorkingList.Where(x => x.WorkingType == Models.Enums.WorkingTypeEnum.Office).Select(x => x.Duration).ToList().Sum();
            double durationHome = au.WorkingList.Where(x => x.WorkingType == Models.Enums.WorkingTypeEnum.Home).Select(x => x.Duration).ToList().Sum();

            double averageOffice = (durationOffice / totalDuration) * 100;
            double averageHome = (durationHome/totalDuration) * 100;

            Console.WriteLine($"You have spent\n\t{averageOffice}% working in office\n\t{averageHome}% working home");
            Console.WriteLine($"Total hours working from office {durationOffice/60} and  total hours working from home {durationHome/60}");
        }
        public double GetTotalTimeHobbies(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            double totalTime = au.OtherHobbiesList.Select(x => x.Duration).ToList().Sum();

            return totalTime;
        }
        public void GetAllHobbies(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            au.OtherHobbiesList.Select(x => x.Title)
                               .Distinct().ToList()
                               .ForEach(x => Console.WriteLine(x));
        }
        public double GetTotalTimeGlobal(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            //double durationReading = au.ReadingList.Select(x => x.Duration).ToList().Sum();
            //double durationWorking = au.WorkingList.Select(x => x.Duration).ToList().Sum();
            //double durationExercising = au.ExerciseList.Select(x => x.Duration).ToList().Sum();
            //double durationHobbies = au.OtherHobbiesList.Select(x => x.Duration).ToList().Sum();

            //double totalTime = durationReading + durationWorking + durationExercising + durationHobbies;

            //return totalTime;

            List<Activity> activities = new List<Activity>();
            activities.AddRange(au.ReadingList);
            activities.AddRange(au.ExerciseList);
            activities.AddRange(au.WorkingList);
            activities.AddRange(au.OtherHobbiesList);

            var totalTime = activities.Select(x => x.Duration).Sum();
            return totalTime;
        }

        public void FavouriteActivity(int userId)
        {
            User u = _userDatabase.GetById(userId);
            ActiveUser au = (ActiveUser)u;

            List<Activity> activities = new List<Activity>();

            activities.AddRange(au.ReadingList);
            activities.AddRange(au.ExerciseList);
            activities.AddRange(au.WorkingList);
            activities.AddRange(au.OtherHobbiesList);

            var group = activities.GroupBy(x => x.Name).Select(x => new KeyValuePair<string, int>(x.Key, x.Count())).ToList();

            var max = group.OrderByDescending(x => x.Value).FirstOrDefault();

            Console.WriteLine($"Your favourite activity is: {max.Key}");

        }
    }
}
