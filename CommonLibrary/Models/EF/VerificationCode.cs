using System;
using System.Collections.Generic;

namespace CommonLibrary.Models.EF;

public partial class VerificationCode
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Code { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool Used { get; set; }


    public VerificationCode(int userId, string code)
    {
        UserId = userId;
        Code = code;

        //Default values
        Used = false;
        CreatedAt = DateTime.UtcNow;
    }
}
