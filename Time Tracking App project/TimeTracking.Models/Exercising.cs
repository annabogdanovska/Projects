using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models.Enums;

namespace TimeTracking.Models
{
    public class Exercising : Activity
    {
        public ExercisingTypeEnum ExercisingType { get; set; }
        public double TimeGeneralExercise { get; set; }
        public double TimeRunning { get; set; }
        public double TimeSport { get; set; }

        public Exercising(string title , double duration, ExercisingTypeEnum exercingType) 
            :base(title, duration)
        {
            Name = "Exercising";
            ExercisingType = exercingType;
        }
        public Exercising()
        {

        }
    }
}
