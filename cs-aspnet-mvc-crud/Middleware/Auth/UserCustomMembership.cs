using cs_aspnet_mvc_crud.Models;
using System;
using System.Web.Security;

namespace cs_aspnet_mvc_crud.Middleware.Auth
{
    public class UserCustomMembership : MembershipUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public ICollection<user_position> Roles { get; set; }
        public user_position Roles { get; set; }

        public UserCustomMembership(user userModel) : base("CustomMembership", userModel.username, userModel.id, userModel.email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = userModel.id;
            FirstName = userModel.first_name;
            LastName = userModel.last_name;
            Roles = userModel.user_position;
        }
    }
}