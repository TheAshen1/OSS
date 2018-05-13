using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Models.SurveySystemModels
{
    [NotMapped]
    public class SummaryViewModel
    {
        public Lecturer Lecturer { get; set; }
        public decimal Avg { get; set; }
    }
}
