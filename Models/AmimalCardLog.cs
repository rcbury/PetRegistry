using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class AmimalCardLog
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly CreateTime { get; set; }

    public int FkLogsType { get; set; }

    public int FkUser { get; set; }

    public virtual LogType FkLogsTypeNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;
}
