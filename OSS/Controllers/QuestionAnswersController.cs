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
    public class QuestionAnswersController : Controller
    {
        private readonly SurveySystemDbContext _context;

        public QuestionAnswersController(SurveySystemDbContext context)
        {
            _context = context;
        }

        // GET: QuestionAnswers
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuestionAnswer.ToListAsync());
        }

        // GET: QuestionAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _context.QuestionAnswer
                .SingleOrDefaultAsync(m => m.QuestionAnswerId == id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionAnswerId,Score")] QuestionAnswer questionAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _context.QuestionAnswer.SingleOrDefaultAsync(m => m.QuestionAnswerId == id);
            if (questionAnswer == null)
            {
                return NotFound();
            }
            return View(questionAnswer);
        }

        // POST: QuestionAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionAnswerId,Score")] QuestionAnswer questionAnswer)
        {
            if (id != questionAnswer.QuestionAnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionAnswerExists(questionAnswer.QuestionAnswerId))
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
            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionAnswer = await _context.QuestionAnswer
                .SingleOrDefaultAsync(m => m.QuestionAnswerId == id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            return View(questionAnswer);
        }

        // POST: QuestionAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionAnswer = await _context.QuestionAnswer.SingleOrDefaultAsync(m => m.QuestionAnswerId == id);
            _context.QuestionAnswer.Remove(questionAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionAnswerExists(int id)
        {
            return _context.QuestionAnswer.Any(e => e.QuestionAnswerId == id);
        }
    }
}
