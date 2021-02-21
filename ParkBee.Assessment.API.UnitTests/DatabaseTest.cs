using Microsoft.EntityFrameworkCore;
using Parkbee_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ParkBee.Assessment.API.UnitTests
{
    public class DatabaseTest
    {
        private ApplicationDbContext _context;

        [Fact]
        public void VerifyDatabaseCreation()
        {
            _context = CreateContext();

            _context.Database.EnsureCreated();
        }

        [Fact]
        public void VerifyDatabaseSeeding()
        {
            _context = CreateContext();

            SeedingService.SeedDb(_context);

            Assert.NotNull(_context.Garages.ToList());
            Assert.NotNull(_context.GarageDoors.ToList());
        }

        [Fact]
        public void Dispose()
        {
            _context = CreateContext();

            _context.Database.EnsureDeleted();
        }

        public ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            return new ApplicationDbContext(options);
        }

    }
}
