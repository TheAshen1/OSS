using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Localization;
using OSS.Data;
using OSS.Models;
using OSS.Models.SurveySystemModels;

namespace OSS.Controllers
{
    public class HomeController : Controller
    {
        private readonly SurveySystemDbContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(SurveySystemDbContext context, IStringLocalizer<HomeController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public IActionResult Index()
        {

            var model = new SurveyViewModel();

            var Faculties = from f in _context.Faculties
                            select new SelectListItem
                            {
                                Value = f.FacultyId.ToString(),
                                Text = f.ShortName
                            };

            var Lecturers = from l in _context.Lecturers
                            select new SelectListItem
                            {
                                Value = l.LecturerId.ToString(),
                                Text = l.FirstName
                            };

            model.AvailableSpecialties = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };

            model.Subjects = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };

            var Years = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = "1",
                    Text = "1"
                },
                new SelectListItem
                {
                    Value = "2",
                    Text = "2"
                },
                new SelectListItem
                {
                    Value = "3",
                    Text = "3"
                },
                new SelectListItem
                {
                    Value = "4",
                    Text = "4"
                },
                new SelectListItem
                {
                    Value = "5",
                    Text = "5"
                }
            };

            var facultytip = new SelectListItem()
            {
                Value = null,
                Text = "--- select faculty ---"
            };
            var lecturertip = new SelectListItem()
            {
                Value = null,
                Text = "--- select lecturer ---"
            };
            var yeartip = new SelectListItem()
            {
                Value = null,
                Text = "--- select year ---"
            };

            List<SelectListItem> FacList = Faculties.ToList();
            List<SelectListItem> LecList = Lecturers.ToList();
            FacList.Insert(0, facultytip);
            LecList.Insert(0, lecturertip);
            Years.Insert(0, yeartip);

            model.Faculties = new SelectList(FacList, "Value", "Text");
            model.Lecturers = new SelectList(LecList, "Value", "Text");
            model.Years = new SelectList(Years, "Value", "Text");


            //ViewData["Lecturers"] = new SelectList(_context.Lecturers, "LecturerId", "Initials");
            //ViewData["Subjects"] = new SelectList(_context.Subjects, "SubjectId", "FullName");

            model.Years = new SelectList(new[] { 1, 2, 3, 4, 5 });

            var id = _context.Surveys.Where(s => s.Name == "Best lecturer").FirstOrDefault().SurveyId;

            var query = _context.Questions
                .Where(q => q.SurveyId == id);

            ViewBag.SurveyId = id;

            ViewData["Questions"] = query.ToList();

            return View(model);
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
        public IActionResult Index(SurveyViewModel model)
        {
            //Student student = null;
            //string ip = String.Empty ; 
            ///*Get user IP adress and check if he has already sumbitted for this Lecturer and Subject*/
            //string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            //if (!string.IsNullOrEmpty(ipAddress)) {
            //    ip = ipAddress;
            //    var stud = _context.Students.Where(s => s.StudentIP == ip).FirstOrDefault();

            //    if (stud != null)
            //    {
            //        student = stud;
            //        if (_context.Answers.Where(a => a.StudentId == student.StudentId && a.LecturerId == model.LecturerId && a.SubjectId == model.SubjectId).Any())
            //        {
            //            return View("SubmitDenied");
            //        }
            //    }

            //}
            ///**/

            if (ModelState.IsValid)
            {

                var student = new Student()
                {
                    Gender = model.Gender,
                    SpecialtyId = model.SpecialtyId.Value,
                    Year = model.Year.Value
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
                            LecturerId = model.LecturerId.Value,
                            SubjectId = model.SubjectId.Value,
                            SurveyId = model.SurveyId.Value,
                            QuestionId = q.QuestionId,
                            QuestionAnswer = new QuestionAnswer() { Score = S }
                        }
                    );
                }
                _context.Answers.AddRange(answers);

                _context.SaveChanges();

                return View("Success");
            }
            /**/
            var Faculties = from f in _context.Faculties
                            select new SelectListItem
                            {
                                Value = f.FacultyId.ToString(),
                                Text = f.ShortName
                            };

            var Lecturers = from l in _context.Lecturers
                            select new SelectListItem
                            {
                                Value = l.LecturerId.ToString(),
                                Text = l.FirstName
                            };

            model.AvailableSpecialties = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };

            model.Subjects = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };

            var Years = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = "1",
                    Text = "1"
                },
                new SelectListItem
                {
                    Value = "2",
                    Text = "2"
                },
                new SelectListItem
                {
                    Value = "3",
                    Text = "3"
                },
                new SelectListItem
                {
                    Value = "4",
                    Text = "4"
                },
                new SelectListItem
                {
                    Value = "5",
                    Text = "5"
                }
            };

            var facultytip = new SelectListItem()
            {
                Value = null,
                Text = "--- select faculty ---"
            };
            var lecturertip = new SelectListItem()
            {
                Value = null,
                Text = "--- select lecturer ---"
            };
            var yeartip = new SelectListItem()
            {
                Value = null,
                Text = "--- select year ---"
            };

            List<SelectListItem> FacList = Faculties.ToList();
            List<SelectListItem> LecList = Lecturers.ToList();
            FacList.Insert(0, facultytip);
            LecList.Insert(0, lecturertip);
            Years.Insert(0, yeartip);

            model.Faculties = new SelectList(FacList, "Value", "Text");
            model.Lecturers = new SelectList(LecList, "Value", "Text");
            model.Years = new SelectList(Years, "Value", "Text");

            model.Years = new SelectList(new[] { 1, 2, 3, 4, 5 });

            var id = _context.Surveys.Where(s => s.Name == "Best lecturer").FirstOrDefault().SurveyId;

            var queryQuestions = _context.Questions
                .Where(q => q.SurveyId == id);

            ViewBag.SurveyId = id;

            ViewData["Questions"] = queryQuestions.ToList();
            /**/
            return View(model);
        }



        public JsonResult GetSpecialties(int? facultyId)
        {
            if (facultyId.HasValue)
            {

                IEnumerable<SelectListItem> specialties = from s in _context.Specialties
                                                          where s.FacultyId == facultyId
                                                          select new SelectListItem()
                                                          {
                                                              Text = s.FullName,
                                                              Value = s.SpecialtyId.ToString()
                                                          };

                return Json(specialties);
            }
            return null;
        }

        public JsonResult GetSubjects(int? lecturerId)
        {
            if (lecturerId.HasValue)
            {

                IEnumerable<SelectListItem> subjects = from ls in _context.LecturerSubject
                                                       where ls.LecturerId == lecturerId
                                                       join s in _context.Subjects on ls.SubjectId equals s.SubjectId
                                                       select new SelectListItem()
                                                       {
                                                           Text = s.FullName,
                                                           Value = s.SubjectId.ToString()
                                                       };

                return Json(subjects);
            }
            return null;
        }


        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

    }
}
