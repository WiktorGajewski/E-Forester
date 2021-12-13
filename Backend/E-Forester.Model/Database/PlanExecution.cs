using System;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    public class PlanExecution
    {
        [Key]
        public int Id { get; set; }

        public double Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public int PlanItemId { get; set; }
        public PlanItem PlanItem { get; set; }

        public int PlanId { get; set; }
        public Plan Plan { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
