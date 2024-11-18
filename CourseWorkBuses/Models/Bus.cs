using System;
using System.Collections.Generic;

namespace CourseWorkBuses.Models;

public partial class Bus
{
    public int BusId { get; set; }

    public string BusBrand { get; set; } = null!;

    public string BusModel { get; set; } = null!;

    public int BusYearOfManufacture { get; set; }

    public string BusLicensePlate { get; set; } = null!;

    public int BusCapacity { get; set; }

    public string BusStatus { get; set; } = null!;

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();
}
