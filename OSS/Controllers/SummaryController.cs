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
using OSS.Models.SurveySystemViewModels;

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

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var data = _context.Answers.Include(a => a.Lecturer).Include(a => a.Lecturer.Faculty);

            var lecturerAvg = data.GroupBy(a => a.Lecturer,
                    (key, g) => new SummaryViewModel
                    {
                        Lecturer = key,
                        Avg = (decimal)g.Average(a => a.QuestionAnswer.Score)
                    }
                );

            return View(lecturerAvg.OrderByDescending(a => a.Avg));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? lecturerId)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult VotingStatistics()
        {
            //var query = from s in _context.Students
            //            group s by s.Specialty.Faculty into g
            //            select new {
            //                Faculty = g.Key,
            //                Count = g.Count(),
            //                Details = (from st in g
            //                           group st by st.Specialty into gr
            //                           select new {
            //                                Specialty = gr.Key,
            //                                Count = g.Count()
            //                           })
            //            };

            var query = from f in _context.Faculties
                        join s in _context.Students
                        on f.FacultyId equals s.Specialty.FacultyId
                        into groupedStudents
                        select new VotingStatistics
                        {
                            Faculty = f,
                            Count = groupedStudents.Count(),
                            //Details = (from sp in _context.Specialties
                            //           where sp.Faculty == f
                            //           join gs in groupedStudents
                            //           on sp equals gs.Specialty
                            //           into groupedBySpecialtyStudents
                            //           select new Tuple<Specialty,int>
                            //           (
                            //               sp,
                            //               groupedBySpecialtyStudents.Count()
                            //           ))
                        };

            return View(query);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult VotingStatisticsDetails(int? id)
        {

            if(!id.HasValue)
                return NotFound();

            ViewBag.Faculty = _context.Faculties.Where(f => f.FacultyId == id).Select(f => f.FullName).SingleOrDefault();

            var query = from sp in _context.Specialties
                        where sp.FacultyId == id
                        join s in _context.Students
                        on sp.SpecialtyId equals s.SpecialtyId
                        into groupedBySpecialtyStudents
                        select new VotingStatisticsDetail
                        { 
                            Specialty =  sp,
                            Count = groupedBySpecialtyStudents.Count()
                        };

            return View(query);
        }

        /**/
        [Authorize(Roles = "Admin, EP")]
        public ActionResult EP()
        {
            var fId = _context.Faculties.Where(f => f.ShortName == "ЭП").SingleOrDefault().FacultyId;
            var data = _context.Answers.Include(a => a.Lecturer).Include(a => a.Lecturer.Faculty).Where(a => a.Lecturer.FacultyId == fId);

            var lecturerAvg = data.GroupBy(a => a.Lecturer,
                    (key, g) => new SummaryViewModel
                    {
                        Lecturer = key,
                        Avg = (decimal)g.Average(a => a.QuestionAnswer.Score)
                    }
                );

            return View("Index", lecturerAvg.OrderByDescending(a => a.Avg));
        }

        [Authorize(Roles = "Admin, EP")]
        public ActionResult EPDetails(int? lecturerId)
        {
            return View();
        }
        /**/
        [Authorize(Roles = "Admin, EI")]
        public ActionResult EI()
        {
            var fId = _context.Faculties.Where(f => f.ShortName == "ЭИ").SingleOrDefault().FacultyId;
            var data = _context.Answers.Include(a => a.Lecturer).Include(a => a.Lecturer.Faculty).Where(a => a.Lecturer.FacultyId == fId);

            var lecturerAvg = data.GroupBy(a => a.Lecturer,
                    (key, g) => new SummaryViewModel
                    {
                        Lecturer = key,
                        Avg = (decimal)g.Average(a => a.QuestionAnswer.Score)
                    }
                );

            return View("Index", lecturerAvg.OrderByDescending(a => a.Avg));
        }

        [Authorize(Roles = "Admin, EI")]
        public ActionResult EIDetails(int? lecturerId)
        {
            return View();
        }
        /**/
        [Authorize(Roles = "Admin, MIM")]
        public ActionResult MIM()
        {
            var fId = _context.Faculties.Where(f => f.ShortName == "МИМ").SingleOrDefault().FacultyId;
            var data = _context.Answers.Include(a => a.Lecturer).Include(a => a.Lecturer.Faculty).Where(a => a.Lecturer.FacultyId == fId);

            var lecturerAvg = data.GroupBy(a => a.Lecturer,
                    (key, g) => new SummaryViewModel
                    {
                        Lecturer = key,
                        Avg = (decimal)g.Average(a => a.QuestionAnswer.Score)
                    }
                );

            return View("Index", lecturerAvg.OrderByDescending(a => a.Avg));
        }

        [Authorize(Roles = "Admin, MIM")]
        public ActionResult MIMDetails(int? lecturerId)
        {
            return View();
        }
        /**/
        [Authorize(Roles = "Admin, MEV")]
        public ActionResult MEV()
        {
            var fId = _context.Faculties.Where(f => f.ShortName == "МЭО").SingleOrDefault().FacultyId;
            var data = _context.Answers.Include(a => a.Lecturer).Include(a => a.Lecturer.Faculty).Where(a => a.Lecturer.FacultyId == fId);

            var lecturerAvg = data.GroupBy(a => a.Lecturer,
                    (key, g) => new SummaryViewModel
                    {
                        Lecturer = key,
                        Avg = (decimal)g.Average(a => a.QuestionAnswer.Score)
                    }
                );

            return View("Index", lecturerAvg.OrderByDescending(a => a.Avg));
        }

        [Authorize(Roles = "Admin, MEV")]
        public ActionResult MEVDetails(int? lecturerId)
        {
            return View();
        }
        /**/
        [Authorize(Roles = "Admin, KIMB")]
        public ActionResult KIMB()
        {
            var fId = _context.Faculties.Where(f => f.ShortName == "КИМБ").SingleOrDefault().FacultyId;
            var data = _context.Answers.Include(a => a.Lecturer).Include(a => a.Lecturer.Faculty).Where(a => a.Lecturer.FacultyId == fId);

            var lecturerAvg = data.GroupBy(a => a.Lecturer,
                    (key, g) => new SummaryViewModel
                    {
                        Lecturer = key,
                        Avg = (decimal)g.Average(a => a.QuestionAnswer.Score)
                    }
                );

            return View("Index", lecturerAvg.OrderByDescending(a => a.Avg));
        }

        [Authorize(Roles = "Admin, KIMB")]
        public ActionResult KIMBDetails(int? lecturerId)
        {
            return View();
        }
        /**/
        [Authorize(Roles = "Admin, FIN")]
        public ActionResult FIN()
        {
            var fId = _context.Faculties.Where(f => f.ShortName == "ФФ").SingleOrDefault().FacultyId;
            var data = _context.Answers.Include(a => a.Lecturer).Include(a => a.Lecturer.Faculty).Where(a => a.Lecturer.FacultyId == fId);

            var lecturerAvg = data.GroupBy(a => a.Lecturer,
                    (key, g) => new SummaryViewModel
                    {
                        Lecturer = key,
                        Avg = (decimal)g.Average(a => a.QuestionAnswer.Score)
                    }
                );

            return View("Index", lecturerAvg.OrderByDescending(a => a.Avg));
        }

        [Authorize(Roles = "Admin, FIN")]
        public ActionResult FINDetails(int? lecturerId)
        {
            return View();
        }
        /**/
        [Authorize(Roles = "Admin, FSF")]
        public ActionResult FSF()
        {
            var fId = _context.Faculties.Where(f => f.ShortName == "ФСФ").SingleOrDefault().FacultyId;
            var data = _context.Answers.Include(a => a.Lecturer).Include(a => a.Lecturer.Faculty).Where(a => a.Lecturer.FacultyId == fId);

            var lecturerAvg = data.GroupBy(a => a.Lecturer,
                    (key, g) => new SummaryViewModel
                    {
                        Lecturer = key,
                        Avg = (decimal)g.Average(a => a.QuestionAnswer.Score)
                    }
                );

            return View("Index", lecturerAvg.OrderByDescending(a => a.Avg));
        }

        [Authorize(Roles = "Admin, FSF")]
        public ActionResult FSFDetails(int? lecturerId)
        {
            return View();
        }
    }
}