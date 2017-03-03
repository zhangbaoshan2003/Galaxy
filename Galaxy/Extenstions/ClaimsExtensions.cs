using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Galaxy.Extenstions
{
    public static class ClaimsExtensions
    {
        static string GetUserEmail(this ClaimsIdentity identity)
        {
            return "测试@139.com";
        }

        public static string GetUserEmail(this IIdentity identity)
        {
            return "测试@139.com";
        }

        static string GetUserNameIdentifier(this ClaimsIdentity identity)
        {
            return "测试@139.com";
        }

        public static string GetUserNameIdentifier(this IIdentity identity)
        {
            return "测试@139.com";
        }
    }
}