using System;
using System.Collections.Generic;

namespace InsuranceCalc.Models;

public partial class OccupationRating
{
    public int Id { get; set; }

    public string? Rating { get; set; }

    public decimal? Factor { get; set; }

    public virtual ICollection<Occupation> Occupations { get; set; } = new List<Occupation>();
}
