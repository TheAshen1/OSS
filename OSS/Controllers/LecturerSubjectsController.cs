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
    public class LecturerSubjectsController : Controller
    {
        private readonly SurveySystemDbContext _context;

        public LecturerSubjectsController(SurveySystemDbContext context)
        {
            _context = context;
        }

        // GET: LecturerSubjects
        public async Task<IActionResult> Index()
        {
            var surveySystemDbContext = _context.LecturerSubject.Include(l => l.Lecturer).Include(l => l.Subject);
            return View(await surveySystemDbContext.ToListAsync());
        }

        // GET: LecturerSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerSubject = await _context.LecturerSubject
                .Include(l => l.Lecturer)
                .Include(l => l.Subject)
                .SingleOrDefaultAsync(m => m.LecturerId == id);
            if (lecturerSubject == null)
            {
                return NotFound();
            }

            return View(lecturerSubject);
        }

        // GET: LecturerSubjects/Create
        public IActionResult Create()
        {
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            return View();
        }

        // POST: LecturerSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LecturerId,SubjectId")] LecturerSubject lecturerSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturerSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", lecturerSubject.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", lecturerSubject.SubjectId);
            return View(lecturerSubject);
        }

        // GET: LecturerSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerSubject = await _context.LecturerSubject.SingleOrDefaultAsync(m => m.LecturerId == id);
            if (lecturerSubject == null)
            {
                return NotFound();
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", lecturerSubject.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", lecturerSubject.SubjectId);
            return View(lecturerSubject);
        }

        // POST: LecturerSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LecturerId,SubjectId")] LecturerSubject lecturerSubject)
        {
            if (id != lecturerSubject.LecturerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturerSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerSubjectExists(lecturerSubject.LecturerId))
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
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", lecturerSubject.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", lecturerSubject.SubjectId);
            return View(lecturerSubject);
        }

        // GET: LecturerSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerSubject = await _context.LecturerSubject
                .Include(l => l.Lecturer)
                .Include(l => l.Subject)
                .SingleOrDefaultAsync(m => m.LecturerId == id);
            if (lecturerSubject == null)
            {
                return NotFound();
            }

            return View(lecturerSubject);
        }

        // POST: LecturerSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturerSubject = await _context.LecturerSubject.SingleOrDefaultAsync(m => m.LecturerId == id);
            _context.LecturerSubject.Remove(lecturerSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerSubjectExists(int id)
        {
            return _context.LecturerSubject.Any(e => e.LecturerId == id);
        }
    }
}
