using System;
using System.Collections.Generic;

namespace DataAccessLibrary.Models;

public partial class ScheduleRequests
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ScheduleId { get; set; }

    public bool Accepted { get; set; }

    public bool Active { get; set; }
}
