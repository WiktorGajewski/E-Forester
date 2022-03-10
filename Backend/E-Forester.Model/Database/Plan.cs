using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<PlanItem> PlanItems { get; set; }

        public ICollection<PlanExecution> PlanExecutions { get; set; }

        public int ForestUnitId { get; set; }
        public ForestUnit ForestUnit { get; set; }

        public int CreatorId { get; set; } 
        public User Creator { get; set; }

        public Plan()
        {
            PlanItems = new List<PlanItem>();
            PlanExecutions = new List<PlanExecution>();
        }
    }
}
