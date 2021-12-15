﻿using System;

namespace E_Forester.Application.DataTransferObjects.PlanExecutions
{
    public class PlanExecutionDto
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PlanItemId { get; set; }
        public int PlanId { get; set; }
        public int CreatorId { get; set; }
    }
}
