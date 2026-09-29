using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HeroesWeb.Data;
using HeroesWeb.Models;

namespace HeroesWeb.Pages_Heroes
{
    public class DetailsModel : PageModel
    {
        private readonly HeroesContext _context;

        public DetailsModel(HeroesContext context)
        {
            _context = context;
        }

        public Heroes Heroes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroes = await _context.Heroes
                .Include(h => h.SuperPoderes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heroes == null)
            {
                return NotFound();
            }
                Heroes = heroes;

                return Page();
            }

            return NotFound();
        }
    }
}
