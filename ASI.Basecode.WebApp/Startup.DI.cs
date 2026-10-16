using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Services;
using ASI.Basecode.Services.ViewModelClasses;
using ASI.Basecode.WebApp.Models;
using ASI.Basecode.WebApp.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ASI.Basecode.WebApp
{
    // Other services configuration
    internal partial class StartupConfigurer
    {
        /// <summary>
        /// Configures the other services.
        /// </summary>
        private void ConfigureOtherServices()
        {
            // Framework
            this._services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            this._services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Common
            this._services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Current operator (stand-in for identity until auth is reintroduced)
            this._services.AddScoped<ICurrentUserService, CurrentUserService>();

            // Equipment administration
            this._services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            this._services.AddScoped<IEquipmentService, EquipmentService>();

            // User management
            this._services.AddScoped<IUserRepository, UserRepository>();
            this._services.AddScoped<IUserService, UserService>();

            // Borrower reservations
            this._services.AddScoped<IReservationRepository, ReservationRepository>();
            this._services.AddScoped<IReservationService, ReservationService>();

            // Custodian console
            this._services.AddScoped<ICustodianRepository, CustodianRepository>();
            this._services.AddScoped<ICustodianService, CustodianService>();

            // Return processing + condition inspection
            this._services.AddScoped<IReturnProcessingRepository, ReturnProcessingRepository>();
            this._services.AddScoped<IReturnProcessingService, ReturnProcessingService>();

            // Monitoring & audit
            this._services.AddScoped<IMonitoringRepository, MonitoringRepository>();
            this._services.AddScoped<IMonitoringService, MonitoringService>();

            // System settings + notifications
            this._services.AddScoped<ISettingsRepository, SettingsRepository>();
            this._services.AddScoped<ISettingsService, SettingsService>();

            this._services.AddHttpClient();
        }
    }
}
