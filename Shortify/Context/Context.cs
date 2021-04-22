using Microsoft.EntityFrameworkCore;
using Shortify.Models;

namespace Shortify.Context
{
    public class ContextLinks : DbContext
    {
        // https://metanit.com/sharp/aspnet5/29.9.php
        // use [key] in model links - https://stackoverflow.com/questions/48225989/the-entity-type-requires-a-primary-key-to-be-defined
        public DbSet<Links> Links { get; set; }

        public ContextLinks(DbContextOptions<ContextLinks> options):base(options)
        {
            Database.EnsureCreated();
        }   
    }
}
