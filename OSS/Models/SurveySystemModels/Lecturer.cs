using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Initials { get; set; }

        public byte[] Photo { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }      

        public List<LecturerSubject> LecturerSubjects { get; set; }
    }
}
