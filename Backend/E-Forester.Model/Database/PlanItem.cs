using E_Forester.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    public class PlanItem
    {
        [Key]
        public int Id { get; set; }

        public bool IsCompleted { get; set; }

        public double Quantity { get; set; }

        [Required]
        [StringLength(100)]
        public string MeasureUnit { get; set; }

        public WoodAssortment Assortments { get; set; }

        public ActionGroup ActionGroup { get; set; }

        public int DifficultyLevel { get; set; }

        public double Factor { get; set; }

        public DateTime CreatedAt { get; set; }

        public int PlanId { get; set; }
        public Plan Plan { get; set; }

        public int SubareaId { get; set; }
        public Subarea Subarea { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<PlanExecution> PlanExecutions { get; set; }

        public PlanItem()
        {
            PlanExecutions = new List<PlanExecution>();
        }
    }
}
