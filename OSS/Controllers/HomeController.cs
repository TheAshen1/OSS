using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using OSS.Data;
using OSS.Models;
using OSS.Models.SurveySystemModels;

namespace OSS.Controllers
{
    public class HomeController : Controller
    {
        private readonly SurveySystemDbContext _context;

        public HomeController(SurveySystemDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            ViewData["Faculties"] = new SelectList(_context.Faculties, "FacultyId", "ShortName");
            ViewData["Specialties"] = new SelectList(_context.Specialties, "SpecialtyId", "SpecialtyCode");
            ViewData["Lecturers"] = new SelectList(_context.Lecturers, "LecturerId", "FirstName");
            ViewData["Subjects"] = new SelectList(_context.Subjects, "SubjectId", "FullName");

            var id = _context.Surveys.Where(s => s.Name == "Best lecturer").FirstOrDefault().SurveyId;

            var query = _context.Questions  
                .Where(q => q.SurveyId == id);

            //ViewData["Questions"] = query.ToList();
            
            return View(query.AsEnumerable());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Produces("text/html")]
        public IActionResult SurveySubmit(int FacultyId, int SpecialtyId, int LecturerId, int SubjectId, params int[] answers)
        {
            StringBuilder str = new StringBuilder();

            foreach (var a in Request.Form)
            {
                str.AppendFormat("{0}: {1} \n", a.Key, a.Value);
            }
            return Content(str.ToString());
        }
    }
}
