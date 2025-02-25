using System;
using System.Collections.Generic;

namespace Registration1.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? DueDate { get; set; }

    public int? ProjectId { get; set; }

    public virtual Project? Project { get; set; }
}
