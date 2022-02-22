using AutoMapper;
using E_Forester.Application.DataTransferObjects.Divisions;
using E_Forester.Application.DataTransferObjects.ForestUnits;
using E_Forester.Application.DataTransferObjects.PlanExecutions;
using E_Forester.Application.DataTransferObjects.PlanItems;
using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Application.DataTransferObjects.Subareas;
using E_Forester.Application.DataTransferObjects.Users;
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

            CreateMap<Subarea, SubareaDto>();

            CreateMap<PlanItem, PlanItemDto>()
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(p => p.Subarea.Address));

            CreateMap<PlanExecution, PlanExecutionDto>();

            CreateMap<User, UserDto>();
        }
    }
}
