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
    public class SendingActionTypesController : Controller
    {
        private readonly MMSPLContext _context;

        public SendingActionTypesController(MMSPLContext context)
        {
            _context = context;
        }

        // GET: SendingActionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SendingActionTypes.ToListAsync());
        }

        // GET: SendingActionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendingActionType = await _context.SendingActionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sendingActionType == null)
            {
                return NotFound();
            }

            return View(sendingActionType);
        }

        // GET: SendingActionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SendingActionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SendingActionType sendingActionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sendingActionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sendingActionType);
        }

        // GET: SendingActionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendingActionType = await _context.SendingActionTypes.FindAsync(id);
            if (sendingActionType == null)
            {
                return NotFound();
            }
            return View(sendingActionType);
        }

        // POST: SendingActionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SendingActionType sendingActionType)
        {
            if (id != sendingActionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sendingActionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SendingActionTypeExists(sendingActionType.Id))
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
            return View(sendingActionType);
        }

        // GET: SendingActionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendingActionType = await _context.SendingActionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sendingActionType == null)
            {
                return NotFound();
            }

            return View(sendingActionType);
        }

        // POST: SendingActionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sendingActionType = await _context.SendingActionTypes.FindAsync(id);
            _context.SendingActionTypes.Remove(sendingActionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SendingActionTypeExists(int id)
        {
            return _context.SendingActionTypes.Any(e => e.Id == id);
        }
    }
}
