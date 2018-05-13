using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSS.Data;
using OSS.Models.SurveySystemModels;

namespace OSS.Controllers
{
    public class FileModelsController : Controller
    {
        private readonly SurveySystemDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public FileModelsController(SurveySystemDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: FileModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Files.ToListAsync());
        }

        // GET: FileModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileModel = await _context.Files
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fileModel == null)
            {
                return NotFound();
            }

            return View(fileModel);
        }

        // GET: FileModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Path")] FileModel fileModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileModel);
        }

        // GET: FileModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileModel = await _context.Files.SingleOrDefaultAsync(m => m.Id == id);
            if (fileModel == null)
            {
                return NotFound();
            }
            return View(fileModel);
        }

        // POST: FileModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Path")] FileModel fileModel)
        {
            if (id != fileModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileModelExists(fileModel.Id))
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
            return View(fileModel);
        }

        // GET: FileModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileModel = await _context.Files
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fileModel == null)
            {
                return NotFound();
            }

            return View(fileModel);
        }

        // POST: FileModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileModel = await _context.Files.SingleOrDefaultAsync(m => m.Id == id);
            _context.Files.Remove(fileModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileModelExists(int id)
        {
            return _context.Files.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {

            var allowedExtension = ".xlsx";
            FileModel FILEMODEL = null;
            if (uploadedFile != null)
            {
                var checkextension = Path.GetExtension(uploadedFile.FileName).ToLower();

                if (checkextension == allowedExtension)
                {
                    string path = Path.Combine(_appEnvironment.WebRootPath, "files", uploadedFile.FileName);

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                    FILEMODEL = file;
                    _context.Files.Add(file);
                    _context.SaveChanges();


                }
            }

            return RedirectToAction("ImportLecturers","Lecturers",new { fileModel = FILEMODEL });
        }
    }
}
