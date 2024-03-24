using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models.Enums;

namespace TimeTracking.Models
{
    public class Working : Activity
    {
        public WorkingTypeEnum WorkingType { get; set; }
        public double WorkingHoursOffice { get; set; }
        public double WorkingHoursHome { get; set; }

        public Working(string title, double duration, WorkingTypeEnum workingType)
            : base(title, duration)
        {
            Name = "Working";
            WorkingType = workingType;
        }
        public Working()
        {

        }

    }
}
