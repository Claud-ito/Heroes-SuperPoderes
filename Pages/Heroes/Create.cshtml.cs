using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HeroesWeb.Data;
using HeroesWeb.Models;

namespace HeroesWeb.Pages_Heroes
{
    public class CreateModel : PageModel
    {
        private readonly HeroesContext _context;

        public CreateModel(HeroesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Heroes Heroes { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Heroes.Add(Heroes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
