using PonesiWebApi.Interfaces;

namespace PonesiWebApi.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string CreateHashedPassword(string password)
        {
            
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        public bool Verify(string password,string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
