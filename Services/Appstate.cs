using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArchive.Services
{
    public static class AppState
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static int RoleId { get; set; }
        public static int AccessLevel { get; set; }
    }
}
