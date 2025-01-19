using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicantsDbContext : IdentityDbContext<User>
    {
        public ApplicantsDbContext(DbContextOptions<ApplicantsDbContext> options)
            : base(options) { }

        public DbSet<Applicant> Applicants { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Applicant>()
                .HasIndex(r => new { r.Document })
                .IsUnique();
        }
    }
}
