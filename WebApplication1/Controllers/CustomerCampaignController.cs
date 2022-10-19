using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.models.databasemodels;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    public class CustomerCampaignController : Controller
    {
        private readonly MMSPLContext _context;

        public CustomerCampaignController(MMSPLContext context)
        {
            _context = context;
        }

        // GET: CustomerCampaign
        public async Task<IActionResult> Index()
        {
            var mMSPLContext = _context.CustomerCampaigns.Include(c => c.IdCampaignNavigation).Include(c => c.IdCustomerNavigation);
            return View(await mMSPLContext.ToListAsync());
        }

        // GET: CustomerCampaign/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCampaign = await _context.CustomerCampaigns
                .Include(c => c.IdCampaignNavigation)
                .Include(c => c.IdCustomerNavigation)
                .FirstOrDefaultAsync(m => m.IdCustomer == id);
            if (customerCampaign == null)
            {
                return NotFound();
            }

            return View(customerCampaign);
        }

        // GET: CustomerCampaign/Create
        public IActionResult Create()
        {
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Name");
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "FullName");
            return View();
        }

        // POST: CustomerCampaign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCustomer,IdCampaign,OkToEmail,OkToThirdParty")] CustomerCampaign customerCampaign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerCampaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description", customerCampaign.IdCampaign);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Address", customerCampaign.IdCustomer);
            return View(customerCampaign);
        }

        // GET: CustomerCampaign/Edit/5
        public async Task<IActionResult> Edit(int campid, int cmid)
        {
            if (campid == 0 || cmid == 0)
            {
                return NotFound();
            }

            var customerCampaign = await _context.CustomerCampaigns
                .Include(c => c.IdCampaignNavigation)
                .Include(c => c.IdCustomerNavigation)
                .FirstOrDefaultAsync(m => m.IdCustomer == cmid && m.IdCampaign == campid);

            if (customerCampaign == null)
            {
                return NotFound();
            }

            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description", customerCampaign.IdCampaign);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Address", customerCampaign.IdCustomer);
            return View(customerCampaign);
        }

        // POST: CustomerCampaign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdCustomer,IdCampaign,OkToEmail,OkToThirdParty")] CustomerCampaign customerCampaign)
        {
            if (customerCampaign == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerCampaign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerCampaignExists(customerCampaign.IdCustomer))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Campaigns", new {id = customerCampaign.IdCampaign});
            }
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description", customerCampaign.IdCampaign);
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Address", customerCampaign.IdCustomer);
            return View(customerCampaign);
        }

        // GET: CustomerCampaign/Delete/5
        public async Task<IActionResult> Delete(int campid, int cmid)
        {
            if (campid == 0 || cmid == 0)
            {
                return NotFound();
            }

            var customerCampaign = await _context.CustomerCampaigns
                .Include(c => c.IdCampaignNavigation)
                .Include(c => c.IdCustomerNavigation)
                .FirstOrDefaultAsync(m => m.IdCustomer == cmid && m.IdCampaign == campid);
            if (customerCampaign == null)
            {
                return NotFound();
            }

            return View(customerCampaign);
        }

        // POST: CustomerCampaign/Delete?campid=5;cmid=5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdCampaign, int IdCustomer)
        {
            var customerCampaign = await _context.CustomerCampaigns.FirstOrDefaultAsync(x => x.IdCampaign.Equals(IdCampaign) && x.IdCustomer.Equals(IdCustomer));
            _context.CustomerCampaigns.Remove(customerCampaign);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Campaigns", new { id = IdCampaign});
        }

        private bool CustomerCampaignExists(int id)
        {
            return _context.CustomerCampaigns.Any(e => e.IdCustomer == id);
        }

        // POST: CustomerCampaign/AddCustomers/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomers([FromRoute]int id, List<int> customerId)
        {
            if (ModelState.IsValid)
            {
                foreach (int cid in customerId)
                {
                    
                    _context.Add(new CustomerCampaign 
                    {
                        IdCampaign = id,
                        IdCustomer = cid,
                        //OkToEmail = cms.OkToEmail,
                        //OkToThirdParty = cms.OkTo3Pty
                    });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Campaigns", new { id });
           }
           
           return Ok("Błąd");
           
        }
    }
}
