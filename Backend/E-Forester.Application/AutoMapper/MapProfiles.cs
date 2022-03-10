using AutoMapper;
using E_Forester.Application.DataTransferObjects.Divisions;
using E_Forester.Application.DataTransferObjects.ForestUnits;
using E_Forester.Application.DataTransferObjects.PlanExecutions;
using E_Forester.Application.DataTransferObjects.PlanItems;
using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Application.DataTransferObjects.Subareas;
using E_Forester.Application.DataTransferObjects.Users;
using E_Forester.Model.Database;
using System.Linq;

namespace E_Forester.Application.AutoMapper
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Plan, PlanDto>()
                .ForMember(dest => dest.ForestUnitName,
                    opt => opt.MapFrom(p => p.ForestUnit.Name))
                .ForMember(dest => dest.PlannedHectares,
                    opt => opt.MapFrom(p => p.PlanItems.Sum(x => x.PlannedHectares)))
                .ForMember(dest => dest.PlannedCubicMeters,
                    opt => opt.MapFrom(p => p.PlanItems.Sum(x => x.PlannedCubicMeters)))
                .ForMember(dest => dest.ExecutedHectares,
                    opt => opt.MapFrom(p => p.PlanExecutions.Sum(x => x.ExecutedHectares)))
                .ForMember(dest => dest.HarvestedCubicMeters,
                        opt => opt.MapFrom(p => p.PlanExecutions.Sum(x => x.HarvestedCubicMeters)))
                .ForMember(dest => dest.CompletedPlanItems,
                        opt => opt.MapFrom(p => p.PlanItems.Count(x => x.IsCompleted == true)))
                .ForMember(dest => dest.PlanItemsNumber,
                        opt => opt.MapFrom(p => p.PlanItems.Count()));

            CreateMap<ForestUnit, ForestUnitDto>();

            CreateMap<Division, DivisionDto>();

            CreateMap<Subarea, SubareaDto>();

            CreateMap<PlanItem, PlanItemDto>()
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(p => p.Subarea.Address))
                .ForMember(dest => dest.ExecutedHectares,
                    opt => opt.MapFrom(p => p.PlanExecutions.Sum(x => x.ExecutedHectares)))
                .ForMember(dest => dest.HarvestedCubicMeters,
                    opt => opt.MapFrom(p => p.PlanExecutions.Sum(x => x.HarvestedCubicMeters)));

            CreateMap<PlanExecution, PlanExecutionDto>();

            CreateMap<User, UserDto>();
        }
    }
}
