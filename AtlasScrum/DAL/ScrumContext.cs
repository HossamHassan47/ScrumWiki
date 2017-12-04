using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasScrum.DAL
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using AtlasScrum.Models;

    public class ScrumContext : DbContext
    {
        public ScrumContext()
            : base("ScrumContext")
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        
        public DbSet<Sprint> Sprints { get; set; }
        
        public DbSet<TeamMember> TeamMembers { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<BacklogItem> BacklogItems { get; set; }
        
        public DbSet<SprintTeamMember> SprintTeamMembers{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            ////base.OnModelCreating(modelBuilder);
        }
    }
}