using System;
using System.Collections.Generic;

namespace BE_SWD.Models;

public partial class Lesson
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public int? OrderIndex { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
