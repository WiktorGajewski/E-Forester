using E_Forester.Model.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.PlanItems.Commands.CreatePlanItemCommand
{
    public class CreatePlanItemCommand : IRequest
    {
        public double PlannedHectares { get; set; }

        public double PlannedCubicMeters { get; set; }

        [EnumDataType(typeof(WoodAssortment))]
        public WoodAssortment Assortments { get; set; }

        [EnumDataType(typeof(ActionGroup))]
        public ActionGroup ActionGroup { get; set; }

        public int DifficultyLevel { get; set; }

        public double Factor { get; set; }

        public int PlanId { get; set; }

        public int SubareaId { get; set; }
    }
}
