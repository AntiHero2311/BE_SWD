using System;
using System.Collections.Generic;

namespace BE_SWD.Models;

public partial class QuizAnswer
{
    public int Id { get; set; }

    public int QuizRecordId { get; set; }

    public int QuestionId { get; set; }

    public string? SelectedAnswer { get; set; }

    public bool? IsCorrect { get; set; }

    public virtual QuizQuestion Question { get; set; } = null!;

    public virtual QuizRecord QuizRecord { get; set; } = null!;
}
