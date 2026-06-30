
using BLOGIC_CRM.Data;
using BLOGIC_CRM.Models;
using BLOGIC_CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BLOGIC_CRM.Controllers
{
    public class SmlouvaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SmlouvaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SMLOUVAS
        public async Task<IActionResult> Index()
        {
            var smlouvy = await _context.Smlouvy
             .Include(s => s.Klient)
             .Include(s => s.SpravceSmlouvy)
             .ToListAsync();

            return View(smlouvy);
        }

        // GET: SMLOUVAS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smlouva = await _context.Smlouvy
                       .Include(s => s.Klient)
                       .Include(s => s.SpravceSmlouvy)
                       .Include(s => s.SmlouvaPoradce)
                           .ThenInclude(sp => sp.Poradce)
                       .FirstOrDefaultAsync(m => m.Id == id);

            if (smlouva == null)
            {
                return NotFound();
            }

            return View(smlouva);
        }

        // GET: SMLOUVAS/Create
        public IActionResult Create()
        {
            //seznam klientů a poradců pro výběr v dropdown menu
            var klientiList = _context.Klienti
            .Select(k => new SelectListItem
            {
                Value = k.Id.ToString(),
                Text = k.CeleJmeno
            }).ToList();

            var poradciList = _context.Poradci
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.CeleJmeno
                }).ToList();

            //Naplnění listů do ViewModelu
            var viewModel = new SmlouvaCreateViewModel
            {
                KlientiList = klientiList,
                PoradciList = poradciList
            };

            return View(viewModel);

        }

        // POST: SMLOUVAS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SmlouvaCreateViewModel viewModel)
        {

            if (viewModel.DatumPlatnosti < viewModel.DatumUzavreni)
            {
                ModelState.AddModelError("DatumPlatnosti", "Datum platnosti nesmí být dřívější než datum uzavření.");
            }


            if (!viewModel.VybraniUcastniciIds.Contains(viewModel.SpravceId))
            {
                viewModel.VybraniUcastniciIds.Add(viewModel.SpravceId);
            }

            if (ModelState.IsValid)
            {
                var novaSmlouva = new Smlouva
                {
                    EvidencniCislo = viewModel.EvidencniCislo,
                    DatumUzavreni = viewModel.DatumUzavreni,
                    DatumPlatnosti = viewModel.DatumPlatnosti,
                    DatumUkonceni = viewModel.DatumUkonceni,
                    KlientId = viewModel.KlientId,
                    SpravceSmlouvyId = viewModel.SpravceId
                };

                foreach (var poradceId in viewModel.VybraniUcastniciIds)
                {
                    novaSmlouva.SmlouvaPoradce.Add(new SmlouvaPoradce
                    {
                        PoradceId = poradceId
                    });
                }

                _context.Add(novaSmlouva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            viewModel.KlientiList = _context.Klienti.Select(k => new SelectListItem
            {
                Value = k.Id.ToString(),
                Text = k.CeleJmeno
            }).ToList();

            viewModel.PoradciList = _context.Poradci.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.CeleJmeno
            }).ToList();

            return View(viewModel);
        }


        // GET: SMLOUVAS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smlouva = await _context.Smlouvy.FindAsync(id);

            if (smlouva == null)
            {
                return NotFound();
            }
            return View(smlouva);
        }

        // POST: SMLOUVAS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,EvidencniCislo,KlientId,Klient,SpravceSmlouvyId,SpravceSmlouvy,DatumUzavreni,DatumPlatnosti,DatumUkonceni,SmlouvaPoradce")] Smlouva smlouva)
        {
            if (id != smlouva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(smlouva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SmlouvaExists(smlouva.Id))
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
            return View(smlouva);
        }

        // GET: SMLOUVAS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smlouva = await _context.Smlouvy
                .Include(s => s.Klient)
                .Include(s => s.SpravceSmlouvy)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (smlouva == null)
            {
                return NotFound();
            }

            return View(smlouva);
        }

        // POST: SMLOUVAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var smlouva = await _context.Smlouvy.FindAsync(id);
            if (smlouva != null)
            {
                _context.Smlouvy.Remove(smlouva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SmlouvaExists(int? id)
        {
            return _context.Smlouvy.Any(e => e.Id == id);
        }
    }
}
