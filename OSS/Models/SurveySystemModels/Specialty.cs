﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string SpecialtyCode { get; set; }
        public string FullName { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public List<Student> Students { get; set; }

    }
}
