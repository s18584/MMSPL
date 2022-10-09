using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.models.databasemodels;




namespace WebApplication1.Controllers
{
    public class DocTypesController : Controller
    {
        private readonly MMSPLContext _context;

        public DocTypesController(MMSPLContext context)
        {
            _context = context;
        }

        // GET: DocTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocTypes.ToListAsync());
        }

        // GET: DocTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docType = await _context.DocTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (docType == null)
            {
                return NotFound();
            }

            return View(docType);
        }

        // GET: DocTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DocType docType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(docType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docType);
        }

        // GET: DocTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docType = await _context.DocTypes.FindAsync(id);
            if (docType == null)
            {
                return NotFound();
            }
            return View(docType);
        }

        // POST: DocTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DocType docType)
        {
            if (id != docType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocTypeExists(docType.Id))
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
            return View(docType);
        }

        // GET: DocTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docType = await _context.DocTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (docType == null)
            {
                return NotFound();
            }

            return View(docType);
        }

        // POST: DocTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var docType = await _context.DocTypes.FindAsync(id);
            _context.DocTypes.Remove(docType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocTypeExists(int id)
        {
            return _context.DocTypes.Any(e => e.Id == id);
        }
    }
}
