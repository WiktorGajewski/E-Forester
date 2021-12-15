using E_Forester.Model.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.PlanItems.Commands.CreatePlanItemCommand
{
    public class CreatePlanItemCommand : IRequest
    {
        public double Quantity { get; set; }

        [Required]
        [StringLength(100)]
        public string MeasureUnit { get; set; }

        public WoodAssortment Assortments { get; set; }

        public ActionGroup ActionGroup { get; set; }

        public int DifficultyLevel { get; set; }

        public double Factor { get; set; }

        public int PlanId { get; set; }

        public int SubareaId { get; set; }
    }
}
