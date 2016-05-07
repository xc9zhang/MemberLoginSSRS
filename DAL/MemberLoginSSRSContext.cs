using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MemberLoginSSRS.Models;

namespace MemberLoginSSRS.DAL
{
    public class MemberLoginSSRSContext : DbContext
    {
        public MemberLoginSSRSContext()
            : base("MemberLoginSSRSContext")
        {
        }

        public DbSet<TEReport> TEReports { get; set; }
        public DbSet<ReportAssignment> ReportAssignments { get; set; }
        public DbSet<CategoryAssignment> CategoryAssignments { get; set; }
        public DbSet<RoleAssignment> RoleAssignments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}