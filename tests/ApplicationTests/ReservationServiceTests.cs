using Application.Services;
using Moq;
using Xunit;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Settings;
using Microsoft.Extensions.Options;
using Domain.Exceptions.ReservationExceptions;

namespace ReservationSystem.Tests.Application.Services
{
    public class ReservationServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IOptions<ReservationsSettings>> _reservationsSettingsMock;
        private readonly Mock<IReservationRepository> _reservationRepoMock;
        private readonly ReservationService _service;

        public ReservationServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _reservationRepoMock = new Mock<IReservationRepository>();
            _reservationsSettingsMock = new Mock<IOptions<ReservationsSettings>>();

            var settings = new ReservationsSettings
            {
                MinDurationMinutes = 15,
                MaxDurationMinutes = 120
            };

            _reservationsSettingsMock.Setup(s => s.Value).Returns(settings);

            _unitOfWorkMock.Setup(u => u.ReservationRepository).Returns(_reservationRepoMock.Object);
            _service = new ReservationService(_unitOfWorkMock.Object, _reservationsSettingsMock.Object);
        }

        [Fact]
        public async Task CreateReservation_ShouldThrowInvalidReservationDurationException_WhenDurationIsInvalid()
        {
            // Arrange
            var reservation = new Reservation
            {
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(10) // Menor al mínimo permitido
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidReservationDurationException>(
                () => _service.CreateReservationAsync(reservation));
        }

        [Fact]
        public async Task CreateReservation_ShouldThrowReservationConflictException_WhenOverlapExists()
        {
            // Arrange
            var reservation = new Reservation
            {
                StartDateTime = DateTime.Now.AddHours(1),
                EndDateTime = DateTime.Now.AddHours(2),
                SpaceId = Guid.NewGuid()
            };

            var overlappingReservations = new List<Reservation>
            {
                new Reservation
                {
                    StartDateTime = DateTime.Now.AddHours(1).AddMinutes(30),
                    EndDateTime = DateTime.Now.AddHours(2).AddMinutes(30),
                    SpaceId = reservation.SpaceId
                }
            };

            _reservationRepoMock
                .Setup(r => r.GetOverlappingReservationsAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(overlappingReservations);

            // Act & Assert
            await Assert.ThrowsAsync<ReservationConflictException>(
                () => _service.CreateReservationAsync(reservation));
        }
    }
}
