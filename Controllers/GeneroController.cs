using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController:ControllerBase
    {
        private IGeneroService _db = new GeneroService();
        
        [HttpGet]
        public async Task<IActionResult> GetAllGeneros()
        {
            var generos = await _db.GetAllGenero();
            return Ok(generos);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGenero([FromBody] Genero genero)
        {
            await _db.InsertGenero(genero);
            return CreatedAtAction(nameof(GetAllGeneros), new{id = genero.Id});

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenero([FromBody] Genero genero, string id){

            genero.Id = id;
            await _db.UpdateGenero(genero);
            return Created("Created",true);
            
        }


        
    }
    
}