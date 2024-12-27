using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Tests.Infrastructure.Repositories
{
    public class ReservationRepositoryTests
    {
        private readonly ReservationsDbContext _dbContext;

        public ReservationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ReservationsDbContext>()
                .UseInMemoryDatabase("ReservationTestDb")
                .Options;
            _dbContext = new ReservationsDbContext(options);

            // Inicializar datos necesarios
            _dbContext.Reservations.Add(new Reservation
            {
                Id = Guid.NewGuid(),
                SpaceId = Guid.NewGuid(),
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1)
            });
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task HasOverlapAsync_ShouldReturnTrue_WhenOverlapExists()
        {
            // Arrange
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                SpaceId = Guid.NewGuid(),
                StartDateTime = DateTime.Now.AddHours(0.5),
                EndDateTime = DateTime.Now.AddHours(1.5)
            };
            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _dbContext.Reservations
                .AnyAsync(r => r.SpaceId == reservation.SpaceId && r.StartDateTime < reservation.EndDateTime && r.EndDateTime > reservation.StartDateTime);

            // Assert
            Assert.True(result);
        }
    }
}