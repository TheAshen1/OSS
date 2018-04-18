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
    public class SurveyQuestionsController : Controller
    {
        private readonly SurveySystemDbContext _context;

        public SurveyQuestionsController(SurveySystemDbContext context)
        {
            _context = context;
        }

        // GET: SurveyQuestions
        public async Task<IActionResult> Index()
        {
            var surveySystemDbContext = _context.SurveyQuestion.Include(s => s.Question).Include(s => s.Survey);
            return View(await surveySystemDbContext.ToListAsync());
        }

        // GET: SurveyQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyQuestion = await _context.SurveyQuestion
                .Include(s => s.Question)
                .Include(s => s.Survey)
                .SingleOrDefaultAsync(m => m.QuestionId == id);
            if (surveyQuestion == null)
            {
                return NotFound();
            }

            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId");
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyId");
            return View();
        }

        // POST: SurveyQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,SurveyId")] SurveyQuestion surveyQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surveyQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId", surveyQuestion.QuestionId);
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyId", surveyQuestion.SurveyId);
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyQuestion = await _context.SurveyQuestion.SingleOrDefaultAsync(m => m.QuestionId == id);
            if (surveyQuestion == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId", surveyQuestion.QuestionId);
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyId", surveyQuestion.SurveyId);
            return View(surveyQuestion);
        }

        // POST: SurveyQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,SurveyId")] SurveyQuestion surveyQuestion)
        {
            if (id != surveyQuestion.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surveyQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyQuestionExists(surveyQuestion.QuestionId))
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
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionId", surveyQuestion.QuestionId);
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyId", surveyQuestion.SurveyId);
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyQuestion = await _context.SurveyQuestion
                .Include(s => s.Question)
                .Include(s => s.Survey)
                .SingleOrDefaultAsync(m => m.QuestionId == id);
            if (surveyQuestion == null)
            {
                return NotFound();
            }

            return View(surveyQuestion);
        }

        // POST: SurveyQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surveyQuestion = await _context.SurveyQuestion.SingleOrDefaultAsync(m => m.QuestionId == id);
            _context.SurveyQuestion.Remove(surveyQuestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyQuestionExists(int id)
        {
            return _context.SurveyQuestion.Any(e => e.QuestionId == id);
        }
    }
}
