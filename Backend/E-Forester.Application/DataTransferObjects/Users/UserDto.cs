using E_Forester.Application.DataTransferObjects.ForestUnits;
using E_Forester.Model.Enums;
using System;
using System.Collections.Generic;

namespace E_Forester.Application.DataTransferObjects.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ForestUnitDto> AssignedForestUnits { get; set; }
    }
}
