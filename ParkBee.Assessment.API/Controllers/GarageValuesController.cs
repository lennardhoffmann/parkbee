using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkBee.Assessment.API;
using ParkBee.Assessment.API.Facades;
using ParkBee.Assessment.API.GarageModels;
using ParkBee.Assessment.API.ResponseModels;
using Parkbee_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkbee_API.Controllers
{
    [Authorize]
    [Route("api/garages")]
    public class GarageValuesController : Controller
    {
        private GarageService _garageService;
        private GarageDoorService _doorService;
        private readonly ApplicationDbContext _context;

        #region constructor
        public GarageValuesController(ApplicationDbContext context)
        {
            _context = context;
            _garageService = new GarageService(_context);
            _doorService = new GarageDoorService(_context);
        }
        #endregion constructor

        //Returns all the garages in the DB set
        //Excludes the garage doors as they should only be accessed with the appropriategarage id
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<List<object>>> Get()
        {
            try
            {
                var response = await _garageService.GetAllGarages();

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return ExceptionResponse.RespondWith(ex);
            }

        }


        //Returns a specific garage, with populated garagedoor property based on the garage id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<Garage>> Get(int id)
        {
            try
            {
                var response = await _garageService.GetByGarageId(id);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return ExceptionResponse.RespondWith(ex);
            }

        }


        [HttpGet("serial/{serialNo}")]
        public async Task<GarageDoor> GetDoorBySerial(string serialNo)
        {
            return await _doorService.GetDoorBySerialNumber(serialNo);
        }

        [HttpPost("{garageId}/doors/{doorId}/open")]
        [ProducesResponseType(200)]
        [ProducesResponseType(503)]
        public async Task<ActionResult<DoorResponse>> ToggleGarageDoor([FromBody] ParamPayload payload, int garageId, int doorId)
        {
            try
            {
                    return new OkObjectResult(await _doorService.ToggleGarageDoor(payload.StringParam, garageId, doorId, payload.BoolParam));
            }
            catch (Exception ex)
            {
                return ExceptionResponse.RespondWith(ex);
            }
        }

        [HttpGet("status/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<Garage>> CheckGarageStatus(int id)
        {
            try
            {
                var response = await _garageService.CheckStatus(id);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return ExceptionResponse.RespondWith(ex);
            }
        }

        [HttpPost("{garageId}/doors/{doorId}/status")]
        [ProducesResponseType(200)]
        [ProducesResponseType(503)]
        public async Task<ActionResult<DoorResponse>> CheckGarageDoorStatus([FromBody] ParamPayload payload, int garageId, int doorId)
        {
            try
            {
                var response = await _doorService.CheckDoorStatus(payload.StringParam, garageId, doorId);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return ExceptionResponse.RespondWith(ex);
            }
        }
    }
}
