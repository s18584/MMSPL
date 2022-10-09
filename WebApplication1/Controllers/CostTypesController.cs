using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.models.databasemodels;

namespace WebApplication1.Controllers
{
    public class CostTypesController : Controller
    {
        private readonly MMSPLContext _context;

        public CostTypesController(MMSPLContext context)
        {
            _context = context;
        }

        // GET: CostTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CostTypes.ToListAsync());
        }

        // GET: CostTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costType = await _context.CostTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costType == null)
            {
                return NotFound();
            }

            return View(costType);
        }

        // GET: CostTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CostTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CostType costType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(costType);
        }

        // GET: CostTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costType = await _context.CostTypes.FindAsync(id);
            if (costType == null)
            {
                return NotFound();
            }
            return View(costType);
        }

        // POST: CostTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CostType costType)
        {
            if (id != costType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostTypeExists(costType.Id))
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
            return View(costType);
        }

        // GET: CostTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costType = await _context.CostTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costType == null)
            {
                return NotFound();
            }

            return View(costType);
        }

        // POST: CostTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var costType = await _context.CostTypes.FindAsync(id);
            _context.CostTypes.Remove(costType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostTypeExists(int id)
        {
            return _context.CostTypes.Any(e => e.Id == id);
        }
    }
}
