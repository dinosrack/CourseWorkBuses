using System;
using System.Collections.Generic;

namespace CourseWorkBuses.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string ClientLastName { get; set; } = null!;

    public string ClientFirstName { get; set; } = null!;

    public string? ClientMiddleName { get; set; }

    public string? ClientContacts { get; set; }
}
