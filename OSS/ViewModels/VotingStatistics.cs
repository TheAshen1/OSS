using OSS.Models.SurveySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemViewModels
{
    public class VotingStatistics
    {
        public Faculty Faculty { get; set; }
        public int Count { get; set; }
        //public IEnumerable<Tuple<Specialty,int>> Details { get; set; }
    }
}
