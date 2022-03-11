using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Divisions.Commands.CreateDivisionCommand
{
    public class CreateDivisionCommand : IRequest
    {
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public double Area { get; set; }

        public int ForestUnitId { get; set; }
    }
}
