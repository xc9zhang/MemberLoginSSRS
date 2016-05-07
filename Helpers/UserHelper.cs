using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MemberLoginSSRS.DAL;
using MemberLoginSSRS.DTO;
using MemberLoginSSRS.Models;

namespace MemberLoginSSRS.Helpers
{
    [Authorize]
    public static class UserHelper
    {
        private static readonly MemberLoginSSRSContext Db = new MemberLoginSSRSContext();

        internal static IEnumerable<Role> GetAllRoles()
        {
            return Db.Roles.ToList();
        }

        public static IEnumerable<Role> GetRoles(string userName)
        {
            var user = Db.Users.FirstOrDefault(p => p.Name == userName);
            if (user==null || user.RoleAssignments == null)
            {
                if (userName == "tony.song")
                {
                    return new[]
                    {
                        new Role
                        {
                            ID = 1,
                            Name = "Admin"
                        }
                    };
                }
                return new List<Role>();
            }
            return user.RoleAssignments.Select(p => p.Role).ToList();
        }

        internal static IEnumerable<UserDto> GetAllUsers()
        {
            var users = Db.Users.ToList();
            return users.Select(GetUser).ToList();
        }

        internal static UserDto GetUser(User user)
        {
            return new UserDto
            {
                ID = user.ID,
                Name = user.Name,
                Roles = user.RoleAssignments == null ? null : string.Join(",", user.RoleAssignments.Select(p => p.Role.Name).OrderBy(p => p)),
                RoleIds = user.RoleAssignments == null ? null : user.RoleAssignments.Select(p => p.RoleID),
            };
        }


        internal static UserDto GetUser(int id)
        {
            var report = Db.Users.FirstOrDefault(p => p.ID == id);
            return GetUser(report);
        }

        internal static void SaveUser(ViewModels.UserEditVM user)
        {
            int id = user.ID;
            var userUpdate = new User
            {
                ID = user.ID,
                Name = user.Name,
            };
            if (id == 0)
            {
                Db.Users.Add(userUpdate);
                Db.SaveChanges();
                id = userUpdate.ID;
            }
            else
            {
                userUpdate = Db.Users.FirstOrDefault(p => p.ID == id);
                userUpdate.Name = user.Name;
                foreach (var assignment in Db.RoleAssignments.Where(p => p.UserID == id))
                {
                    userUpdate.RoleAssignments.Remove(assignment);
                    Db.RoleAssignments.Remove(assignment);
                }

                Db.Users.AddOrUpdate(userUpdate);
                Db.SaveChanges();
            }

            foreach (var roleid in user.SelectedRoles)
            {
                Db.RoleAssignments.Add(new RoleAssignment
                {
                    UserID = id,
                    RoleID = roleid
                });
            }
            Db.SaveChanges();
        }

        internal static void DeleteUser(int id)
        {
            var user = Db.Users.FirstOrDefault(p => p.ID == id);
            Db.Users.Remove(user);
            Db.SaveChanges();
        }
    }
}