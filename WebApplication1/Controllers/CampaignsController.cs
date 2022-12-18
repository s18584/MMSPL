using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.models.databasemodels;
using WebApplication1.Models.DTO;

namespace WebApplication1
{
    public class CampaignsController : Controller
    {
        private readonly MMSPLContext _context;

        public CampaignsController(MMSPLContext context)
        {
            _context = context;
        }

        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
            var mMSPLContext = _context.Campaigns
                                        .Include(c => c.IdContractorNavigation);
            return View(await mMSPLContext.ToListAsync());
        }

        // GET: Campaigns/Details/5
        [ResponseType(typeof(CampaignDetailsDto))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns
                .Include(c => c.IdContractorNavigation)
                .Include(c => c.CustomerCampaigns)
                    .ThenInclude(c => c.IdCustomerNavigation)
                .Include(c => c.SendingActions)
                    .ThenInclude(c => c.IdSendingActionTypeNavigation)
                .Include(c => c.Documents)
                    .ThenInclude(c => c.IdDocTypeNavigation)
                .Include(c => c.Costs)
                    .ThenInclude(c => c.IdCostTypeNavigation)
                .Include(c => c.Costs)
                    .ThenInclude(c => c.IdContractorNavigation)   
                .Where(c => c.Id == id)
                .Select(c => 
                   new CampaignDetailsDto()
                   {
                       Id = c.Id,
                       Name = c.Name,
                       Description = c.Description,
                       IdContractor = c.IdContractor,
                       Budget = c.Budget,
                       IdContractorNavigation = c.IdContractorNavigation,
                       Costs = c.Costs,
                       CustomerCampaigns = c.CustomerCampaigns,
                       Documents = c.Documents,
                       Promocodes = c.Promocodes,
                       SendingActions = c.SendingActions,                     

                       UsersCountData = c.CustomerCampaigns.Count,
                       CostSumData = c.Costs.Sum(x => x.Amount)
                   }
                )                 
                .FirstOrDefaultAsync(c => c.Id == id);

            if (campaign == null)
            {
                return NotFound();
            }
            
            var costGroupData = campaign.Costs
                                .GroupBy(c => c.IdCostTypeNavigation.Name)
                                .Select(g => new GroupRaportDataDto
                                { 
                                    Key = g.Key, 
                                    Value = g.Sum(x => x.Amount)
                                })
                                .ToList();

            var sendingActionsGroupData = campaign.SendingActions
                                            .GroupBy(c => c.IdSendingActionTypeNavigation.Name)
                                            .Select(s => new GroupRaportDataDto
                                            {
                                                Key = s.Key,
                                                Value = s.Count()
                                            })
                                            .ToList();

            ViewData["costGroupData"] = costGroupData; 
            ViewData["sendingActionsGroupData"] = sendingActionsGroupData;
            
            return View(campaign);
        }

        // GET: Campaigns/Create
        public IActionResult Create(int? ctr)
        {
            if (ctr == null)
            {
                ViewData["IdContractor"] = new SelectList(_context.Contractors, "Id", "Name");
            }
            else
            {
                ViewData["IdContractor"] = new SelectList(_context.Contractors, "Id", "Name", ctr);
            }
            
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Budget,Description,IdContractor")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdContractor"] = new SelectList(_context.Contractors, "Id", "Name", campaign.IdContractor);
            return View(campaign);
        }

        // GET: Campaigns/AddCustomers/5
        public async Task<IActionResult> AddCustomers(int id)
        {
            var customers = _context.Customers.Join(_context.CustomerCampaigns,
                                                        cm => cm.Id,
                                                        cc => cc.IdCustomer,
                                                        (cm, cc) => new { cm, cc })
                                                    .Where(x => x.cc.IdCampaign.Equals(id))
                                                    .GroupBy(x => x.cc.IdCustomer)
                                                    .Select(x => x.Key);
                                                    
            /*
                                                    .Select(x => new Customer
                                                    {
                                                        Id = x.cm.Id,
                                                        FirstName = x.cm.FirstName,
                                                        LastName = x.cm.LastName,
                                                        Email = x.cm.Email,
                                                        BirthDate = x.cm.BirthDate,
                                                        Address = x.cm.Address,
                                                        City = x.cm.City,
                                                        PostCode = x.cm.PostCode
                                                    }).Distinct(); */

            var customersRes = _context.Customers.Where(x => !customers.Contains(x.Id))
                                                    .Select(x => new Customer
                                                    {
                                                        Id = x.Id,
                                                        FirstName = x.FirstName,
                                                        LastName = x.LastName,
                                                        Email = x.Email,
                                                        BirthDate = x.BirthDate,
                                                        Address = x.Address,
                                                        City = x.City,
                                                        PostCode = x.PostCode
                                                    }); 

            ViewData["RouteId"] = id;
            return View(await customersRes.ToListAsync());
        }

        // GET: Campaigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            ViewData["IdContractor"] = new SelectList(_context.Contractors, "Id", "Name", campaign.IdContractor);
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Budget,Description,IdContractor")] Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(campaign.Id))
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
            ViewData["IdContractor"] = new SelectList(_context.Contractors, "Id", "Name", campaign.IdContractor);
            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns
                .Include(c => c.IdContractorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignExists(int id)
        {
            return _context.Campaigns.Any(e => e.Id == id);
        }
    }
}
