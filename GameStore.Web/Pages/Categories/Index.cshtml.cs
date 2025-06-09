using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameStore.Data.Models;
using Microsoft.AspNetCore.Authorization;



namespace GameStore.Web.Pages.Categories
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly GameStore.Data.Models.GameStoreDbContext _context;

        public IndexModel(GameStore.Data.Models.GameStoreDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
