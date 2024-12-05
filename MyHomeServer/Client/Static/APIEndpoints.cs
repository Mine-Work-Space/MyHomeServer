using System;
using System.Net;
using System.Net.Sockets;

namespace MyHomeServer.Client.Static
{
    public static class APIEndpoints
    {
#if DEBUG
        internal static readonly string ServerBaseUrl = "https://localhost:5003"; //https://localhost:5003
#else
        internal static readonly string ServerBaseUrl = "https://localhost:5003";
#endif
        internal readonly static string s_register = $"{ServerBaseUrl}/api/user/register";
        internal readonly static string s_signIn = $"{ServerBaseUrl}/api/user/signin";
        internal readonly static string s_getUsers = $"{ServerBaseUrl}/api/user/get-users";
    }
}
