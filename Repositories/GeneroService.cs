using backend.Data;
using backend.Models;
using backend.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Repositories
{
    public class GeneroService : IGeneroService
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Genero> _collection;

        public GeneroService(){
            _collection = _repository.db.GetCollection<Genero>("Generos");
        }

        public async Task<List<Genero>> GetAllGenero()
        { 
            return await _collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }


        public async Task InsertGenero(Genero genero)
        {
            await _collection.InsertOneAsync(genero);
        }

        public async Task UpdateGenero(Genero genero)
        {
            var filtro = Builders<Genero>.Filter.Eq(s => s.Id,genero.Id);
            await _collection.ReplaceOneAsync(filtro,genero);
        }
    }
}