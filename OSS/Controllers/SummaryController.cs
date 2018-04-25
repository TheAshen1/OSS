using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSS.Data;
using OSS.Models.SurveySystemModels;

namespace OSS.Controllers
{
    [Authorize]
    public class SummaryController : Controller
    {
        private readonly SurveySystemDbContext _context;

        public SummaryController(SurveySystemDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            //var surveySystemDbContext = _context.Answers
            //    .Include(a => a.Lecturer)
            //    .Include(a => a.Question)
            //    .Include(a => a.QuestionAnswer)
            //    .Include(a => a.Student)
            //    .Include(a => a.Subject)
            //    .Include(a => a.Survey);

            var teacherAvg = from a in _context.Answers
                             group a by a.Lecturer into g
                             select new SummaryViewModel
                             {
                                 Lecturer = g.Key,
                                 Avg = (decimal)g.Average(a => a.QuestionAnswer.Score)
                             };

            //var model = new SummaryViewModel
            //{
            //    Lecturers = _context.Lecturers,
            //    Avg = teacherAvg
            //};
            return View(teacherAvg.OrderByDescending(a => a.Avg));
        }
    }
}