using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models;

namespace TimeTracking.Services.Helpers
{
    public static class TimeTrack<T> where T : BaseEntity
    {
        public static double ActivityDuration(T entity)
        {
            DateTime startOfActivity = DateTime.Now;
            Console.WriteLine($"The countdown for {entity.GetType().Name} is started");
            Console.ReadLine();
            DateTime stopOfActivity = DateTime.Now;
            TimeSpan interval = stopOfActivity - startOfActivity;
            double duration = interval.TotalMinutes;
            Console.WriteLine($"You have spent total {duration} minutes {entity.GetType().Name}");
            return duration;
        }
    }
}
