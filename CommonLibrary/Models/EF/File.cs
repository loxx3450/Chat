using System;
using System.Collections.Generic;

namespace CommonLibrary.Models.EF;

public partial class File
{
    public int Id { get; set; }

    public int MessageId { get; set; }

    public string FilePath { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
