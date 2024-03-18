using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    public class MedicinaleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicinaleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medicinale
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Medicinali.Include(m => m.Cassetto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Medicinale/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinale = await _context.Medicinali
                .Include(m => m.Cassetto)
                .FirstOrDefaultAsync(m => m.IdMedicinale == id);
            if (medicinale == null)
            {
                return NotFound();
            }

            return View(medicinale);
        }

        // GET: Medicinale/Create
        public IActionResult Create()
        {
            ViewData["IdCassetto"] = new SelectList(_context.Cassetti, "IdCassetto", "IdCassetto");
            return View();
        }

        // POST: Medicinale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descrizione,Prezzo,IdCassetto")] Medicinale medicinale)
        {
            ModelState.Remove("Cassetto");
            ModelState.Remove("CurePrescritte");
            ModelState.Remove("DettagliVendite");
            if (ModelState.IsValid)
            {
                _context.Add(medicinale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCassetto"] = new SelectList(_context.Cassetti, "IdCassetto", "IdCassetto", medicinale.IdCassetto);
            return View(medicinale);
        }

        // GET: Medicinale/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinale = await _context.Medicinali.FindAsync(id);
            if (medicinale == null)
            {
                return NotFound();
            }
            ViewData["IdCassetto"] = new SelectList(_context.Cassetti, "IdCassetto", "IdCassetto", medicinale.IdCassetto);
            return View(medicinale);
        }

        // POST: Medicinale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMedicinale,Nome,Descrizione,Prezzo,IdCassetto")] Medicinale medicinale)
        {
            if (id != medicinale.IdMedicinale)
            {
                return NotFound();
            }

            ModelState.Remove("Cassetto");
            ModelState.Remove("CurePrescritte");
            ModelState.Remove("DettagliVendite");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicinale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicinaleExists(medicinale.IdMedicinale))
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
            ViewData["IdCassetto"] = new SelectList(_context.Cassetti, "IdCassetto", "IdCassetto", medicinale.IdCassetto);
            return View(medicinale);
        }

        // GET: Medicinale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinale = await _context.Medicinali
                .Include(m => m.Cassetto)
                .FirstOrDefaultAsync(m => m.IdMedicinale == id);
            if (medicinale == null)
            {
                return NotFound();
            }

            return View(medicinale);
        }

        // POST: Medicinale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicinale = await _context.Medicinali.FindAsync(id);
            if (medicinale != null)
            {
                _context.Medicinali.Remove(medicinale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicinaleExists(int id)
        {
            return _context.Medicinali.Any(e => e.IdMedicinale == id);
        }
    }
}
