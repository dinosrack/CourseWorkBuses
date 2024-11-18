using System;
using System.Collections.Generic;

namespace CourseWorkBuses.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int FlightId { get; set; }

    public string TicketType { get; set; } = null!;

    public decimal TicketPrice { get; set; }

    public string TicketStatus { get; set; } = null!;

    public virtual Flight Flight { get; set; } = null!;
}
