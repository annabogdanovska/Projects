using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models.Enums;

namespace TimeTracking.Models
{
    public class OtherHobbies : Activity
    {
        public double SpentHours { get; set; }
        public string Note { get; set; }

        public OtherHobbies(string title, double duration, string note)
             : base(title, duration)
        {
            Name = "OtherHobbies";
            Note = note;
        }
        public OtherHobbies()
        {

        }
        public override string ToString()
        {
            return Title;
        }
    }
}
