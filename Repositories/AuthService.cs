using backend.Data;
using backend.Models;
using backend.Services;
using MongoDB.Driver;

namespace backend.Repositories
{
    public class AuthService : IAuthService
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Usuario> _collection;

        public AuthService()
        {
            _collection = _repository.db.GetCollection<Usuario>("usuarios");
        }
        public async Task<bool> Login(Usuario usuario)
        {
            var existingUser = await _collection.Find(u => u.nombre == usuario.nombre && u.contrasena == usuario.contrasena).FirstOrDefaultAsync();
            usuario.Id = existingUser.Id;
            return existingUser != null;
        }

        public async Task<bool> Registrar(Usuario usuario)
        {
            var existingUser = await _collection.Find(u => u.nombre == usuario.nombre).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return false; // Nombre de usuario ya en uso
            }

            await _collection.InsertOneAsync(usuario);

            return true; // Registro exitoso

        }
    }
}