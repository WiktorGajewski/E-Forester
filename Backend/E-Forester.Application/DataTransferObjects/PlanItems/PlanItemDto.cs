using E_Forester.Model.Enums;
using System;

namespace E_Forester.Application.DataTransferObjects.PlanItems
{
    public class PlanItemDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public double PlannedHectares { get; set; }
        public double PlannedCubicMeters { get; set; }
        public WoodAssortment Assortments { get; set; }
        public ActionGroup ActionGroup { get; set; }
        public int DifficultyLevel { get; set; }
        public double Factor { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PlanId { get; set; }
        public int SubareaId { get; set; }
        public int CreatorId { get; set; }
        public string Address { get; set; }
        public double ExecutedHectares { get; set; }
        public double HarvestedCubicMeters { get; set; }

    }
}
