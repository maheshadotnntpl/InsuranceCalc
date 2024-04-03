using System;
using System.Collections.Generic;

namespace InsuranceCalc.Models;

public partial class Occupation
{
    public int Id { get; set; }

    public string? OccupationName { get; set; }

    public int? Rating { get; set; }

    public virtual OccupationRating? RatingNavigation { get; set; }
}
