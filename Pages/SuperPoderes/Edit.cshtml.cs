using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HeroesWeb.Data;
using HeroesWeb.Models;

namespace HeroesWeb.Pages_SuperPoderes
{
    public class EditModel : PageModel
    {
        private readonly HeroesContext _context;

        public EditModel(HeroesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SuperPoderes SuperPoderes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superpoderes = await _context.SuperPoderes.FirstOrDefaultAsync(m => m.Id == id);
            if (superpoderes == null)
            {
                return NotFound();
            }
            SuperPoderes = superpoderes;
            ViewData["HeroeId"] = new SelectList(_context.Heroes, "Id", "Nombre", SuperPoderes.HeroeId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!await _context.Heroes.AnyAsync(h => h.Id == SuperPoderes.HeroeId))
            {
                ModelState.AddModelError("SuperPoderes.HeroeId", "Seleccione un heroe valido.");
            }

            if (!ModelState.IsValid)
            {
                ViewData["HeroeId"] = new SelectList(_context.Heroes, "Id", "Nombre", SuperPoderes.HeroeId);
                return Page();
            }

            _context.Attach(SuperPoderes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.SuperPoderes.AnyAsync(e => e.Id == SuperPoderes.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
