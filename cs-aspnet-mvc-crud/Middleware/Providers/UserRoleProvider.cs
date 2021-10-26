using cs_aspnet_mvc_crud.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace cs_aspnet_mvc_crud.Middleware.Providers
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string userPositionName)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            using (DBEntities entityModel = new DBEntities())
            {
                var userPositions = (
                    from userMapping in entityModel.User
                    join positionMapping in entityModel.UserPosition
                        on userMapping.user_position_id equals positionMapping.id
                    join permissionMapping in entityModel.UserPermission
                        on positionMapping.id equals permissionMapping.user_position_id
                    join ActionMapping in entityModel.UserAction
                        on permissionMapping.user_action_id equals ActionMapping.id
                    where permissionMapping.user_position_id == userMapping.user_position_id
                        && positionMapping.name == userPositionName
                    select positionMapping.name).ToArray();

                return userPositions;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userPositions = GetRolesForUser(username);

            return userPositions.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}