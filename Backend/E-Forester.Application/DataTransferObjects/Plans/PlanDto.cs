using System;

namespace E_Forester.Application.DataTransferObjects.Plans
{
    public class PlanDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ForestUnitId { get; set; }
        public int CreatorId { get; set; }
    }
}