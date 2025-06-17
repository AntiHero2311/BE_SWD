using System;
using System.Collections.Generic;

namespace BE_SWD.Models;

public partial class Quiz
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    public string? Title { get; set; }

    public int? TotalPoints { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

    public virtual ICollection<QuizRecord> QuizRecords { get; set; } = new List<QuizRecord>();
}
