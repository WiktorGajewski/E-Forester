using System.Collections.Generic;
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

        public int DivisionId { get; set; }
        public Division Division { get; set; }

        public ICollection<PlanItem> PlanItems { get; set; }

        public Subarea()
        {
            PlanItems = new List<PlanItem>();
        }
    }
}
