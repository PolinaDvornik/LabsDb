using System;
using System.Collections.Generic;

namespace lab3_School.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public DateTime? LessonDate { get; set; }

    public string? DayOfWeek { get; set; }

    public int? ClassId { get; set; }

    public int? SubjectId { get; set; }

    public TimeSpan? StartLessonTime { get; set; }

    public TimeSpan? EndLessonTime { get; set; }

    public int? TeacherId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
