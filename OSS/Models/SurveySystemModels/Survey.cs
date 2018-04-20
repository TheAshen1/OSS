using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }

        public List<Question> Questions { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
