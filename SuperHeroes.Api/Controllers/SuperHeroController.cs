using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroes.Api.Data;
using SuperHeroes.Api.Models;

namespace SuperHeroes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroDbContext _dbContext;

        public SuperHeroController(SuperHeroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuperHeroes()
        {
            var result = await _dbContext.SuperHeroes.ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuperHero(SuperHero superHero)
        {
            if(superHero == null)
                return BadRequest(); 
            await _dbContext.SuperHeroes.AddAsync(superHero);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSuperHero(SuperHero superHero)
        {
            var dbHero = await _dbContext.SuperHeroes.FindAsync(superHero.Id);
            if(dbHero == null)
                return BadRequest("Hero not found");
            dbHero.Name = superHero.Name;
            dbHero.FirstName = superHero.FirstName; 
            dbHero.LastName = superHero.LastName;
            dbHero.Place = superHero.Place;
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }

        
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            var dbHero = await _dbContext.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found");

            _dbContext.SuperHeroes.Remove(dbHero);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }

    }
}
