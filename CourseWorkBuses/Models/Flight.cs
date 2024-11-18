using System;
using System.Collections.Generic;

namespace CourseWorkBuses.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public int BusId { get; set; }

    public string DeparturePoint { get; set; } = null!;

    public DateOnly DepartureDate { get; set; }

    public TimeOnly DepartureTime { get; set; }

    public string ArrivalPoint { get; set; } = null!;

    public DateOnly ArrivalDate { get; set; }

    public TimeOnly ArrivalTime { get; set; }

    public virtual Bus Bus { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
