using backend.Models;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<bool> Registrar(Usuario usuario);
        Task<bool> Login(Usuario usuario);
    }
}