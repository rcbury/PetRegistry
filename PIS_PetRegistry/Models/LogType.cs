using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class LogType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Log> Logs { get; } = new List<Log>();
}
