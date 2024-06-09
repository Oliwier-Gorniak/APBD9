using APBD9.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD9.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IDbService _dbService;
    public ClientsController(IDbService dbService)
    {
        _dbService = dbService;
    }  
    
    [HttpDelete("{idClient}")]
    public async Task<ActionResult> DeleteClient(int idClient)
    {
        var client = await _dbService.GetClientById(idClient);
        if (client == null)
        {
            return NotFound("Client not found");
        }

        if (await _dbService.ClientHasTrips(idClient))
        {
            return BadRequest("Client has trips");
        }
        
        await _dbService.DeleteClient(client);
        return Ok("Client deleted");
    }
}