using System;
using System.Collections.Generic;
using System.Web.Security;
using Travel_Request_System_EF.Models;
using Roles = Travel_Request_System_EF.Models.Roles;

namespace Travel_Request_System_EF.CustomAuthentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region User Properties

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Roles> Roles { get; set; }

        #endregion User Properties

        public CustomMembershipUser(Users user) : base("CustomMembership", user.Username, user.ID, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Roles = user.Roles;
        }
    }
}