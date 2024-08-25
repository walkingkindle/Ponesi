using System.Security.Claims;

namespace PonesiWebApi.Models;

public class User
{
   public int Id { get; set; }

   public DateTime CreationDate { get; set; }

    public string Email { get; set; }

   public string  PasswordHash { get; set; }

   public string Username { get; set; } 

    


}