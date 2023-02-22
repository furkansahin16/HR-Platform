using AutoMapper;
using Business.Helper.ImageConverter;
using Business.Helper.PermissionHelper;
using Business.ViewModels.Common;
using Core.Entites.Concrete;

namespace Business.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeRegister, Employee>()
                .ForMember(x => x.Photo, dest => dest.MapFrom(
                i => i.Photo == null
                ? "default-user-image.png"
                : i.Photo.ToStringUrl()))
                .ForMember(x => x.WorkTimeByYear, dest => dest.MapFrom(source => AnnualPermissionHelper.GetWorkYears(source.StartDateOfWork)))
                .ForMember(x => x.AnnualPermission, dest => dest.MapFrom(source => AnnualPermissionHelper.GetAnnualPermissionDay(source.StartDateOfWork)));

            CreateMap<Employee, EmployeeSummary>()
                .ForMember(x => x.Department, dest => dest.MapFrom(i => i.Department.ToString()))
                .ForMember(x => x.Job, dest => dest.MapFrom(i => i.Job.ToString()))
                .ForMember(X => X.FullName, dest => dest.MapFrom(m => m.ToString()));
                //.ForMember(x => x.Photo, dest => dest.MapFrom(source =>
                //    Path.Combine($@"~/images/{source.Photo}")
                //    ));

            CreateMap<Employee, EmployeeUpdate>();

            CreateMap<Employee, EmployeeListItem>()
                .ForMember(x => x.Department, dest => dest.MapFrom(i => i.Department.ToString()))
                .ForMember(X => X.FullName, dest => dest.MapFrom(m => m.ToString()));

        }
    }
}
