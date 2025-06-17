using System;
using System.Collections.Generic;

namespace BE_SWD.Models;

public partial class CourseEnrollment
{
    public int UserId { get; set; }

    public int CourseId { get; set; }

    public DateTime? EnrolledAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
