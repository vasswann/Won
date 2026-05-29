using Moq;
using NUnit.Framework;
using Won.Api.Entities;
using Won.Api.Repositories.Interfaces;
using Won.Api.Services;
using Won.Shared.Dtos;

namespace Won.Api.Tests.ServiceTests
{
    public class TripServiceTests
    {
        private TripService _tripService;

        private Mock<ITripRepository> _tripRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _tripRepositoryMock = new Mock<ITripRepository>();

            _tripService = new TripService(_tripRepositoryMock.Object);
        }

        [Test]
        public async Task GetTripsAsync_ShouldReturnListOfTrips()
        {
            // Arrange
            var trips = new List<Trip>()
            {
                new Trip
                {
                    TripId = 1,
                    Name = "Spain"
                }
            };

            _tripRepositoryMock
                .Setup(repository => repository.GetTripsAsync())
                .ReturnsAsync(trips);

            // Act
            var actual = await _tripService.GetTripsAsync();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetTripByIdAsync_ShouldReturnTrip_WhenTripExists()
        {
            // Arrange
            var trip = new Trip
            {
                TripId = 1,
                Name = "Japan"
            };

            _tripRepositoryMock
                .Setup(repository => repository.GetTripByIdAsync(1))
                .ReturnsAsync(trip);

            // Act
            var actual = await _tripService.GetTripByIdAsync(1);

            // Assert
            Assert.That(actual?.Name, Is.EqualTo("Japan"));
        }

        [Test]
        public async Task CreateTripAsync_ShouldReturnCreatedTrip()
        {
            // Arrange
            var tripData = new CreateTripDto
            {
                Name = "Italy",
                Location = "Rome",
                Budget = 1000,
                GroupSize = 2
            };

            var trip = new Trip
            {
                TripId = 1,
                Name = "Italy"
            };

            _tripRepositoryMock
                .Setup(repository => repository.CreateTripAsync(It.IsAny<Trip>()))
                .ReturnsAsync(trip);

            // Act
            var actual = await _tripService.CreateTripAsync(tripData);

            // Assert
            Assert.That(actual.Name, Is.EqualTo("Italy"));
        }

        [Test]
        public async Task UpdateTripAsync_ShouldReturnUpdatedTrip_WhenTripExists()
        {
            // Arrange
            var existingTrip = new Trip
            {
                TripId = 1,
                Name = "Old Trip"
            };

            var updatedTripData = new UpdateTripDto
            {
                Name = "Updated Trip"
            };

            _tripRepositoryMock
                .Setup(repository => repository.GetTripByIdAsync(1))
                .ReturnsAsync(existingTrip);

            _tripRepositoryMock
                .Setup(repository => repository.UpdateTripAsync(existingTrip))
                .ReturnsAsync(existingTrip);

            // Act
            var actual = await _tripService.UpdateTripAsync(1, updatedTripData);

            // Assert
            Assert.That(actual?.Name, Is.EqualTo("Updated Trip"));
        }

        [Test]
        public async Task UpdateTripAsync_ShouldReturnNull_WhenTripDoesNotExist()
        {
            // Arrange
            var updatedTripData = new UpdateTripDto();

            _tripRepositoryMock
                .Setup(repository => repository.GetTripByIdAsync(99))
                .ReturnsAsync((Trip?)null);

            // Act
            var actual = await _tripService.UpdateTripAsync(99, updatedTripData);

            // Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteTripAsync_ShouldReturnTrue_WhenTripDeleted()
        {
            // Arrange
            _tripRepositoryMock
                .Setup(repository => repository.DeleteTripAsync(1))
                .ReturnsAsync(true);

            // Act
            var actual = await _tripService.DeleteTripAsync(1);

            // Assert
            Assert.That(actual, Is.True);
        }
    }
}
