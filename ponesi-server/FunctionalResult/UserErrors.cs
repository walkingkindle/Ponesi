namespace PonesiWebApi.FunctionalResult{
    public static class UserErrors
    {
        public static readonly Error UserExists =  new Error("Users.UserExist", "User with the specified username already exists in the database.");
    }
}
