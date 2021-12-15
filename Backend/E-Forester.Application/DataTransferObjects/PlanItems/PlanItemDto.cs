using E_Forester.Model.Enums;
using System;

namespace E_Forester.Application.DataTransferObjects.PlanItems
{
    public class PlanItemDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public double Quantity { get; set; }
        public string MeasureUnit { get; set; }
        public WoodAssortment Assortments { get; set; }
        public ActionGroup ActionGroup { get; set; }
        public int DifficultyLevel { get; set; }
        public double Factor { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PlanId { get; set; }
        public int SubareaId { get; set; }
        public int CreatorId { get; set; }
    }
}
