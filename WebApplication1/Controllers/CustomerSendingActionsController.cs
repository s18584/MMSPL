using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.models.databasemodels;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class CustomerSendingActionsController : Controller
    {
        private readonly MMSPLContext _context;

        public CustomerSendingActionsController(MMSPLContext context)
        {
            _context = context;
        }

        // GET: CustomerSendingActions
        public async Task<IActionResult> Index()
        {
            var mMSPLContext = _context.CustomerSendingActions.Include(c => c.IdCustomerNavigation).Include(c => c.IdSendingActionNavigation);
            return View(await mMSPLContext.ToListAsync());
        }

        // GET: CustomerSendingActions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerSendingAction = await _context.CustomerSendingActions
                .Include(c => c.IdCustomerNavigation)
                .Include(c => c.IdSendingActionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerSendingAction == null)
            {
                return NotFound();
            }

            return View(customerSendingAction);
        }

        // GET: CustomerSendingActions/Create
        public IActionResult Create()
        {
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Address");
            ViewData["IdSendingAction"] = new SelectList(_context.SendingActions, "Id", "Description");
            return View();
        }

        // POST: CustomerSendingActions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCustomer,IdSendingAction,IsSend")] CustomerSendingAction customerSendingAction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerSendingAction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Address", customerSendingAction.IdCustomer);
            ViewData["IdSendingAction"] = new SelectList(_context.SendingActions, "Id", "Description", customerSendingAction.IdSendingAction);
            return View(customerSendingAction);
        }

        // GET: CustomerSendingActions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerSendingAction = await _context.CustomerSendingActions.FindAsync(id);
            if (customerSendingAction == null)
            {
                return NotFound();
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Address", customerSendingAction.IdCustomer);
            ViewData["IdSendingAction"] = new SelectList(_context.SendingActions, "Id", "Description", customerSendingAction.IdSendingAction);
            return View(customerSendingAction);
        }

        // POST: CustomerSendingActions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCustomer,IdSendingAction,IsSend")] CustomerSendingAction customerSendingAction)
        {
            if (id != customerSendingAction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerSendingAction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerSendingActionExists(customerSendingAction.Id))
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
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "Id", "Address", customerSendingAction.IdCustomer);
            ViewData["IdSendingAction"] = new SelectList(_context.SendingActions, "Id", "Description", customerSendingAction.IdSendingAction);
            return View(customerSendingAction);
        }

        // GET: CustomerSendingActions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerSendingAction = await _context.CustomerSendingActions
                .Include(c => c.IdCustomerNavigation)
                .Include(c => c.IdSendingActionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerSendingAction == null)
            {
                return NotFound();
            }

            return View(customerSendingAction);
        }

        // POST: CustomerSendingActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerSendingAction = await _context.CustomerSendingActions.FindAsync(id);
            _context.CustomerSendingActions.Remove(customerSendingAction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerSendingActionExists(int id)
        {
            return _context.CustomerSendingActions.Any(e => e.Id == id);
        }

        // GET: CustomerSendingActions/Send/5
        public async Task<IActionResult> Send(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customers = _context.Customers.Join(_context.CustomerCampaigns,
                                                        cm => cm.Id,
                                                        cc => cc.IdCustomer,
                                                        (cm, cc) => new { cm, cc })
                                                    .Join(_context.Campaigns,
                                                        cc => cc.cc.IdCampaign,
                                                        cmp => cmp.Id,
                                                        (cc, cmp) => new { cc, cmp })
                                                    .Join(_context.SendingActions,
                                                        cmp => cmp.cmp.Id,
                                                        sa => sa.IdCampaign,
                                                        (cmp, sa) => new { cmp, sa })
                                                    .Where(x => x.sa.Id.Equals(id) && x.cmp.cc.cc.OkToEmail == 1)
                                                    .Select(x => new Customer
                                                    {
                                                        Id = x.cmp.cc.cm.Id,
                                                        FirstName = x.cmp.cc.cm.FirstName,
                                                        LastName = x.cmp.cc.cm.LastName,
                                                        Email = x.cmp.cc.cm.Email,
                                                        BirthDate = x.cmp.cc.cm.BirthDate,
                                                        Address = x.cmp.cc.cm.Address,
                                                        City = x.cmp.cc.cm.City,
                                                        PostCode = x.cmp.cc.cm.PostCode
                                                    });                                                   

            ViewData["RouteId"] = id;

            return View(await customers.ToListAsync());
        }

        // POST: CustomerCampaign/AddCustomers/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send([FromRoute] int id, List<int> customerId)
        {
            if (ModelState.IsValid)
            {
                var sendingAction = await _context.SendingActions.FirstOrDefaultAsync(x => x.Id == id);        // do poprawy - przekazanie całego obiektu przez model
                var customers = _context.Customers.Where(x => customerId.Contains(x.Id));                // to samo z listą klientów
                var email = new EmailService("serwer1311887.home.pl", 465, SecureSocketOptions.SslOnConnect);

                foreach (int cid in customerId)
                {
                    var customer = await customers.FirstOrDefaultAsync(x => x.Id == cid);
                    _context.Add(new CustomerSendingAction
                    {
                        IdCustomer = customer.Id,
                        IdSendingAction = id
                    });
                    email.SendAsync("MMSPL-Powiadomienia", customer.Email, sendingAction.EmailSubject, sendingAction.EmailBody);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Campaigns", new { id });
            }

            return Ok("Błąd");

        }

    }
}
