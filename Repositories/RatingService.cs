using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Repositories
{
    public class RatingService : IRatingService
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Rating> _collection;

        public RatingService()
        {
            _collection = _repository.db.GetCollection<Rating>("Ratings");
        }

        public async Task<List<Rating>> GetAllRatings()
        {
            return await _collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task InsertRating(Rating rating)
        {
            await _collection.InsertOneAsync(rating);
        }

        public async Task UpdateRating(Rating rating)
        {
            var filtro = Builders<Rating>.Filter.Eq(s => s.Id, rating.Id);
            await _collection.ReplaceOneAsync(filtro, rating);
        }

        public async Task<bool> RatingExists(string usuarioId, string peliculaId)
        {
            var filter =
                Builders<Rating>.Filter.Eq(r => r.UsuarioId, usuarioId)
                & Builders<Rating>.Filter.Eq(r => r.PeliculaId, peliculaId);
            var existingRating = await _collection.Find(filter).FirstOrDefaultAsync();
            return existingRating != null;
        }
    }
}
