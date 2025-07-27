using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using RegexCraft.API.Models;
using RegexCraft.Core.Extensions;
using System.Threading.RateLimiting;

namespace RegexCraft.API.Extensions;

/// <summary>
/// Provides extension methods for registering RegexCraft API services and related configurations.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Adds RegexCraft API services, validators, rate limiting, and CORS policies to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddRegexCraftApi(this IServiceCollection services)
    {
        // Add core services
        services.AddRegexCraftCore();

        // Add validators
        services.AddScoped<IValidator<RegexTestRequest>, RegexTestRequestValidator>();
        services.AddScoped<IValidator<CodeGenerationRequest>, CodeGenerationRequestValidator>();
        services.AddScoped<IValidator<PerformanceTestRequest>, PerformanceTestRequestValidator>();
        services.AddScoped<IValidator<PatternExplanationRequest>, PatternExplanationRequestValidator>();

        // Add rate limiting
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("RegexApi", limiterOptions =>
            {
                limiterOptions.PermitLimit = 100;
                limiterOptions.Window = TimeSpan.FromMinutes(1);
                limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                limiterOptions.QueueLimit = 10;
            });
        });

        // Add CORS
        services.AddCors(options =>
        {
            options.AddPolicy("RegexCraftPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        return services;
    }
}