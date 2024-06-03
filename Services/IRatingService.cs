using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface IRatingService
    {
        Task InsertRating(Rating rating);
        Task UpdateRating(Rating rating);
        Task<List<Rating>> GetAllRatings();
        Task<bool> RatingExists(string usuarioId, string peliculaId);
    }
}