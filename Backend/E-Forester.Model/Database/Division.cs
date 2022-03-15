using System.Collections.Generic;

namespace E_Forester.Model.Database
{
    public class Division
    {
        public int Id { get; set; }
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
