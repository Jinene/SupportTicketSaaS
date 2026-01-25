using SupportTicket.Domain.Enums;

namespace SupportTicket.Application.DTOs;

public class TicketDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketPriority Priority { get; set; }
}

