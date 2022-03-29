using System;

namespace E_Forester.Model.Database
{
    public class PlanExecution
    {
        public int Id { get; set; }
        public double ExecutedHectares { get; set; }
        public double HarvestedCubicMeters { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PlanItemId { get; set; }
        public PlanItem PlanItem { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
