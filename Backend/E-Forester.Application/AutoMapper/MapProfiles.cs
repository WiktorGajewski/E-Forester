using AutoMapper;
using E_Forester.Application.DataTransferObjects.Divisions;
using E_Forester.Application.DataTransferObjects.ForestUnits;
using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Model.Database;

namespace E_Forester.Application.AutoMapper
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Plan, PlanDto>();
            CreateMap<ForestUnit, ForestUnitDto>();
            CreateMap<Division, DivisionDto>();
        }
    }
}
