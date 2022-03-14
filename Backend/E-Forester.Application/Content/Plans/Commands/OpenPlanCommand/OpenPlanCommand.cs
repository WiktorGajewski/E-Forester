using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Plans.Commands.OpenPlanCommand
{
    public class OpenPlanCommand : IRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
