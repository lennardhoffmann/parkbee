using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkBee.Assessment.API.GarageModels;

namespace ParkBee.Assessment.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Garage> Garages { get; set; }
        public DbSet<GarageDoor> GarageDoors { get; set; }
        public DbSet<DoorState> DoorStates { get; set; }
    }
}
