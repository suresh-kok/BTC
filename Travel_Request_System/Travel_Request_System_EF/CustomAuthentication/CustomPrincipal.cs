﻿using System.Linq;
using System.Security.Principal;

namespace Travel_Request_System_EF.CustomAuthentication
{
    public class CustomPrincipal : IPrincipal
    {
        #region Identity Properties

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }

        #endregion Identity Properties

        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
    }
}