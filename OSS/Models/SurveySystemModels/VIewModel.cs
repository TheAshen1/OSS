using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    [NotMapped]
    public class ViewModel
    {
        public int FacultyId { get; set; }
        public int SpecialtyId { get; set; }
        public string Gender { get; set; }
        public int LecturerId { get; set; }
        public int SubjectId { get; set; }

        public int SurveyId { get; set; }
        //public List<Question> Questions { get; set; }
        //public List<int> Answers { get; set; }

    }
}
