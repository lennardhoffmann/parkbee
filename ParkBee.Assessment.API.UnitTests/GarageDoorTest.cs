using ParkBee.Assessment.API.GarageModels;
using ParkBee.Assessment.API.ResponseModels;
using Parkbee_API.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkBee.Assessment.API.UnitTests
{
    public class GarageDoorTest
    {
        [Fact]
        public void GetAllGarageDoors()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            var doors = new GarageDoorService(context).GetAllDoors().Result;

            Assert.NotNull(doors);
            Assert.IsType<List<GarageDoor>>(doors);
        }

        [Fact]
        public void GetAllGarageDoorsForValidGarage()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            Assert.NotNull(new GarageDoorService(context).GetDoorsForGarage(1).Result);
            Assert.IsType<List<GarageDoor>>(new GarageDoorService(context).GetDoorsForGarage(1).Result);
        }

        [Fact]
        public void GetAllGarageDoorsForInvalidValidGarage()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            int id = 0;
            var garageDoors = new GarageDoorService(context).GetDoorsForGarage(id);

            Assert.Null(garageDoors.Exception);
        }

        [Fact]
        public void PingGarageDoorWithValidIp()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            string ip = "8.8.8.8";

            Assert.True(new GarageDoorService(context).PingGarageDoor(ip));
            Assert.IsType<bool>(new GarageDoorService(context).PingGarageDoor(ip));
        }

        [Fact]
        public void PingGarageDoorWithInvalidIp()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            string ip = "12345.12345.1234567.100000";

            Assert.False(new GarageDoorService(context).PingGarageDoor(ip));
            Assert.IsType<bool>(new GarageDoorService(context).PingGarageDoor(ip));
        }

        [Fact]
        public void CheckDoorStatusForValidDoor()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            var door = new GarageDoorService(context).GetDoorsForGarage(1).Result[0];
            var isPingable = new GarageDoorService(context).PingGarageDoor(door.IpAddress);

            if (isPingable)
            {
                Assert.True(new GarageDoorService(context).CheckDoorStatus(door.Serialnumber, door.GarageId, door.Id).Result.Success);
                Assert.IsType<DoorResponse>(new GarageDoorService(context).CheckDoorStatus(door.Serialnumber, door.GarageId, door.Id).Result);
            }
            else
            {
                Assert.False(new GarageDoorService(context).CheckDoorStatus(door.Serialnumber, door.GarageId, door.Id).Result.Success);
                Assert.IsType<DoorResponse>(new GarageDoorService(context).CheckDoorStatus(door.Serialnumber, door.GarageId, door.Id).Result);
            }
        }

        [Fact]
        public void CheckDoorStatusForInvalidDoor()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            Assert.False(new GarageDoorService(context).CheckDoorStatus(Guid.NewGuid().ToString(), 0, 0).Result.Success);
        }

        [Fact]
        public void ToogleDoorForValidGarageDoor()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            var door = new GarageDoorService(context).GetDoorsForGarage(1).Result[0];
            var isPingable = new GarageDoorService(context).PingGarageDoor(door.IpAddress);

            if (isPingable)
            {
                Assert.True(new GarageDoorService(context).ToggleGarageDoor(door.Serialnumber, door.GarageId, door.Id, !door.IsOpen).Result.Success);
                Assert.IsType<DoorResponse>(new GarageDoorService(context).CheckDoorStatus(door.Serialnumber, door.GarageId, door.Id).Result);
            }
            else
            {
                Assert.False(new GarageDoorService(context).ToggleGarageDoor(door.Serialnumber, door.GarageId, door.Id, !door.IsOpen).Result.Success);
                Assert.IsType<DoorResponse>(new GarageDoorService(context).CheckDoorStatus(door.Serialnumber, door.GarageId, door.Id).Result);
            }
        }

        [Fact]
        public void ToogleDoorForInvalidGarageDoor()
        {
            var context = new DatabaseTest().CreateContext();
            SeedingService.SeedDb(context);

            Assert.False(new GarageDoorService(context).ToggleGarageDoor(Guid.NewGuid().ToString(), 0, 0, true).Result.Success);
        }
    }
}
