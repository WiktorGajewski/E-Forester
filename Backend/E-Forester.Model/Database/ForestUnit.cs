using System.Collections.Generic;

namespace E_Forester.Model.Database
{
    public class ForestUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
