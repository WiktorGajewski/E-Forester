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
    }
}
