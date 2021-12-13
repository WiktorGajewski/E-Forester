﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    public class Division
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public double Area { get; set; }

        public int ForestUnitId { get; set; }
        public ForestUnit ForestUnit { get; set; }

        public ICollection<Subarea> Subareas { get; set; }

        public Division()
        {
            Subareas = new List<Subarea>();
        }
    }
}