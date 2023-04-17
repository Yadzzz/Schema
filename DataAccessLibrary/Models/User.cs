using System;
using System.Collections.Generic;

namespace DataAccessLibrary.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public bool Active { get; set; }

    public string? ForordnandeDate { get; set; }
}
