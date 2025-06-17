using System;
using System.Collections.Generic;

namespace BE_SWD.Models;

public partial class Course
{
    public int Id { get; set; }

    public int? MathCenterId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual MathCenter? MathCenter { get; set; }
}
