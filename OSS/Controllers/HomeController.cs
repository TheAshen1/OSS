using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSS.Data;
using OSS.Models;

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

            return View();
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
    }
}
