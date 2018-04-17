using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    public class LecturerSubject
    {
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
