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

        [NotMapped]
        public string Initials => FirstName.Substring(0, 1) + "." + MiddleName.Substring(0, 1) + "." + LastName;

        public byte[] Photo { get; set; }

        public List<LecturerSubject> LecturerSubjects { get; set; }
    }
}
