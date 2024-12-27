using Domain.Exceptions;
using Xunit;

namespace ReservationSystem.Tests.Domain.Exceptions
{
    public class DomainExceptionsTests
    {
        [Fact]
        public void ReservationConflictException_ShouldReturnCorrectMessage()
        {
            // Arrange
            var message = "Conflict occurred";

            // Act
            var exception = new ReservationConflictException(message);

            // Assert
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void InvalidReservationDurationException_ShouldReturnCorrectMessage()
        {
            // Arrange
            var message = "Invalid duration";

            // Act
            var exception = new InvalidReservationDurationException(message);

            // Assert
            Assert.Equal(message, exception.Message);
        }
    }
}
