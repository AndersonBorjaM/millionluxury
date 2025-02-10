using Million.Domain.Abstractions;

namespace Million.Domain.Users
{
    public static class UserErrors
    {

        public static Error NotFound = new(
            "User.Found",
            "The user searched for this id does not exist"
        );

        public static Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "Credentials are incorrect"
        );


    }
}
