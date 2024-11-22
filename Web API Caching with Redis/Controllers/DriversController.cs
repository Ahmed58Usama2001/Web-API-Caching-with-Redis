using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Caching_with_Redis.Data;
using Web_API_Caching_with_Redis.Models;
using Web_API_Caching_with_Redis.Services;

namespace Web_API_Caching_with_Redis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly ILogger<DriversController> _logger;
    private readonly ICacheService _cacheService;
    private readonly AppDbContext _appDbContext;

    public DriversController(ILogger<DriversController> logger ,
        ICacheService cacheService,
        AppDbContext appDbContext)
    {
        _logger = logger;
        _cacheService = cacheService;
        _appDbContext = appDbContext;
    }

    [HttpGet("drivers")]
    public async Task<IActionResult> Get()
    {
        var cacheData = _cacheService.GetData<IEnumerable<Driver>>("drivers");

        if(cacheData is not null && cacheData.Any())
            return Ok(cacheData);

        cacheData =await _appDbContext.Drivers.ToListAsync();
        var expiryDate = DateTimeOffset.Now.AddSeconds(300);
        _cacheService.SetData("drivers", cacheData, expiryDate);

        return Ok(cacheData);
    }

    [HttpPost("AddDriver")]
    public async Task<IActionResult> Post(Driver value)
    {
        var addedObj= await _appDbContext.Drivers.AddAsync(value);

        var expiryDate = DateTimeOffset.Now.AddSeconds(300);
        _cacheService.SetData($"driver{value.Id}", addedObj.Entity, expiryDate);

        await _appDbContext.SaveChangesAsync();

        return Ok(addedObj.Entity);
    }

    [HttpDelete("DeleteDriver")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingDriver =await _appDbContext.Drivers.FirstOrDefaultAsync(d => d.Id == id);

        if (existingDriver != null)
        {
            _appDbContext.Drivers.Remove(existingDriver);
            _cacheService.RemoveData($"driver{existingDriver.Id}");
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }


       return NotFound();
    }
}
