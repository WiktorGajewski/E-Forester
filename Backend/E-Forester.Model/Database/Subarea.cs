using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    public class Subarea
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public double Area { get; set; }
    }
}
