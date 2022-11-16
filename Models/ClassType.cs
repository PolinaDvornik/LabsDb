using System;
using System.Collections.Generic;

namespace lab3_School.Models;

public partial class ClassType
{
    public int Id { get; set; }

    public string? Surname { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Class> Classes { get; } = new List<Class>();
}
