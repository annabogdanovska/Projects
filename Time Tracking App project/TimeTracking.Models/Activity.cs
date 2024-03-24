using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public double Duration { get; set; }

        public Activity(string title, double duration)
        {
            Title = title;
            Duration = duration;

        }
        public Activity()
        {

        }

    }
}
