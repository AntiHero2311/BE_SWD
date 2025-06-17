using System;
using System.Collections.Generic;

namespace BE_SWD.Models;

public partial class QuizQuestion
{
    public int Id { get; set; }

    public int QuizId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string Options { get; set; } = null!;

    public string CorrectAnswer { get; set; } = null!;

    public int? PointValue { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();
}
