using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HeroesWeb.Data;
using HeroesWeb.Models;

namespace HeroesWeb.Pages_Heroes
{
    public class IndexModel : PageModel
    {
        private readonly HeroesContext _context;

        public IndexModel(HeroesContext context)
        {
            _context = context;
        }

        public IList<Heroes> Heroes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Heroes = await _context.Heroes.ToListAsync();
        }
    }
}
