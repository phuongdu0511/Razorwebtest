using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorwebTest.Models;

namespace RazorwebTest.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly RazorwebTest.Models.MyBlogContext _context;

        public IndexModel(RazorwebTest.Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }
        public const int ITEMS_PER_PAGE = 5;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPages { get; set; }

        public int countPages { get; set; }

        public async Task OnGetAsync(string SearchString)
        {
            int totalArticle = await _context.articles.CountAsync();
            countPages = (int)Math.Ceiling((double)totalArticle / ITEMS_PER_PAGE);

            if (currentPages < 1)
                currentPages = 1;
            if (currentPages > countPages)
                currentPages = countPages;

            //Article = await _context.articles.ToListAsync();
            if (!string.IsNullOrEmpty(SearchString))
            {
                Article = _context.articles.
                    Where(a => a.Title.Contains(SearchString)).
                    Skip((currentPages - 1) * ITEMS_PER_PAGE).
                    Take(ITEMS_PER_PAGE).
                    OrderByDescending(p =>p.Created).
                    ToList();
            }
            else
            {
                Article = await _context.articles.
                    Skip((currentPages - 1) * 10).
                    Take(ITEMS_PER_PAGE).
                    OrderByDescending(a => a.Created).ToListAsync();
            }

        }
    }
}
