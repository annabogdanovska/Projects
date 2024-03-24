using TimeTracking.Services.Interface;
using TimeTracking.Models;
using TimeTracking.Models.Database;
using TimeTracking.Services.Helpers;
using System.Net.Http.Headers;

namespace TimeTracking.Services
{
    public class ActivitiesService : IActivitiesService
    {
        public IDatabase<Activity> _activitiesDatabase;

        public ActivitiesService(IDatabase<Activity> activitiesDatabase)
        {
            _activitiesDatabase = activitiesDatabase;
        }

        public void StartNewActivity(Activity activity)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if (string.IsNullOrEmpty(activity.Title) || activity.Title.Length < 3)
            {
                throw new ArgumentException("Invalid data for title");
            }
            if (activity.Duration < 0)
            {
                throw new ArgumentException("Invalid data for duration, it should be more than 0 hours");
            }

            Console.ResetColor();

            if (activity.GetType().Name == "Reading")
            {
                //Reading r = new Reading();
                Reading r = (Reading)activity;

                Console.ForegroundColor = ConsoleColor.Red;

                if (r.NbOfPages < 1)
                {
                    throw new ArgumentException("Invalid data for number of pages, it should be at least 1");
                }
                Console.ResetColor();

                if (r.ReadingType == Models.Enums.ReadingTypeEnum.Belles_Lettres)
                {
                    r.TimeReadingBelet += r.Duration;
                } 
                if(r.ReadingType == Models.Enums.ReadingTypeEnum.Fiction)
                {
                    r.TimeReadingFiction += r.Duration;
                }
                if(r.ReadingType == Models.Enums.ReadingTypeEnum.Professional_Literature)
                {
                    r.TimeReadingProfLiterature += r.Duration;
                }

                //_activitiesDatabase.Insert(r);
            }
            else if(activity.GetType().Name == "Exercising")
            {
                Exercising e = (Exercising)activity;

                if(e.ExercisingType == Models.Enums.ExercisingTypeEnum.General)
                {
                    e.TimeGeneralExercise += e.Duration;
                }
                if (e.ExercisingType == Models.Enums.ExercisingTypeEnum.Running)
                {
                    e.TimeRunning += e.Duration;
                }
                if (e.ExercisingType == Models.Enums.ExercisingTypeEnum.Sport)
                {
                    e.TimeSport += e.Duration;
                }
                //_activitiesDatabase.Insert(e);
            }
            else if(activity.GetType().Name == "Working")
            {
                Working w = (Working)activity;

                if(w.WorkingType == Models.Enums.WorkingTypeEnum.Office)
                {
                    w.WorkingHoursOffice += w.Duration;
                }
                if(w.WorkingType == Models.Enums.WorkingTypeEnum.Home)
                {
                    w.WorkingHoursHome += w.Duration;
                }
                //_activitiesDatabase.Insert(w);
            }
            else if(activity.GetType().Name == "OtherHobbies")
            {
                OtherHobbies o = (OtherHobbies)activity;
                //_activitiesDatabase.Insert(o);
            }
            _activitiesDatabase.Insert(activity);
        }
    }
}