using System.Threading.Tasks.Dataflow;
using TimeTracking.Models;
using TimeTracking.Models.Database;
using TimeTracking.Models.Enums;

namespace TimeTracking.Services.Seeder
{
    public class DatabaseSeeder
    {
        private IDatabase<ActiveUser> _userDatabase;
        private IDatabase<Activity> _activitiesDatabase;

        public DatabaseSeeder(IDatabase<ActiveUser> userDatabase, IDatabase<Activity> activitiesDatabase)
        {
            _userDatabase = userDatabase;
            _activitiesDatabase = activitiesDatabase;
        }

        public void Seed()
        {
            ActiveUser activeUser1 = new ActiveUser("Ana", "Bogdanovska", "anaB", "anaB123", "ana_bogdanovska@yahoo.com");
            ActiveUser activeUser2 = new ActiveUser("John", "Doe", "johnD", "johnD123", "john_doe@yahoo.com");

            _userDatabase.Insert(activeUser1);
            _userDatabase.Insert(activeUser2);

            Reading r1 = new Reading("Harry Potter", 30, 20, ReadingTypeEnum.Fiction);
            Reading r2 = new Reading("C# Advanced", 25, 28, ReadingTypeEnum.Professional_Literature);
            Reading r3 = new Reading("The Snowman", 65, 35, ReadingTypeEnum.Belles_Lettres);
            Reading r4 = new Reading("Lord of the rings", 50, 30, ReadingTypeEnum.Fiction);

            Exercising e1 = new Exercising("Hiit Cardio", 30, ExercisingTypeEnum.Sport);
            Exercising e2 = new Exercising("Morning Run", 45, ExercisingTypeEnum.Running);
            Exercising e3 = new Exercising("Bachata", 60, ExercisingTypeEnum.General);
            Exercising e4 = new Exercising("Afternoon Run", 45, ExercisingTypeEnum.Running);


            Working w1 = new Working("Monday work", 480, WorkingTypeEnum.Office);
            Working w2 = new Working("Friday Work", 480, WorkingTypeEnum.Home);
            Working w3 = new Working("Tuesday work", 480, WorkingTypeEnum.Office);

            OtherHobbies o1 = new OtherHobbies("Hiking", 80, "Elevation gain 800");
            OtherHobbies o2 = new OtherHobbies("Dancing", 55, "First class passed");

            activeUser1.ReadingList.Add(r1);
            activeUser1.ReadingList.Add(r3);
            activeUser1.ReadingList.Add(r4);
            activeUser1.ExerciseList.Add(e1);
            activeUser1.ExerciseList.Add(e2);
            activeUser1.ExerciseList.Add(e4);
            activeUser1.WorkingList.Add(w1);
            activeUser1.WorkingList.Add(w2);
            activeUser1.WorkingList.Add(w3);
            activeUser1.OtherHobbiesList.Add(o1);
            activeUser1.OtherHobbiesList.Add(o2);

            activeUser2.ReadingList.Add(r2);
            activeUser2.ExerciseList.Add(e2);
            activeUser2.ExerciseList.Add(e3);
            activeUser2.WorkingList.Add(w2);
            activeUser2.OtherHobbiesList.Add(o2);

            _activitiesDatabase.Insert(r1);
            _activitiesDatabase.Insert(r2);
            _activitiesDatabase.Insert(r3);

            _activitiesDatabase.Insert(e1);
            _activitiesDatabase.Insert(e2);
            _activitiesDatabase.Insert(e3);

            _activitiesDatabase.Insert(w1);
            _activitiesDatabase.Insert(w2);

            _activitiesDatabase.Insert(o1);
            _activitiesDatabase.Insert(o2);
        }
    }
}
