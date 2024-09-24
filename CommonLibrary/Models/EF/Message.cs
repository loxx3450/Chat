using System;
using System.Collections.Generic;

namespace CommonLibrary.Models.EF;

public partial class Message
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int DialogueId { get; set; }

    public DateTime SentAt { get; set; }

    public string? Text { get; set; }

    public bool HasFiles { get; set; }

    public virtual Dialogue Dialogue { get; set; } = null!;

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual User User { get; set; } = null!;
}
