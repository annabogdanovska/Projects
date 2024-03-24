using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models.Enums;

namespace TimeTracking.Models
{
    public class Reading : Activity
    {
        //public string Name = "Reading";
        public int NbOfPages { get; set; }
        public ReadingTypeEnum ReadingType { get; set; }
        public double TimeReadingBelet { get; set; }
        public double TimeReadingFiction { get; set; }
        public double TimeReadingProfLiterature { get; set; }

        public Reading(string title, double duration, int nbOfPages, ReadingTypeEnum readingType)
            :base(title, duration)
        {
            Name = "Reading";
            NbOfPages = nbOfPages;
            ReadingType = readingType;
        }
        public Reading()
        {

        }

        public override string ToString()
        {
            return Title;
        }

    }
}
