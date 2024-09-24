using System;
using System.Collections.Generic;

namespace CommonLibrary.Models.EF;

public partial class Group
{
    public int DialogueId { get; set; }

    public string DialogueName { get; set; } = null!;

    public string? IconPath { get; set; }

    public virtual Dialogue Dialogue { get; set; } = null!;
}
