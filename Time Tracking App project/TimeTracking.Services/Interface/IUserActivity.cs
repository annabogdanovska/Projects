using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models;

namespace TimeTracking.Services.Interface
{
    public interface IUserActivity
    {
        void AddActivityToUser(int userId, int activityId);
        double GetTotalTimeReading(int userId);
        void GetAveragePerReading(int userId);
        int TotalNbOfPages(int userId);
        void FavouriteTypeReading(int userId);
        double GetTotalTimeExercising(int userId);
        void GetAveragePerExercise(int userId);
        void FavouriteTypeExercise(int userId);
        double GetTotalTimeWorking(int userId);
        void GetAveragePerWorking(int userId);
        double GetTotalTimeHobbies(int userId);
        void GetAllHobbies(int userId);
        double GetTotalTimeGlobal(int userId);
        void FavouriteActivity(int userId);
    }
}
