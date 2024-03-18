using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    public class VenditaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenditaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vendita
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vendite.Include(v => v.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vendita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendita = await _context.Vendite
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.IdVendita == id);
            if (vendita == null)
            {
                return NotFound();
            }

            return View(vendita);
        }

        // GET: Vendita/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "NomeCompleto");

            return View();
        }

        public IActionResult GetVisite(int idCliente)
        {
            var visite = _context.Animali
                                .Include(a => a.Visite)
                                .Where(a => a.IdCliente == idCliente)
                                .SelectMany(a => a.Visite) // Per ottenere tutte le visite degli animali del cliente
                                .Select(v => new { v.Id, v.DataVisita }) // Seleziona solo id e nome delle visite
                                .ToList();

            return Json(new { listaVisite = visite });
        }

        // POST: Vendita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,DataVendita,PrezzoTotale")] Vendita vendita)
        {
            ModelState.Remove("DettagliVendite");
            if (ModelState.IsValid)
            {
                _context.Add(vendita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "CodiceFiscale", vendita.IdCliente);
            return View(vendita);
        }

        // GET: Vendita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendita = await _context.Vendite.FindAsync(id);
            if (vendita == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "CodiceFiscale", vendita.IdCliente);
            return View(vendita);
        }

        // POST: Vendita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVendita,IdCliente,DataVendita,PrezzoTotale")] Vendita vendita)
        {
            if (id != vendita.IdVendita)
            {
                return NotFound();
            }

            ModelState.Remove("DettagliVendite");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenditaExists(vendita.IdVendita))
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
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "CodiceFiscale", vendita.IdCliente);
            return View(vendita);
        }

        // GET: Vendita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendita = await _context.Vendite
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.IdVendita == id);
            if (vendita == null)
            {
                return NotFound();
            }

            return View(vendita);
        }

        // POST: Vendita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendita = await _context.Vendite.FindAsync(id);
            if (vendita != null)
            {
                _context.Vendite.Remove(vendita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenditaExists(int id)
        {
            return _context.Vendite.Any(e => e.IdVendita == id);
        }
    }
}
