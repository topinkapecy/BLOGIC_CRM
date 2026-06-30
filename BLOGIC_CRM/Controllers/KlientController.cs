using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLOGIC_CRM.Models;
using BLOGIC_CRM.Data;

namespace BLOGIC_CRM.Controllers
{
    public class KlientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KlientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KLIENTS
        public async Task<IActionResult> Index()
        {
            var data = await _context.Klienti.ToListAsync();
            return View(data);
        }

        // GET: KLIENTS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klient = await _context.Klienti
                .Include(k => k.Smlouvy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // GET: KLIENTS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KLIENTS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Jmeno,Prijmeni,Email,Telefon,RodneCislo,Vek,Smlouvy,CeleJmeno")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klient);
        }

        // GET: KLIENTS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klient = await _context.Klienti.FindAsync(id);
            if (klient == null)
            {
                return NotFound();
            }
            return View(klient);
        }

        // POST: KLIENTS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Jmeno,Prijmeni,Email,Telefon,RodneCislo,Vek")] Klient klient)
        {
            if (id != klient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlientExists(klient.Id))
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
            return View(klient);
        }

        // GET: KLIENTS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klient = await _context.Klienti
                .Include(k => k.Smlouvy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klient == null)
            {
                return NotFound();
            }

            // Klient může mít navázané smlouvy - ty se smažou společně s ním (cascade delete)
            ViewBag.PocetSmluv = klient.Smlouvy.Count;

            return View(klient);
        }

        // POST: KLIENTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var klient = await _context.Klienti
                .Include(k => k.Smlouvy)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (klient != null)
            {
                _context.Klienti.Remove(klient);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool KlientExists(int? id)
        {
            return _context.Klienti.Any(e => e.Id == id);
        }
    }
}