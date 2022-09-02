using Lombiq.Hosting.MultiTenancy.Tenants.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OrchardCore.Environment.Extensions.Features;
using System.Linq;

namespace Lombiq.Hosting.MultiTenancy.Tenants.Services;

public class TenantFeatureBuilderEvents : IFeatureBuilderEvents
{
    private readonly IConfiguration _configuration;

    public TenantFeatureBuilderEvents(IConfiguration configuration) =>
        _configuration = configuration;

    public void Building(FeatureBuildingContext context)
    {
        var forbiddenFeaturesOptions = _configuration
            .GetSection("OrchardCore")
            .GetSection("Lombiq_Hosting_MultiTenancy_Tenants")
            .GetSection("ForbiddenFeaturesOptions")
            .Get<ForbiddenFeaturesOptions>();

        if (forbiddenFeaturesOptions.ForbiddenFeatures.Contains(context.FeatureId))
        {
            context.DefaultTenantOnly = true;
        }

        // for alwaysEnabledFeatures, set isAlwaysEnabled to true
        // needs to be run conditionally: only on user tenants (probably not possible from this method)
        //else
        //{
        //    var alwaysEnabledFeatures = _alwaysOnFeaturesOptions.Value.AlwaysOnFeatures;
        //    if (alwaysEnabledFeatures.Contains(context.FeatureId))
        //    {
        //        context.IsAlwaysEnabled = true;
        //    }
        //}
    }

    public void Built(IFeatureInfo featureInfo)
    {
        // Not needed.
    }
}
