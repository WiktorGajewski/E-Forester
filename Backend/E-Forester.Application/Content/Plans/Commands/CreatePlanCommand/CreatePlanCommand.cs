using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Plans.Commands.CreatePlanCommand
{
    public class CreatePlanCommand : IRequest
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public int ForestUnitId { get; set; }

    }
}