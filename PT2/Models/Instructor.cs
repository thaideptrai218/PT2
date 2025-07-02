using System;
using System.Collections.Generic;

namespace PT2.Models;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string? InstructorFirstName { get; set; }

    public string? InstructorMidName { get; set; }

    public string? InstructorLastName { get; set; }

    public int? DepartmentId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Department? Department { get; set; }
}
