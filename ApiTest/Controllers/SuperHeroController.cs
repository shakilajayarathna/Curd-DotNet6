using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> heroes = new List<SuperHero>
            {
                    new SuperHero {
                                  Id=1,
                                  Name="SpiderMan",
                                  FirstName="Peter",
                                  LastName="Parker",
                                  Place="NewYork"
                                },
                    new SuperHero {
                                  Id=2,
                                  Name="IronMan",
                                  FirstName="Tony",
                                  LastName="Stark",
                                  Place="Long Island"
                                }
            };

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            //return Ok(heroes);
            return Ok(await _context.SuperHero.ToListAsync());
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            //var hero = heroes.Find(a => a.Id == id);
            var hero = await _context.SuperHero.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }



        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            //heroes.Add(hero);
            _context.SuperHero.Add(hero);
            await _context.SaveChangesAsync();

            //return Ok(heroes);
            return Ok(await _context.SuperHero.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            //var hero = heroes.Find(a => a.Id == request.Id);
            var hero =await _context.SuperHero.FindAsync(request.Id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;   
            hero.Place = request.Place;

            await _context.SaveChangesAsync();

            //return Ok(heroes);
            return Ok(await _context.SuperHero.ToListAsync()); 
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            //var hero = heroes.Find(a => a.Id == id);
            var hero = await _context.SuperHero.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }

            //heroes.Remove(hero);
            _context.SuperHero.Remove(hero);
            await _context.SaveChangesAsync();

            //return Ok(heroes);
            return Ok(await _context.SuperHero.ToListAsync());
        }
    }
}
