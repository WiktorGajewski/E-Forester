using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.ForestUnits.Commands.CreateForestUnitCommand
{
    public class CreateForestUnitCommand : IRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        public double Area { get; set; }
    }
}
