using System;
using System.Collections.Generic;

namespace lab3_School.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? MiddleName { get; set; }

    public string? Position { get; set; }

    public virtual ICollection<Class> Classes { get; } = new List<Class>();

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();
}
