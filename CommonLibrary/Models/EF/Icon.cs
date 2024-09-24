using System;
using System.Collections.Generic;

namespace CommonLibrary.Models.EF;

public partial class Icon
{
    public int UserId { get; set; }

    public string Path { get; set; } = null!;
}
