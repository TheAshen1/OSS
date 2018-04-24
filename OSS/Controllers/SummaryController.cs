using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            //var query = from a in _context.Answers
            //            join l in _context.Lecturers on a.LecturerId equals l.LecturerId

            return View();
        }
    }
}