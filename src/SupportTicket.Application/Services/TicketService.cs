public class TicketService
{
    private readonly AppDbContext _db;

    public TicketService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Ticket>> GetTickets(Guid tenantId)
    {
        return await _db.Tickets
            .Where(t => t.TenantId == tenantId)
            .ToListAsync();
    }

    public async Task<Ticket> CreateTicket(Ticket ticket)
    {
        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();
        return ticket;
    }
}

