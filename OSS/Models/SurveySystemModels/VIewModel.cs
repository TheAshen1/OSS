using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    [NotMapped]
    public class ViewModel
    {

        public IEnumerable<SelectListItem> Faculties { get; set; }
        [Display(Name = "Faculty")]
        public int FacultyId { get; set; }

        public IEnumerable<SelectListItem> AvailableSpecialties { get; set; }
        [Display(Name = "Specialty")]
        public int SpecialtyId { get; set; }
        
        [Display(Name = "Course year")]
        public int Year { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }

        public string Gender { get; set; }

        public IEnumerable<SelectListItem> Lecturers { get; set; }
        [Display(Name = "Lecturer")]
        public int LecturerId { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        public int SurveyId { get; set; }

        //public List<Question> Questions { get; set; }
        //public List<int> Answers { get; set; }

    }
}
