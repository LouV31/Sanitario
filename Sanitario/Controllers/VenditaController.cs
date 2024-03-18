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

        [HttpGet]
        public async Task<IActionResult> GetVisite(int? idCliente)
        {
            try
            {
                if (idCliente == null)
                {
                    return BadRequest("Id cliente non fornito");
                }

                var visite = await _context.Visite
                            .Include(v => v.Animale)
                            .Where(v => v.Animale.IdCliente == idCliente && v.IsArchiviato == false)
                            .ToListAsync();

                // Costruisci una lista di oggetti con le informazioni necessarie
                var visitaData = visite.Select(v => new
                {
                    id = v.Id,
                    data = v.DataVisita,
                    esame = v.Esame,
                    nomeAnimale = v.Animale.Nome  // Aggiungi il nome dell'animale alla risposta
                });

                return Json(new { listaVisite = visitaData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCure(int? idVisita)
        {
            try
            {
                if (idVisita == null)
                {
                    return BadRequest("Id visita non fornito");
                }

                var cure = await _context.CurePrescritte
                            .Include(cp => cp.Prodotto)
                            .Where(cp => cp.IdVisita == idVisita)
                            .ToListAsync();

                // Costruisci una lista di oggetti con le informazioni necessarie
                var cureData = cure.Select(cp => new
                {
                    id = cp.IdCuraPrescritta,
                    prodotto = new
                    {
                        idProdotto = cp.IdProdotto,
                        nome = cp.Prodotto.Nome,
                        prezzo = cp.Prodotto.Prezzo
                    }
                });

                return Json(new { listaCure = cureData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        // POST: Vendita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,DataVendita")] Vendita vendita, List<int> idVisita)
        {
            ModelState.Remove("DettagliVendite");
            ModelState.Remove("Cliente");
            if (ModelState.IsValid)
            {
                var curePrescritte = await _context.CurePrescritte
                    .Include(cp => cp.Prodotto)
                    .ThenInclude(p => p.CurePrescritte)
                    .Where(cp => idVisita.Contains(cp.IdVisita))
                    .ToListAsync();
                double prezzoTotale = 0;
                foreach (var cura in curePrescritte)
                {
                    prezzoTotale += cura.Prodotto.Prezzo;
                }
                vendita.PrezzoTotale = prezzoTotale;
                _context.Add(vendita);
                await _context.SaveChangesAsync();
                foreach (var cura in curePrescritte)
                {
                    var dettaglioVendita = new DettagliVendita
                    {
                        IdVendita = vendita.IdVendita,
                        IdProdotto = cura.IdProdotto,
                    };
                    _context.DettagliVendite.Add(dettaglioVendita);
                }
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
