using System.ComponentModel.DataAnnotations;

namespace InsuranceCalc.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "Name is mandatory filed")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Age is mandatory filed")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Date of Birth is mandatory filed")]
        public OccupationConstants Occupation { get; set; }
        [Required(ErrorMessage = "Sum Assured is mandatory filed")]
        public double SumAssured { get; set; }
    }
    public enum OccupationConstants
    {
        Cleaner=1,
        Doctor=2,
        Author=3,
        Farmer=4,
        Mechanic=5,
        Florist=6
    }
}
