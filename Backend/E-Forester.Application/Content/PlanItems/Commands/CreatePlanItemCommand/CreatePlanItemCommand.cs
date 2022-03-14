using E_Forester.Model.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.PlanItems.Commands.CreatePlanItemCommand
{
    public class CreatePlanItemCommand : IRequest
    {
        [Required]
        public double PlannedHectares { get; set; }

        [Required]
        public double PlannedCubicMeters { get; set; }

        [Required]
        public WoodAssortment Assortments { get; set; }

        [Required]
        public ActionGroup ActionGroup { get; set; }

        [Required]
        public int DifficultyLevel { get; set; }

        [Required]
        public double Factor { get; set; }

        [Required]
        public int PlanId { get; set; }

        [Required]
        public int SubareaId { get; set; }
    }
}
