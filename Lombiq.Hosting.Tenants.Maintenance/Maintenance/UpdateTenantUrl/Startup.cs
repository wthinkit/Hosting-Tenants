using Lombiq.Hosting.Tenants.Maintenance.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;

namespace Lombiq.Hosting.Tenants.Maintenance.Maintenance.UpdateTenantUrl;

[Feature(Constants.FeatureNames.UpdateTenantUrl)]
public class Startup : StartupBase
{
    private readonly IShellConfiguration _shellConfiguration;

    public Startup(IShellConfiguration shellConfiguration) =>
        _shellConfiguration = shellConfiguration;

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IMaintenanceProvider, UpdateShellRequestUrlMaintenanceProvider>();
        services.AddScoped<IMaintenanceProvider, UpdateSiteUrlMaintenanceProvider>();

        var options = new UpdateTenantUrlMaintenanceOptions();
        var configSection = _shellConfiguration.GetSection("Lombiq_Hosting_Tenants_Maintenance:UpdateTenantUrl");
        configSection.Bind(options);
        services.Configure<UpdateTenantUrlMaintenanceOptions>(configSection);
    }
}
