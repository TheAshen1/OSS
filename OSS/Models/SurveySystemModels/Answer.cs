using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    public class Answer
    {
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int QuestionAnswerId { get; set; }
        public QuestionAnswer QuestionAnswer { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

    }
}
