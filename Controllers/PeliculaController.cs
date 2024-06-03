
using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private IPeliculaService _db = new PeliculaService();
        [HttpGet]
        public async Task<IActionResult> getAllMovies()
        {
            return Ok(await _db.GetPeliculas());
        }

        [HttpPost]
        public async Task<IActionResult> createMovies([FromBody] Pelicula pelicula)
        {

            await _db.insertPeliculas(pelicula);
            return Created("Created", pelicula);

        }
        [HttpGet("random/{cantidad}")]
        public async Task<IActionResult> GetRandomMovies(int cantidad)
        {
            return Ok(await _db.GetRandomPeliculas(cantidad));
        }

    }
}