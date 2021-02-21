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

        public async Task<object> GetAllGarages()
        {
            var garages = await _context.Garages.Select(x => new { x.Name, x.GarageId, x.ZoneNumber, x.CountryCode }).ToListAsync();

            return garages;
        }


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

    }
}
