using backend.DTOs;
using backend.Models;

namespace backend.Services
{
    public interface IPeliculaService
    {
        Task<List<PeliculaResponse>> GetPeliculas();
        Task<List<PeliculaResponse>> GetRandomPeliculas(int cantidad);
        Task insertPeliculas(Pelicula pelicula);
    }
}