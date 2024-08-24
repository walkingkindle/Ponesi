namespace PonesiWebApi.FunctionalResult{
    public static class UserErrors
    {
        public static readonly Error UserExists =  new Error("Users.UserExist", "User with the specified username already exists in the database.");

        public static readonly Error NullUser = new Error("Users.NullUser", "We could not find a user with the specified credentials");

        public static readonly Error WrongCredentials = new Error("Users.WrongCredentials", "The credentials you've entered are incorrect.");

        public static readonly Error InvalidEmail = new Error("Users.InvalidEmail", "Sorry, the Email you entered is in an invalid format");
    }
}
