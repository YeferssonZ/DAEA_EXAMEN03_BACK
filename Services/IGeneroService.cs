using System.Net.Http.Headers;
using backend.Models;

namespace backend.Services
{
    public interface IGeneroService
    {
        Task InsertGenero(Genero genero);
        Task UpdateGenero(Genero genero);
        Task <List<Genero>> GetAllGenero();
    }
}
