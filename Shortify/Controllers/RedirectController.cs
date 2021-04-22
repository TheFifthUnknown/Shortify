using Microsoft.AspNetCore.Mvc;
using Shortify.Context;
using Shortify.Models;
using System;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Shortify.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly ContextLinks _context; 
        public RedirectController(ContextLinks context)
        {
            _context = context;
        }
        // GET /things
        [HttpGet("/{ShortLink}")]
        public async Task<IActionResult> Get([FromRoute]string ShortLink)
        {
            Links temp =await _context.Links.FindAsync(ShortLink);
            UriBuilder uri = new UriBuilder(temp.LongLink);
            return (temp != null) ? Redirect(uri.ToString()) : BadRequest("There isn't");
        }

    }
}
