using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.API;
using ParkBee.Assessment.API.Facades;
using ParkBee.Assessment.API.GarageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GarageController
{

    private readonly ApplicationDbContext _context;

    public GarageController(ApplicationDbContext context)
    {
        _context = context;
    }

    //Purely a test method to determine whether the api is running
    [HttpGet("base"), AllowAnonymous]
    public string Get() => "API RUNNING";

}
