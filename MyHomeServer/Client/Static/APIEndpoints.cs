namespace MyHomeServer.Client.Static
{
    public static class APIEndpoints
    {
#if DEBUG
        internal const string ServerBaseUrl = "https://localhost:5003";
#else
        internal const string ServerBaseUrl = "https://localhost:5003";
#endif
        internal readonly static string s_register = $"{ServerBaseUrl}/api/user/register";
        internal readonly static string s_signIn = $"{ServerBaseUrl}/api/user/signin";
        internal readonly static string s_getUsers = $"{ServerBaseUrl}/api/user/get-users";
    }
}
