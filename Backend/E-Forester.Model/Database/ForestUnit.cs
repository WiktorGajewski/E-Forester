﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    public class ForestUnit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public double Area { get; set; }

        public ICollection<User> AssignedUsers { get; set; }

        public ICollection<Division> Divisions { get; set; }

        public ICollection<Plan> Plans { get; set; }

        public ForestUnit()
        {
            AssignedUsers = new List<User>();
            Divisions = new List<Division>();
            Plans = new List<Plan>();
        }
    }
}
