using GameStore.Business.Interfaces;
using GameStore.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameStore.Web.Pages.Games
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;
        private const int PageSize = 10;

        public IndexModel(IGameService gameService)
        {
            _gameService = gameService;
        }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }
        public IList<Game> Game { get; set; } = new List<Game>();

        public async Task<IActionResult> OnGetAsync()
        {
            var totalCount = await _gameService.GetTotalGamesCountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            Game = await _gameService.GetGamesAsync(PageNumber, PageSize);
            return Page();
        }

    }
}
