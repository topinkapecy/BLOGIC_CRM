
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLOGIC_CRM.Models;
using BLOGIC_CRM.Data;

namespace BLOGIC_CRM.Controllers
{
    public class PoradceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoradceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PORADCES
        public async Task<IActionResult> Index()
        {
            var data = await _context.Poradci.ToListAsync();
            return View(data);
        }

        // GET: PORADCES/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poradce = await _context.Poradci
                .Include(p => p.SpravovaneSmlouvy)
                .Include(p => p.SmlouvaPoradce)
                    .ThenInclude(sp => sp.Smlouva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poradce == null)
            {
                return NotFound();
            }

            return View(poradce);
        }

        // GET: PORADCES/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PORADCES/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Jmeno,Prijmeni,Email,Telefon,RodneCislo,Vek,SmlouvaPoradce,SpravovaneSmlouvy,CeleJmeno")] Poradce poradce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poradce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poradce);
        }

        // GET: PORADCES/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poradce = await _context.Poradci.FindAsync(id);
            if (poradce == null)
            {
                return NotFound();
            }
            return View(poradce);
        }

        // POST: PORADCES/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Jmeno,Prijmeni,Email,Telefon,RodneCislo,Vek")] Poradce poradce)
        {
            if (id != poradce.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poradce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoradceExists(poradce.Id))
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
            return View(poradce);
        }

        // GET: PORADCES/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poradce = await _context.Poradci
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poradce == null)
            {
                return NotFound();
            }

            return View(poradce);
        }

        // POST: PORADCES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var poradce = await _context.Poradci.FindAsync(id);
            if (poradce != null)
            {
                _context.Poradci.Remove(poradce);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoradceExists(int? id)
        {
            return _context.Poradci.Any(e => e.Id == id);
        }
    }
}