using Microsoft.Extensions.DependencyInjection;
using RegexCraft.Core.Services.Contracts;
using RegexCraft.Core.Services.Implementations;

namespace RegexCraft.Core.Extensions;

public static class RegexCraftServiceCollectionExtensions
{
    public static IServiceCollection AddRegexCraftCore(this IServiceCollection services)
    {
        services.AddSingleton<IRegexTestingService, RegexTestingService>();
        services.AddSingleton<IPatternLibraryService, PatternLibraryService>();
        services.AddSingleton<IRegexExplainerService, RegexExplainerService>();
        services.AddSingleton<ICodeGeneratorService, CodeGeneratorService>();

        return services;
    }
}
