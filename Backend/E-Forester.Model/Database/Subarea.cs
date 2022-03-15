using System.Collections.Generic;

namespace E_Forester.Model.Database
{
    public class Subarea
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Area { get; set; }
        public int DivisionId { get; set; }
        public Division Division { get; set; }
        public ICollection<PlanItem> PlanItems { get; set; }

        public Subarea()
        {
            PlanItems = new List<PlanItem>();
        }
    }
}
