using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HeyRed.Mime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using WebApplication1.models.databasemodels;
using WebApplication1.Models.DTO;


namespace WebApplication1.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly MMSPLContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;


        public DocumentsController(MMSPLContext context)
        {
            _context = context;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var mMSPLContext = _context.Documents.Include(d => d.IdDocTypeNavigation).Include(d => d.IdCampaignNavigation);

            var list = await mMSPLContext.ToListAsync();
            return View(list);
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.IdDocTypeNavigation)
                .Include(d => d.IdCampaignNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            ViewData["IdDocType"] = new SelectList(_context.DocTypes, "Id", "Name");
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdDocType,Path,Description,IdCampaign")] AddFileModel model)
        {
            Document document = new Document();
            if (ModelState.IsValid)
            {
                
                //Getting file meta data
                var fileName = Path.GetFileName(model.Path.FileName);
                var contentType = model.Path.ContentType;

                document.Id = model.Id;
                document.IdDocType = model.IdDocType;
                document.IdCampaign = model.IdCampaign;

                var uniqueFileName = GetUniqueFileName(model.Path.FileName);
                //var uploads = Path.Combine(@"C:\Users\lasoc\source\repos\MMSPL\WebApplication1\wwwroot\", "Files");
                var uploads = Path.Combine(@"C:\PJATK\inż\WebApplication1\wwwroot\", "Files");
                var filePath = Path.Combine(uploads, uniqueFileName);
                var fs = new FileStream(filePath, FileMode.Create);
                model.Path.CopyTo(fs);


                document.Path = filePath;
                document.Description = model.Description;
                _context.Add(document);
                fs.Close();
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCampaign"] = new SelectList(_context.DocTypes, "Id", "Name", model.IdCampaign);
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description", model.IdCampaign);
            return View(document);
        }

        public async Task<IActionResult> DownloadFile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(document.Path);
            string mimeType = MimeTypesMap.GetMimeType(Path.GetFileName(document.Path));


            return File(fileBytes, mimeType, Path.GetFileName(document.Path));



        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["IdDocType"] = new SelectList(_context.DocTypes, "Id", "Name", document.IdDocType);
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description", document.IdCampaign);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdDocType,Description,Path,IdCampaign")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Id))
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
            ViewData["IdCampaign"] = new SelectList(_context.DocTypes, "Id", "Name", document.IdCampaign);
            ViewData["IdCampaign"] = new SelectList(_context.Campaigns, "Id", "Description", document.IdCampaign);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.IdDocTypeNavigation)
                .Include(d => d.IdCampaignNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }




        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Guid.NewGuid().ToString()
                   + Path.GetExtension(fileName);
        }
    }
}
