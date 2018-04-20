using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }

        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
