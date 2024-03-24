using System.Security.Cryptography.X509Certificates;
using TimeTracking.Models;

namespace TimeTracking.Services.Interface
{
    public interface IActivitiesService 
    {
        void StartNewActivity(Activity activity);
        //void RemoveActivity(Activity activity);
        //string GetReadingStats();

    }
}
