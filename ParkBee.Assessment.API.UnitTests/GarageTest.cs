using ParkBee.Assessment.API.GarageModels;
using Parkbee_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkBee.Assessment.API.UnitTests
{
    public class GarageTest
    {
        [Fact]
        public void GetAllGarages()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            var garages = new GarageService(context).GetAllGarages();

            Assert.NotNull(garages);
        }

        [Fact]
        public void GetGarageWithValidId()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            int id = 1;
            var garage = new GarageService(context).GetByGarageId(id).Result;

            Assert.NotNull(garage);
            Assert.IsType<Garage>(garage);
        }

        [Fact]
        public void GetGarageWithInvalidId()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            int id = 500;
            var garage = new GarageService(context).GetByGarageId(id);

            Assert.Null(garage.Exception);
        }
    }
}
