using System;
using Travel_Request_System_EF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
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

        #endregion

        public CustomMembershipUser(Users user):base("CustomMembership", user.Username, user.ID, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Roles = user.Roles;
        }
    }
}