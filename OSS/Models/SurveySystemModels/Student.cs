using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    public class Student
    {
        public int StudentId { get; set; }

        //public string StudentIP { get; set; }

        //public int FacultyId { get; set; }
        //public Faculty Faculty { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public string Gender { get; set; }

        public int Year { get; set; }

    }
}
