namespace PonesiWebApi.Authentication
{
    public class AuthenticationConfiguration
    {
        private readonly IConfiguration _configuration;

        public AuthenticationConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            PrivateKey = _configuration["Authentication:JwtSecret"];
            ExpireDays = _configuration["Authentication:JwtExpireDays"];
        }

        public string PrivateKey { get; set; }

        public string ExpireDays{ get;set; }
    }
}
