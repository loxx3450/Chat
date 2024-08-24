using System;
using System.Collections.Generic;

namespace CommonLibrary.Models.EF;

public partial class Dialogue
{
    public int Id { get; set; }

    public virtual Group? Group { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
