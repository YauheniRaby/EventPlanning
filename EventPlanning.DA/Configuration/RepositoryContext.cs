using EventPlanning.DA.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlanning.DA.Configuration
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Case> Cases { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CaseParam> CaseParams { get; set; }

        public DbSet<Participation> Participations { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
