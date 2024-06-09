using APBD9.Data;
using APBD9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD9.Services;

public class DbService : IDbService
{
    private readonly Apbd9Context _context;
    public DbService(Apbd9Context context)
    {
        _context = context;
    }
    public async Task<ICollection<Trip>> GetTripsSorted(int page, int pageSize)
    {
        return await _context.Trips
            .Include(e => e.IdCountries)
            .Include(e => e.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(e => e.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<int> GetTotalTripsCount()
    {
        return await _context.Trips.CountAsync();
    }

    public async Task<Client?> GetClientById(int idClient)
    {
        return await _context.Clients.FindAsync(idClient);
    }

    public async Task DeleteClient(Client client)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ClientHasTrips(int idClient)
    {
        return await _context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);
    }

    public async Task<Client?> GetClientByPesel(string clientDtoPesel)
    {
        return await _context.Clients.FirstOrDefaultAsync(e => e.Pesel == clientDtoPesel);
    }

    public async Task<bool> ClientIsAssignedToTrip(string clientDtoPesel, int clientDtoIdTrip)
    {
        return await _context.ClientTrips.AnyAsync(ct =>
            ct.IdClientNavigation.Pesel == clientDtoPesel && ct.IdTrip == clientDtoIdTrip);
    }

    public async Task<Trip?> GetTripById(int clientDtoIdTrip)
    {
        return await _context.Trips.FirstOrDefaultAsync(e => e.IdTrip == clientDtoIdTrip);
    }

    public async Task AddClient(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task AddClientTrip(ClientTrip clientTrip)
    {
        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();
    }
}