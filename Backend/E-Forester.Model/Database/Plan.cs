using System;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
