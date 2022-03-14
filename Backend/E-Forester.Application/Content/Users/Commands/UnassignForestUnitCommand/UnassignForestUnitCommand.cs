using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Users.Commands.UnassignForestUnitCommand
{
    public class UnassignForestUnitCommand : IRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ForestUnitId { get; set; }
    }
}
