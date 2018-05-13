using Microsoft.ApplicationInsights.AspNetCore;
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
    public class SurveyViewModel
    {

        public IEnumerable<SelectListItem> Faculties { get; set; }
        [Required(ErrorMessage= "FacultyIsRequired")]
        [Display(Name = "Faculty")]
        public int? FacultyId { get; set; }

        public IEnumerable<SelectListItem> AvailableSpecialties { get; set; }
        [Required(ErrorMessage = "SpecialtyIsRequired")]
        [Display(Name = "Specialty")]
        public int? SpecialtyId { get; set; }

        [Required(ErrorMessage = "CourseIsRequired")]
        [Display(Name = "Course")]
        public int? Year { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }

        [Required(ErrorMessage = "GenderIsRequired")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public IEnumerable<SelectListItem> Lecturers { get; set; }
        [Required(ErrorMessage = "LecturerIsRequired")]
        [Display(Name = "Lecturer")]
        public int? LecturerId { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }
        [Required(ErrorMessage = "SubjectIsRequired")]
        [Display(Name = "Subject")]
        public int? SubjectId { get; set; }

        public int? SurveyId { get; set; }


    }
}
