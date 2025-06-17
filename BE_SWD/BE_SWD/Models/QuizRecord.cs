using System;
using System.Collections.Generic;

namespace BE_SWD.Models;

public partial class QuizRecord
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int QuizId { get; set; }

    public int? Score { get; set; }

    public int? Total { get; set; }

    public DateTime? TakenAt { get; set; }

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();

    public virtual User User { get; set; } = null!;
}
