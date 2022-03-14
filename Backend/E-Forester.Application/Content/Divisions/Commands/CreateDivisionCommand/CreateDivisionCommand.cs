using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Divisions.Commands.CreateDivisionCommand
{
    public class CreateDivisionCommand : IRequest
    {
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        public double Area { get; set; }

        [Required]
        public int ForestUnitId { get; set; }
    }
}
