using Microsoft.EntityFrameworkCore;
using SuperHeroes.Api.Models;

namespace SuperHeroes.Api.Data
{
    public class SuperHeroDbContext : DbContext
    {
        public SuperHeroDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<SuperHero> SuperHeroes => Set<SuperHero>();
    }
}
