using Microsoft.AspNetCore.Mvc.RazorPages;
using Shortify.Context;
using Shortify.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shortify.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ContextLinks _context;
        public string redirect { get; set; }
        public IndexModel(ContextLinks context)
        {
            _context = context;
        }

        public void OnGet()
        {
            redirect = "";
        }
        public async Task OnPostAsync(string link)
        {
            Links temp = _context.Links.Where(i => i.LongLink == link).SingleOrDefault();
            if (link!="" && temp == null)
            {
                Links links = new Links() { ShortLink = GenerateShort(), LongLink = link };
                await _context.Links.AddAsync(links);
                await _context.SaveChangesAsync();
                redirect = links.ShortLink;
            }
            else
            {
                redirect = temp.ShortLink;
            }

        }
        private string GenerateShort()
        {
            string url = "";
            Enumerable.Range(48, 75).Where(i => i < 58 || i > 64 && i < 91 || i > 96).OrderBy(o => new Random().Next()).ToList().ForEach(i => url += Convert.ToChar(i));
            return url.Substring(new Random().Next(0, url.Length), new Random().Next(2, 6));
            //if this code crush, i will be stuiped
        }
    }
}