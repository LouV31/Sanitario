using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    public class AnimaleSmarritoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimaleSmarritoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AnimaleSmarrito
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimaliSmarriti.ToListAsync());
        }

        // GET: AnimaleSmarrito/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animaleSmarrito = await _context.AnimaliSmarriti
                .FirstOrDefaultAsync(m => m.IdAnimaleSmarrito == id);
            if (animaleSmarrito == null)
            {
                return NotFound();
            }

            return View(animaleSmarrito);
        }

        // GET: AnimaleSmarrito/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimaleSmarrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Tipologia,DataRegistrazione,DataNascita,ColoreMantello,CodiceFiscaleProprietario,Microchip,DataInizioRicovero,Foto")] AnimaleSmarrito animaleSmarrito)
        {
            ModelState.Remove("");
            if (ModelState.IsValid)
            {
                _context.Add(animaleSmarrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animaleSmarrito);
        }

        // GET: AnimaleSmarrito/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animaleSmarrito = await _context.AnimaliSmarriti.FindAsync(id);
            if (animaleSmarrito == null)
            {
                return NotFound();
            }
            return View(animaleSmarrito);
        }

        // POST: AnimaleSmarrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAnimaleSmarrito,Nome,Tipologia,DataRegistrazione,DataNascita,ColoreMantello,CodiceFiscaleProprietario,Microchip,DataInizioRicovero,Foto")] AnimaleSmarrito animaleSmarrito)
        {
            if (id != animaleSmarrito.IdAnimaleSmarrito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animaleSmarrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimaleSmarritoExists(animaleSmarrito.IdAnimaleSmarrito))
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
            return View(animaleSmarrito);
        }

        // GET: AnimaleSmarrito/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animaleSmarrito = await _context.AnimaliSmarriti
                .FirstOrDefaultAsync(m => m.IdAnimaleSmarrito == id);
            if (animaleSmarrito == null)
            {
                return NotFound();
            }

            return View(animaleSmarrito);
        }

        // POST: AnimaleSmarrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animaleSmarrito = await _context.AnimaliSmarriti.FindAsync(id);
            if (animaleSmarrito != null)
            {
                _context.AnimaliSmarriti.Remove(animaleSmarrito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimaleSmarritoExists(int id)
        {
            return _context.AnimaliSmarriti.Any(e => e.IdAnimaleSmarrito == id);
        }
    }
}
