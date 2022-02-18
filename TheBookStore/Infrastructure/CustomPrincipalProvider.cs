using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TheBookStore.Contracts;

namespace TheBookStore.Infrastructure
{
    public class CustomPrincipalProvider : IPrincipalProvider
    {
        private const string username = "fanier";
        private const string password = "secretpassword";

        public IPrincipal CreatePrincipal(string username, string password)
        {
            if (CustomPrincipalProvider.username != username || CustomPrincipalProvider.password != password)
            {
                return null;
            }

            var identity = new GenericIdentity(username);
            IPrincipal principal = new GenericPrincipal(identity, new[] { "User" });
            return principal;
        }
    }
}