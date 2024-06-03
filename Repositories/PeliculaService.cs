using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace backend.Repositories
{
    public class PeliculaService : IPeliculaService
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Pelicula> _collection;

        private IMongoCollection<Genero> _generoCollection;

        public PeliculaService()
        {
            _collection = _repository.db.GetCollection<Pelicula>("Peliculas");
            _generoCollection = _repository.db.GetCollection<Genero>("Generos");
        }


        public async Task<List<PeliculaResponse>> GetPeliculas()
        {
            var peliculas = await _collection.FindAsync(new BsonDocument()).Result.ToListAsync();
            var generos = await _generoCollection.FindAsync(new BsonDocument()).Result.ToListAsync();

            var generoDict = generos.ToDictionary(g => g.Id, g => new GeneroDto
            {
                Id = g.Id.ToString(),
                Nombre = g.nombre
            });

            var peliculasResponse = peliculas.Select(pelicula => new PeliculaResponse
            {
                Id = pelicula.Id,
                Titulo = pelicula.titulo,
                VideoUrl = pelicula.VideoUrl,
                Generos = pelicula.GeneroIds
                    .Where(id => generoDict.ContainsKey(id))
                    .Select(id => generoDict[id])
                    .ToList()
            }).ToList();

            return peliculasResponse;
        }

        public async Task<List<PeliculaResponse>> GetRandomPeliculas(int cantidad)

        {
            var peliculas = await _collection.AsQueryable().Sample(cantidad).ToListAsync();
            var generos = await _generoCollection.FindAsync(new BsonDocument()).Result.ToListAsync();

            var generoDict = generos.ToDictionary(g => g.Id, g => new GeneroDto
            {
                Id = g.Id.ToString(),
                Nombre = g.nombre
            });

            var peliculasResponse = peliculas.Select(pelicula => new PeliculaResponse
            {
                Id = pelicula.Id,
                Titulo = pelicula.titulo,
                VideoUrl = pelicula.VideoUrl,
                Generos = pelicula.GeneroIds
                    .Where(id => generoDict.ContainsKey(id))
                    .Select(id => generoDict[id])
                    .ToList()
            }).ToList();

            return peliculasResponse;
        }

        public async Task insertPeliculas(Pelicula pelicula)
        {
            await _collection.InsertOneAsync(pelicula);
        }
    }
}