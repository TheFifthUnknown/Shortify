using System.ComponentModel.DataAnnotations;

namespace Shortify.Models
{
    public class Links
    {
        [Key, Url]
        public string ShortLink { get; set; }
        [Url]
        public string LongLink { get; set; }
    }
}
