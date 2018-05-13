using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    [NotMapped]
    public class LecturerViewModel
    {
        public IEnumerable<Lecturer> Lecturers { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }

    }
}
