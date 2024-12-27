using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ReservationsDbContext : DbContext
    {
        public ReservationsDbContext(DbContextOptions<ReservationsDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Space)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SpaceId);

            // Índices para evitar solapamientos
            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.SpaceId, r.StartDateTime, r.EndDateTime })
                .IsUnique();
        }
    }
}
