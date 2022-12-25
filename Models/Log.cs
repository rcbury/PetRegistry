using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class Log
{
    public int Id { get; set; }

    public string? Data { get; set; }

    public DateOnly? CreateTime { get; set; }

    public int FkLogsType { get; set; }

    public int FkShelter { get; set; }

    public int FkUser { get; set; }

    public virtual LogType FkLogsTypeNavigation { get; set; } = null!;

    public virtual Shelter FkShelterNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;
}
