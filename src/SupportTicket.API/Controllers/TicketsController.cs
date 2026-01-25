[ApiController]
[Route("api/tickets")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly TicketService _service;

    public TicketsController(TicketService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tenantId = (Guid)HttpContext.Items["TenantId"]!;
        return Ok(await _service.GetTickets(tenantId));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Ticket ticket)
    {
        ticket.TenantId = (Guid)HttpContext.Items["TenantId"]!;
        return Ok(await _service.CreateTicket(ticket));
    }
}

