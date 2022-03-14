using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Plans.Commands.ClosePlanCommand
{
    public class ClosePlanCommand : IRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
