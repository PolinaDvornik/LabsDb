using System;
using System.Collections.Generic;

namespace lab3_School.Models;

public partial class Class
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? ClassroomTeacherId { get; set; }

    public int? ClassTypeId { get; set; }

    public int? StudentsCount { get; set; }

    public int? CreationYear { get; set; }

    public virtual ClassType? ClassType { get; set; }

    public virtual Teacher? ClassroomTeacher { get; set; }

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
