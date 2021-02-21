using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBee.Assessment.API.ResponseModels
{
    public class DoorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
