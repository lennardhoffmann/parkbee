
using ParkBee.Assessment.API;
using ParkBee.Assessment.API.GarageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkbee_API.Services
{
    public static class SeedingService
    {
        public static void SeedDb(ApplicationDbContext context)
        {
            context.Garages.AddRange(Enumerable.Range(1, 3).Select(s =>
            new Garage
            {
                Name = $"Garage {s}",
                GarageId = Guid.NewGuid().ToString(),
                ZoneNumber = new Random().Next(1000, 2000),
                CountryCode = Countrycodes[new Random().Next(0, Countrycodes.Count)],
                Capacity = new Random().Next(100, 200),
                AvailableSpaces = new Random().Next(90, 185),
            }));

            context.SaveChanges();

            foreach (var g in context.Garages.ToList())
            {
                GenerateDoors(context, g.Id);
            }
        }

        public static void GenerateDoors(ApplicationDbContext context, int garageId)
        {
            for (int i = 0; i < new Random().Next(1, 6); i++)
            {
                context.GarageDoors.Add(new GarageDoor
                {
                    Serialnumber = Guid.NewGuid().ToString(),
                    IsOnline = new Random().Next(1, 5) > 3,
                    GarageId = garageId,
                    IpAddress = IpAddresses[new Random().Next(0, IpAddresses.Count)],
                    IsOpen = new Random().Next(1, 11) > 5
                });
            }

            context.SaveChanges();
        }

        private static readonly List<string> IpAddresses = new List<string>
        {
            "208.67.222.222",
            "208.67.220.220",
            "8.8.8.8",
            "127.0.0.12345",
            "127.0.0.98765",
            "127.0.0.24680"
        };

        private static readonly List<string> Countrycodes = new List<string>
        {
            "NL",
            "UK",
            "FR",
            "DE"
        };

    }
}
