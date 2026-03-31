namespace OctoBridge.Domain.Constants;

public static class ApiRoutes
{
    public const string Base = "api";
    public const string Version1 = Base + "/v1";

    public static class Users
    {
        public const string Root = Version1 + "/users";
        public const string Profile = Root + "/profile/{id}";
    }

    public static class Auth
    {
        public const string Root = Version1 + "/auth";
    }

    public static class Github
    {
        public const string Root = Version1 + "/github";
    }
    public static class PAT
    {
        public const string Root = Version1 + "/pat";
    }

}
