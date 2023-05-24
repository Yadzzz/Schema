using System;
using System.Collections.Generic;

namespace DataAccessLibrary.Models;

public partial class ScheduleFilter
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string JobType { get; set; } = null!;

    public string JobPlace { get; set; } = null!;
}
