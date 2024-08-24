namespace PonesiWebApi.Interfaces
{
    public interface IPasswordHasher
    {

        string CreateHashedPassword(string password);

        bool Verify(string password,string passwordHash);


    }
}
