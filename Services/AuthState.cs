using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArchive.Services
{
    using Microsoft.Maui.Storage;

    public static class AuthState
    {
        public static async Task<string?> GetToken()
        {
            return await SecureStorage.GetAsync("jwt_token");
        }

        public static async Task Logout()
        {
            SecureStorage.Remove("jwt_token");
            SecureStorage.Remove("username");
            SecureStorage.Remove("role");
        }
    }
}
