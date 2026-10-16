using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ASI.Basecode.WebApp
{
    // AutoMapper configuration
    internal partial class StartupConfigurer
    {
        /// <summary>
        /// Configure auto mapper
        /// </summary>
        private void ConfigureAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProfileConfiguration());
            });

            this._services.AddSingleton<IMapper>(sp => mapperConfiguration.CreateMapper());
        }

        private class AutoMapperProfileConfiguration : Profile
        {
            public AutoMapperProfileConfiguration()
            {
                // Equipment administration
                this.CreateMap<Data.Models.Equipment, Services.ViewModelClasses.EquipmentListViewModel>()
                    .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.EquipmentCategory.CategoryName))
                    .ForMember(d => d.LocationName, o => o.MapFrom(s => s.Location != null ? s.Location.LocationName : null))
                    .ForMember(d => d.ConditionName, o => o.MapFrom(s => s.CurrentCondition.Description))
                    .ForMember(d => d.OperationalStatusName, o => o.MapFrom(s => s.OperationalStatus.Description));

                this.CreateMap<Data.Models.Equipment, Services.ViewModelClasses.EquipmentFormViewModel>()
                    .ForMember(d => d.TotalQuantity, o => o.Ignore());

                this.CreateMap<Services.ViewModelClasses.EquipmentFormViewModel, Data.Models.Equipment>()
                    .ForMember(d => d.EquipmentId, o => o.Ignore())
                    .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                    .ForMember(d => d.RowVersion, o => o.Ignore())
                    .ForMember(d => d.IsActive, o => o.MapFrom(s => s.IsActive));
            }
        }
    }
}
