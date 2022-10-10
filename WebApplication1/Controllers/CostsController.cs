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
    public class CostsController : Controller
    {
        private readonly MMSPLContext _context;

        public CostsController(MMSPLContext context)
        {
            _context = context;
        }

        // GET: Costs
        public async Task<IActionResult> Index()
        {
            var mMSPLContext = _context.Costs.Include(c => c.IdCampaignNavigation).Include(c => c.IdContractorNavigation).Include(c => c.IdCostTypeNavigation);
            return View(await mMSPLContext.ToListAsync());
        }

        // GET: Costs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .Include(c => c.IdCampaignNavigation)
                .Include(c => c.IdContractorNavigation)
                .Include(c => c.IdCostTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // GET: Costs/Create
        public IActionResult Create()
        {
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description");
            ViewData["IdContractor"] = new SelectList(_context.Contractors, "Id", "Name");
            ViewData["IdCostType"] = new SelectList(_context.CostTypes, "Id", "Name");
            return View();
        }

        // POST: Costs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCostType,Amount,IdCampaign,IdContractor")] Cost cost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description", cost.IdCampaign);
            ViewData["IdContractor"] = new SelectList(_context.Contractors, "Id", "Name", cost.IdContractor);
            ViewData["IdCostType"] = new SelectList(_context.CostTypes, "Id", "Name", cost.IdCostType);
            return View(cost);
        }

        // GET: Costs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs.FindAsync(id);
            if (cost == null)
            {
                return NotFound();
            }
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description", cost.IdCampaign);
            ViewData["IdContractor"] = new SelectList(_context.Contractors, "Id", "Name", cost.IdContractor);
            ViewData["IdCostType"] = new SelectList(_context.CostTypes, "Id", "Name", cost.IdCostType);
            return View(cost);
        }

        // POST: Costs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCostType,Amount,IdCampaign,IdContractor")] Cost cost)
        {
            if (id != cost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostExists(cost.Id))
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
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description", cost.IdCampaign);
            ViewData["IdContractor"] = new SelectList(_context.Contractors, "Id", "Name", cost.IdContractor);
            ViewData["IdCostType"] = new SelectList(_context.CostTypes, "Id", "Name", cost.IdCostType);
            return View(cost);
        }

        // GET: Costs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .Include(c => c.IdCampaignNavigation)
                .Include(c => c.IdContractorNavigation)
                .Include(c => c.IdCostTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // POST: Costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cost = await _context.Costs.FindAsync(id);
            _context.Costs.Remove(cost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostExists(int id)
        {
            return _context.Costs.Any(e => e.Id == id);
        }
    }
}
