using Microsoft.EntityFrameworkCore;
using Won.Api.Data;
using Won.Api.Entities;
using Won.Api.Repositories;

namespace Won.Api.Tests.RepositoryTests
{
    public class TripRepositoryTests
    {
        private WonDbContext _dbContext;

        private TripRepository _tripRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WonDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _dbContext = new WonDbContext(options);

            _dbContext.Database.EnsureDeleted();

            _dbContext.Database.EnsureCreated();

            _tripRepository = new TripRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetTripsAsync_ShouldReturnListOfTrips()
        {
            // Arrange
            var trip = new Trip
            {
                Name = "Paris"
            };

            _dbContext.Trips.Add(trip);

            await _dbContext.SaveChangesAsync();

            // Act
            var actual = await _tripRepository.GetTripsAsync();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(1));

            Assert.That(actual[0].Name, Is.EqualTo("Paris"));
        }

        [Test]
        public async Task GetTripByIdAsync_ShouldReturnTrip_WhenTripExists()
        {
            // Arrange
            var trip = new Trip
            {
                Name = "Japan"
            };

            _dbContext.Trips.Add(trip);

            await _dbContext.SaveChangesAsync();

            // Act
            var actual = await _tripRepository.GetTripByIdAsync(trip.TripId);

            // Assert
            Assert.That(actual, Is.Not.Null);

            Assert.That(actual.Name, Is.EqualTo("Japan"));
        }

        [Test]
        public async Task GetTripByIdAsync_ShouldReturnNull_WhenTripDoesNotExist()
        {
            // Act
            var actual = await _tripRepository.GetTripByIdAsync(99);

            // Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task CreateTripAsync_ShouldAddTripToDatabase()
        {
            // Arrange
            var trip = new Trip
            {
                Name = "Italy",
                Location = "Rome",
                Budget = 2000,
                GroupSize = 2
            };

            // Act
            var actual = await _tripRepository.CreateTripAsync(trip);

            // Assert
            Assert.That(actual.Name, Is.EqualTo("Italy"));

            Assert.That(_dbContext.Trips.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateTripAsync_ShouldUpdateTrip_WhenTripExists()
        {
            // Arrange
            var trip = new Trip
            {
                Name = "Old Trip",
                Location = "Spain"
            };

            _dbContext.Trips.Add(trip);

            await _dbContext.SaveChangesAsync();

            trip.Name = "Updated Trip";

            // Act
            var actual = await _tripRepository.UpdateTripAsync(trip);

            // Assert
            Assert.That(actual?.Name, Is.EqualTo("Updated Trip"));
        }

        [Test]
        public async Task DeleteTripAsync_ShouldReturnTrue_WhenTripDeleted()
        {
            // Arrange
            var trip = new Trip
            {
                Name = "France"
            };

            _dbContext.Trips.Add(trip);

            await _dbContext.SaveChangesAsync();

            // Act
            var actual = await _tripRepository.DeleteTripAsync(trip.TripId);

            // Assert
            Assert.That(actual, Is.True);

            Assert.That(_dbContext.Trips.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteTripAsync_ShouldReturnFalse_WhenTripDoesNotExist()
        {
            // Act
            var actual = await _tripRepository.DeleteTripAsync(999);

            // Assert
            Assert.That(actual, Is.False);
        }
    }
}
