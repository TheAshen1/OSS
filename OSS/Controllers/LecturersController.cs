using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OSS.Data;
using OSS.Models;
using OSS.Models.SurveySystemModels;

namespace OSS.Controllers
{
    public class LecturersController : Controller
    {
        private readonly SurveySystemDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public LecturersController(SurveySystemDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Lecturers
        public async Task<IActionResult> Index()
        {
            var surveySystemDbContext = _context.Lecturers.Include(l => l.Faculty);
            return View(await surveySystemDbContext.ToListAsync());
        }

        // GET: Lecturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers
                .Include(l => l.Faculty)
                .SingleOrDefaultAsync(m => m.LecturerId == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // GET: Lecturers/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "ShortName");
            return View();
        }

        // POST: Lecturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LecturerId,FirstName,LastName,MiddleName,Initials,Photo,FacultyId")] Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "ShortName", lecturer.FacultyId);
            return View(lecturer);
        }

        // GET: Lecturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers.SingleOrDefaultAsync(m => m.LecturerId == id);
            if (lecturer == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "ShortName", lecturer.FacultyId);
            return View(lecturer);
        }

        // POST: Lecturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LecturerId,FirstName,LastName,MiddleName,Initials,Photo,FacultyId")] Lecturer lecturer)
        {
            if (id != lecturer.LecturerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerExists(lecturer.LecturerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "ShortName", lecturer.FacultyId);
            return View(lecturer);
        }

        // GET: Lecturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers
                .Include(l => l.Faculty)
                .SingleOrDefaultAsync(m => m.LecturerId == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturer = await _context.Lecturers.SingleOrDefaultAsync(m => m.LecturerId == id);
            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerExists(int id)
        {
            return _context.Lecturers.Any(e => e.LecturerId == id);
        }



        public IActionResult ImportLecturers(FileModel fileModel)
        {
            if (fileModel == null)
            {
                return View("Error");
            }


            string rootFolder = _hostingEnvironment.WebRootPath;
            FileInfo file = new FileInfo(fileModel.Path);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Lecturer"];
                int totalRows = workSheet.Dimension.Rows;

                List<Lecturer> lecturers = new List<Lecturer>();

                for (int i = 2; i <= totalRows; i++)
                {
                    var facultyName = workSheet.Cells[i, 4].Value.ToString();
                    var Fid = _context.Faculties.Where(f => f.ShortName == facultyName).SingleOrDefault().FacultyId;
                    lecturers.Add(new Lecturer
                    {
                        FirstName = workSheet.Cells[i, 1].Value.ToString(),
                        LastName = workSheet.Cells[i, 2].Value.ToString(),
                        MiddleName = workSheet.Cells[i, 3].Value.ToString(),
                        FacultyId = Fid
                    });
                }

                _context.Lecturers.AddRange(lecturers);
                _context.SaveChanges();

                file.Delete();

                return RedirectToAction("Index");
            }
        }

        //[Produces("application/json")]
        public FileResult ExportLecturers()
        {
            string rootFolder = _hostingEnvironment.WebRootPath;
            rootFolder = Path.Combine(rootFolder, "files");
            string fileName = @"ExportLecturers.xlsx";

            
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            if (file.Exists)
            {
                file.Delete();
            }

            using (ExcelPackage package = new ExcelPackage(file))
            {

                var lecturers = _context.Lecturers.Include(l => l.Faculty);

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Lecturer");

                worksheet.Cells[1, 1].Value = "Lecturer ID";
                worksheet.Cells[1, 2].Value = "Lecturer FirstName";
                worksheet.Cells[1, 3].Value = "Lecturer LastName";
                worksheet.Cells[1, 4].Value = "Lecturer MiddleName";
                worksheet.Cells[1, 5].Value = "Lecturer Faculty";

                int row = 2;
                foreach (var lecturer in lecturers)
                {
                    worksheet.Cells[row, 1].Value = lecturer.LecturerId;
                    worksheet.Cells[row, 2].Value = lecturer.FirstName;
                    worksheet.Cells[row, 3].Value = lecturer.LastName;
                    worksheet.Cells[row, 4].Value = lecturer.MiddleName;
                    worksheet.Cells[row, 5].Value = lecturer.Faculty.ShortName;
                    row++;
                }

                package.Save();

            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(rootFolder, fileName));
            return File(fileBytes, "application/x-msdownload", fileName);
        }
    }
}
