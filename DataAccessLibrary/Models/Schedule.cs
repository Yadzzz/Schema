using System;
using System.Collections.Generic;

namespace DataAccessLibrary.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public string? TimeStart { get; set; }

    public string? TimeEnd { get; set; }

    public string? JobType { get; set; }

    public string? JobPlace { get; set; }

    public string? Comment { get; set; }
}
