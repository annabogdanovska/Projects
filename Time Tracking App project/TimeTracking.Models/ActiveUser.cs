using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models.Enums;

namespace TimeTracking.Models
{
    public class ActiveUser : User
    {
        public List<Reading> ReadingList { get; set; }
        public List<Exercising> ExerciseList { get; set; }
        public List<Working> WorkingList { get; set; }
        public List<OtherHobbies> OtherHobbiesList { get; set; }

        public ActiveUser(string firstName, string lastName, string username, string password, string email) 
            :base(firstName, lastName, username, password, UserRole.Active, email)
        {
            ReadingList = new List<Reading>();
            ExerciseList = new List<Exercising>();
            WorkingList = new List<Working>();
            OtherHobbiesList = new List<OtherHobbies>();
        
        }

        public void DeactivateAccount()
        {
            UserRole = UserRole.Deactive;
        }
        public void ActivateAccount()
        {
            UserRole = UserRole.Active;
        }

    }
}
