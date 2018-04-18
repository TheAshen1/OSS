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
    public class AnswersController : Controller
    {
        private readonly SurveySystemDbContext _context;

        public AnswersController(SurveySystemDbContext context)
        {
            _context = context;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            var surveySystemDbContext = _context.Answers.Include(a => a.Lecturer).Include(a => a.Question).Include(a => a.QuestionAnswer).Include(a => a.Student).Include(a => a.Subject).Include(a => a.Survey);
            return View(await surveySystemDbContext.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Lecturer)
                .Include(a => a.Question)
                .Include(a => a.QuestionAnswer)
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .Include(a => a.Survey)
                .SingleOrDefaultAsync(m => m.StudentId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId");
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId");
            ViewData["QuestionAnswerId"] = new SelectList(_context.QuestionAnswer, "QuestionAnswerId", "QuestionAnswerId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyId");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurveyId,QuestionId,QuestionAnswerId,StudentId,LecturerId,SubjectId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", answer.LecturerId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId", answer.QuestionId);
            ViewData["QuestionAnswerId"] = new SelectList(_context.QuestionAnswer, "QuestionAnswerId", "QuestionAnswerId", answer.QuestionAnswerId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", answer.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", answer.SubjectId);
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyId", answer.SurveyId);
            return View(answer);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.SingleOrDefaultAsync(m => m.StudentId == id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", answer.LecturerId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId", answer.QuestionId);
            ViewData["QuestionAnswerId"] = new SelectList(_context.QuestionAnswer, "QuestionAnswerId", "QuestionAnswerId", answer.QuestionAnswerId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", answer.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", answer.SubjectId);
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyId", answer.SurveyId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyId,QuestionId,QuestionAnswerId,StudentId,LecturerId,SubjectId")] Answer answer)
        {
            if (id != answer.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.StudentId))
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
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", answer.LecturerId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId", answer.QuestionId);
            ViewData["QuestionAnswerId"] = new SelectList(_context.QuestionAnswer, "QuestionAnswerId", "QuestionAnswerId", answer.QuestionAnswerId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", answer.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", answer.SubjectId);
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyId", answer.SurveyId);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Lecturer)
                .Include(a => a.Question)
                .Include(a => a.QuestionAnswer)
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .Include(a => a.Survey)
                .SingleOrDefaultAsync(m => m.StudentId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.Answers.SingleOrDefaultAsync(m => m.StudentId == id);
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.StudentId == id);
        }
    }
}
