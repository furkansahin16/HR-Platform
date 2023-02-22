using AutoMapper;
using Business.Helper.PermissionHelper;
using Business.ViewModels.PermissionVM;
using Core.Entites.Concrete;
using Core.Enums;

namespace Business.Mapper
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<PermissionCreate, Permission>()
                .ForMember(x => x.Id, dest => dest.MapFrom(source => Guid.NewGuid()))
                .ForMember(x => x.Status, dest => dest.MapFrom(source => StatusTypes.Pending))
                .ForMember(x => x.Day, dest => dest.MapFrom(source => AnnualPermissionHelper.GetWorkingDays(source.StartTime, source.EndTime)))
                .ForMember(x => x.RequestTime, dest => dest.MapFrom(source => DateTime.Now));

            CreateMap<Permission, PermissionListItem>()
                .ForMember(x => x.PermissonType, dest => dest.MapFrom(source => source.PermissionTypes.ToString()))
                .ForMember(x => x.Employee, dest => dest.MapFrom(source => source.Personnel.ToString()))
                .ForMember(x => x.Status, dest => dest.MapFrom(source => source.Status.ToString()));
        }
    }
}
