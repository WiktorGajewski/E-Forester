using System;

namespace E_Forester.Application.DataTransferObjects.Plans
{
    public class PlanDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool IsCompleted { get; set; }
        public int CompletedPlanItems { get; set; }
        public int PlanItemsNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ForestUnitId { get; set; }
        public int CreatorId { get; set; }
        public string ForestUnitName { get; set; }
        public double PlannedHectares { get; set; }
        public double PlannedCubicMeters { get; set; }
        public double ExecutedHectares { get; set; }
        public double HarvestedCubicMeters { get; set; }
    }
}