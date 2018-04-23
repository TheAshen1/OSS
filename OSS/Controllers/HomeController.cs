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
            ViewData["Specialties"] = new SelectList(_context.Specialties, "SpecialtyId", "FullName");
            ViewData["Lecturers"] = new SelectList(_context.Lecturers, "LecturerId", "FirstName");
            ViewData["Subjects"] = new SelectList(_context.Subjects, "SubjectId", "FullName");

            var id = _context.Surveys.Where(s => s.Name == "Best lecturer").FirstOrDefault().SurveyId;

            var query = _context.Questions  
                .Where(q => q.SurveyId == id);

            ViewBag.SurveyId = id;

            ViewData["Questions"] = query.ToList();

            return View();
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            //ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SurveySubmit(ViewModel model)
        {
            StringBuilder str = new StringBuilder();

            //str.AppendFormat("{0}: {1} \n", "Факультет", model.FacultyId);
            //str.AppendFormat("{0}: {1} \n", "Специальность", model.SpecialtyId);
            //str.AppendFormat("{0}: {1} \n", "Пол", model.Gender);
            //str.AppendFormat("{0}: {1} \n", "Предодаватель", model.LecturerId);
            //str.AppendFormat("{0}: {1} \n", "Предмет", model.SubjectId);

            var student = new Student()
            {
                Gender = model.Gender,
                SpecialtyId = model.SpecialtyId
            };
            var answers = new List<Answer>();

            var query = _context.Questions
               .Where(q => q.SurveyId == model.SurveyId);

            var questions = query.ToList();
            foreach (var q in questions)
            {
                int S = 0;
                if (!string.IsNullOrWhiteSpace(Request.Form[q.QuestionId.ToString()]))
                {
                    S = Int32.Parse(Request.Form[q.QuestionId.ToString()]);
                }

                answers.Add(
                    new Answer()
                    {
                        Student = student,
                        LecturerId = model.LecturerId,
                        SubjectId = model.SubjectId,
                        SurveyId = model.SurveyId,
                        QuestionId = q.QuestionId,
                        QuestionAnswer = new QuestionAnswer() { Score = S  }                     
                    }
                );
            }
            _context.Answers.AddRange(answers);

            _context.SaveChanges();


            foreach (var p in Request.Form)
            {
                str.AppendFormat("{0}: {1} \n", p.Key, p.Value);
            }

            return View("Success");/* Content(str.ToString());*/
        }
    }
}
