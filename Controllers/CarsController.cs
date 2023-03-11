using Garage.Data;
using Garage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage.Controllers;

[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly ILogger<CarsController> _logger;
    private readonly ApiDbContext _context;

    public CarsController(ILogger<CarsController> logger, ApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetAllCars")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var cars = await _context.Cars.ToListAsync();
            return Ok(cars);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Error getting cars");
            return StatusCode(500);
        }
    }

    [HttpPost(Name = "CreateCar")]
    public async Task<IActionResult> Post([FromBody] Car car)
    {
        try
        {
            // Add the new car to the database
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            // Return a 201 response with the new car object
            return CreatedAtAction("Get", new { id = car.Id }, car);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Error creating car");
            return StatusCode(500);
        }
    }
}