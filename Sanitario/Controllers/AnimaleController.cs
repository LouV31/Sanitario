using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    public class AnimaleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimaleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animale
        public async Task<IActionResult> Index()
        {
            return View(await _context.Animali.ToListAsync());
        }

        // GET: Animale/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animale = await _context.Animali
                .FirstOrDefaultAsync(m => m.IdAnimale == id);
            if (animale == null)
            {
                return NotFound();
            }

            return View(animale);
        }

        // GET: Animale/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Tipologia,DataRegistrazione,DataNascita,ColoreMantello,CodiceFiscaleProprietario,Microchip")] Animale animale)
        {
            ModelState.Remove("Visite");
            if (ModelState.IsValid)
            {
                _context.Add(animale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animale);
        }

        // GET: Animale/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animale = await _context.Animali.FindAsync(id);
            if (animale == null)
            {
                return NotFound();
            }
            return View(animale);
        }

        // POST: Animale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAnimale,Nome,Tipologia,DataRegistrazione,DataNascita,ColoreMantello,CodiceFiscaleProprietario,Microchip")] Animale animale)
        {
            if (id != animale.IdAnimale)
            {
                return NotFound();
            }

            ModelState.Remove("Visite");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimaleExists(animale.IdAnimale))
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
            return View(animale);
        }

        // GET: Animale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animale = await _context.Animali
                .FirstOrDefaultAsync(m => m.IdAnimale == id);
            if (animale == null)
            {
                return NotFound();
            }

            return View(animale);
        }

        // POST: Animale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animale = await _context.Animali.FindAsync(id);
            if (animale != null)
            {
                _context.Animali.Remove(animale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimaleExists(int id)
        {
            return _context.Animali.Any(e => e.IdAnimale == id);
        }
    }
}
