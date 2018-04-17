using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    public class QuestionAnswer
    {
        public int QuestionAnswerId { get; set; }
        public int Score { get; set; }

        public Answer Answer { get; set; }
    }
}
