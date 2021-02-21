using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.API;
using ParkBee.Assessment.API.GarageModels;
using ParkBee.Assessment.API.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Parkbee_API.Services
{
    public class GarageDoorService
    {
        private readonly ApplicationDbContext _context;

        #region constructors
        public GarageDoorService() { }

        public GarageDoorService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion constructors

        public async Task<List<GarageDoor>> GetAllDoors() => await _context.GarageDoors.ToListAsync();
        public async Task<List<GarageDoor>> GetDoorsForGarage(int garageId)
        {
            if (garageId < 1)
            {
                return null;
            }
            else
                return await _context.GarageDoors.Where(x => x.GarageId == garageId).OrderBy(a => a.Id).ToListAsync();
        }
        public async Task<GarageDoor> GetDoorBySerialNumber(string serial) => await _context.GarageDoors.Where(x => x.Serialnumber == serial).FirstOrDefaultAsync();


        public async Task<DoorResponse> OpenGarageDoor(string serialNumber)
        {
            var response = await _context.GarageDoors.Where(x => x.Serialnumber == serialNumber).FirstOrDefaultAsync();
            var isOnline = false;

            if (response != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    isOnline = PingGarageDoor(response.IpAddress);

                    if (isOnline)
                        break;
                }

                if (isOnline)
                {
                    _context.DoorStates.AddRange(new DoorState
                    {
                        DoorSerial = response.Serialnumber,
                        ModifiedDate = DateTime.Now,
                        PrevDoorStatus = response.IsOpen,
                        CurrentDoorStatus = true
                    });

                    response.IsOpen = true;
                    response.IsOnline = true;

                    _context.GarageDoors.Update(response);
                    _context.SaveChanges();

                    return new DoorResponse { Code = "service_success", Message = "Door successfully opened", Success = true };
                }
                else
                {
                    return new DoorResponse { Code = "service_unavailable", Message = $"Door with serial {response.Serialnumber} could not be opened", Success = false };
                }
            }
            else
                return new DoorResponse { Code = "service_unavailable", Message = "Door could not be found", Success = false };
        }

        public async Task<DoorResponse> CheckDoorStatus(string serialNumber, int garageId, int doorId)
        {
            var response = await _context.GarageDoors.Where(x => x.Serialnumber == serialNumber && x.GarageId == garageId && x.Id == doorId).FirstOrDefaultAsync();
            var isOnline = false;

            if (response != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    isOnline = PingGarageDoor(response.IpAddress);

                    if (isOnline)
                        break;
                }

                if (isOnline)
                {
                    if (!response.IsOnline)
                    {
                        _context.DoorStates.AddRange(new DoorState
                        {
                            DoorSerial = response.Serialnumber,
                            ModifiedDate = DateTime.Now,
                            PrevDoorStatus = response.IsOnline,
                            CurrentDoorStatus = true
                        });

                        response.IsOnline = true;

                        _context.GarageDoors.Update(response);
                        _context.SaveChanges();
                    }

                    return new DoorResponse { Code = "service_success", Message = "Door is online", Success = true };
                }
                else
                {
                    if (response.IsOnline)
                    {
                        _context.DoorStates.AddRange(new DoorState
                        {
                            DoorSerial = response.Serialnumber,
                            ModifiedDate = DateTime.Now,
                            PrevDoorStatus = response.IsOnline,
                            CurrentDoorStatus = false
                        });

                        response.IsOnline = false;

                        _context.GarageDoors.Update(response);
                        _context.SaveChanges();
                    }

                    return new DoorResponse { Code = "service_unavailable", Message = $"Door with serial {response.Serialnumber} is offline", Success = false };
                }
            }
            else
                return new DoorResponse { Code = "service_unavailable", Message = "Door could not be found", Success = false };
        }

        public bool PingGarageDoor(string ip)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(ip);

                if (reply.Status == IPStatus.Success)
                {
                    pingable = true;
                }
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

    }
}
