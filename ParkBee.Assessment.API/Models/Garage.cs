using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBee.Assessment.API.GarageModels
{
    public class Garage
    {
        public string GarageId { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public int ZoneNumber { get; set; }
        public int Id { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
        public int Capacity { get; set; }
        public int AvailableSpaces { get; set; }

        public List<GarageDoor> Doors { get; set; }
    }

    public class GarageDoor
    {
        public int Id { get; set; }
        public string Serialnumber { get; set; }
        public string IpAddress { get; set; }
        public bool IsOnline { get; set; }
        public bool IsOpen { get; set; }
        public int GarageId { get; set; }
        public DateTime LastModified { get; set; }
        public bool PreviousState { get; set; }
    }

    public class DoorState
    {
        public int Id { get; set; }
        public string DoorSerial { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool CurrentDoorStatus { get; set; }
        public bool PrevDoorStatus { get; set; }
        public bool CurrentDoorOpen { get; set; }
        public bool PrevDoorOpen { get; set; }

    }

    public class ParamPayload
    {
        public string StringParam { get; set; }
        public bool BoolParam { get; set; }
    }
}
