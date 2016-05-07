using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberLoginSSRS.DTO
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public IEnumerable<int> RoleIds { get; set; }

    }
}