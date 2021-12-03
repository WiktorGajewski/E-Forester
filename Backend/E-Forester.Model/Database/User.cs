using E_Forester.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }

        public ICollection<ForestUnit> AssignedForestUnits { get; set; }

        public User()
        {
            AssignedForestUnits = new List<ForestUnit>();
        }
    }
}
