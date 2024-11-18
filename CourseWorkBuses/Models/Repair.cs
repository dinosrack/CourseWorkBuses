using System;
using System.Collections.Generic;

namespace CourseWorkBuses.Models;

public partial class Repair
{
    public int RepairId { get; set; }

    public int BusId { get; set; }

    public string RepairStatus { get; set; } = null!;

    public string? RepairNotes { get; set; }

    public virtual Bus Bus { get; set; } = null!;
}
