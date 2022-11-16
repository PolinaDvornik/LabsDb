using System;
using System.Collections.Generic;

namespace lab3_School.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? MiddleName { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool? Sex { get; set; }

    public string? Address { get; set; }

    public string? MotherFullName { get; set; }

    public string? FutherFullName { get; set; }

    public int? ClassId { get; set; }

    public string? AdditionalInfo { get; set; }

    public virtual Class? Class { get; set; }
}
