using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.API;
using ParkBee.Assessment.API.GarageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkbee_API.Services
{
    public class GarageService
    {
        private readonly ApplicationDbContext _context;
        private readonly GarageDoorService _doorService;

        #region constructors
        public GarageService()
        {
        }

        public GarageService(ApplicationDbContext context)
        {
            _context = context;
            _doorService = new GarageDoorService(_context);
        }
        #endregion constructors


        //Returns the name, id, zone number and country code of all garages in the Garage DBSet
        public async Task<object> GetAllGarages() => await _context.Garages.Select(x => new { x.Name, x.GarageId, x.ZoneNumber, x.CountryCode }).ToListAsync();


        //Returns a specific garage in the db set based on the id provided
        public async Task<Garage> GetByGarageId(int id)
        {
            if (id < 1)
            {
                return null;
            }
            else
            {
                var garage = await _context.Garages.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (garage != null)
                    garage.Doors = await _doorService.GetDoorsForGarage(id);

                return garage;
            }
        }

        //Checks the status of all garage doors of a specific garage based on the garage id
        public async Task<Garage> CheckStatus(int id)
        {
            if (id < 1)
            {
                return null;
            }
            else
            {
                var doors = await new GarageDoorService(_context).GetDoorsForGarage(id);

                foreach (var door in doors)
                {
                    await new GarageDoorService(_context).CheckDoorStatus(door.Serialnumber, id, door.Id);
                }

                return await GetByGarageId(id);
            }

        }

    }
}
