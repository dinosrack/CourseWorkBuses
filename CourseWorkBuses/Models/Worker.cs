using System;
using System.Collections.Generic;

namespace CourseWorkBuses.Models;

public partial class Worker
{
    public int WorkerId { get; set; }

    public string WorkerLastName { get; set; } = null!;

    public string WorkerFirstName { get; set; } = null!;

    public string? WorkerMiddleName { get; set; }

    public string WorkerPosition { get; set; } = null!;

    public string? WorkerContacts { get; set; }

    public DateOnly WorkerHireDate { get; set; }
}
