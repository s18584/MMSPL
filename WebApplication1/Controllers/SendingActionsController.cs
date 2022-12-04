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
    public class SendingActionsController : Controller
    {
        private readonly MMSPLContext _context;

        public SendingActionsController(MMSPLContext context)
        {
            _context = context;
        }

        // GET: SendingActions
        public async Task<IActionResult> Index()
        {
            var mMSPLContext = _context.SendingActions
                .Include(s => s.IdCampaignNavigation)
                .Include(s => s.IdSendingActionTypeNavigation);
            return View(await mMSPLContext.ToListAsync());
        }

        // GET: SendingActions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendingAction = await _context.SendingActions
                .Include(s => s.IdCampaignNavigation)
                .Include(s => s.IdSendingActionTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sendingAction == null)
            {
                return NotFound();
            }

            return View(sendingAction);
        }

        // GET: SendingActions/Create
        public IActionResult Create(int? campid)
        {
            if (campid == null)
            {                
                ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Name");
            }
            else 
            {
                ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Name", campid);
            }
            
            ViewData["IdSendingActionType"] = new SelectList(_context.SendingActionTypes, "Id", "Name");
            return View();
        }

        // POST: SendingActions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdSendingActionType,Name,Description,IdCampaign,EmailSubject,EmailBody")] SendingAction sendingAction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sendingAction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Name", sendingAction.IdCampaign);
            ViewData["IdSendingActionType"] = new SelectList(_context.SendingActionTypes, "Id", "Name", sendingAction.IdSendingActionType);
            return View(sendingAction);
        }

        // GET: SendingActions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendingAction = await _context.SendingActions
                .Include(s => s.IdCampaignNavigation)
                .Include(s => s.IdSendingActionTypeNavigation)
                .FirstOrDefaultAsync( x => x.Id.Equals(id));

            if (sendingAction == null)
            {
                return NotFound();
            }
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Name", sendingAction.IdCampaign);
            ViewData["IdSendingActionType"] = new SelectList(_context.SendingActionTypes, "Id", "Name", sendingAction.IdSendingActionType);
            return View(sendingAction);
        }

        // POST: SendingActions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdSendingActionType,Name,Description,IdCampaign,EmailSubject,EmailBody")] SendingAction sendingAction)
        {
            if (id != sendingAction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sendingAction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SendingActionExists(sendingAction.Id))
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
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Name", sendingAction.IdCampaign);
            ViewData["IdSendingActionType"] = new SelectList(_context.SendingActionTypes, "Id", "Name", sendingAction.IdSendingActionType);
            return View(sendingAction);
        }

        // GET: SendingActions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendingAction = await _context.SendingActions
                .Include(s => s.IdCampaignNavigation)
                .Include(s => s.IdSendingActionTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sendingAction == null)
            {
                return NotFound();
            }

            return View(sendingAction);
        }

        // POST: SendingActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sendingAction = await _context.SendingActions.FindAsync(id);
            _context.SendingActions.Remove(sendingAction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SendingActionExists(int id)
        {
            return _context.SendingActions.Any(e => e.Id == id);
        } 
    }
}
