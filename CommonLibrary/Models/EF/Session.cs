using System;
using System.Collections.Generic;

namespace CommonLibrary.Models.EF;

public partial class Session
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Ip { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public bool IsLoggedIn { get; set; }

    public virtual User User { get; set; } = null!;


    public Session(int userId, string ip)
    {
        UserId = userId;
        Ip = ip;

        //Default values
        IsLoggedIn = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
