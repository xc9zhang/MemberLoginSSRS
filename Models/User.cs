using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberLoginSSRS.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Layout { get; set; }
        public virtual ICollection<RoleAssignment> RoleAssignments { get; set; }
    }
}