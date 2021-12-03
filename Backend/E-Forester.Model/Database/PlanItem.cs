using E_Forester.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    public class PlanItem
    {
        [Key]
        public int Id { get; set; }

        public bool IsCompleted { get; set; }

        public double Quantity { get; set; }

        [Required]
        [StringLength(100)]
        public string MeasureUnit { get; set; }

        public ICollection<WoodAssortment> Assortments { get; set; }

        public ActionGroup ActionGroup { get; set; }

        public int DifficultyLevel { get; set; }

        public double Factor { get; set; }

        public DateTime CreatedAt { get; set; }

        public PlanItem()
        {
            Assortments = new List<WoodAssortment>();
        }
    }
}
