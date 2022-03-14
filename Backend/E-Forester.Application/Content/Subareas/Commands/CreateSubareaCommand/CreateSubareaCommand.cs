using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Subareas.Commands.CreateSubareaCommand
{
    public class CreateSubareaCommand : IRequest
    {
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public double Area { get; set; }

        public int DivisionId { get; set; }
    }
}
