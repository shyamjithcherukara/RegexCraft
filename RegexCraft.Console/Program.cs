using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RegexCraft.Console.UI;
using RegexCraft.Core.Extensions;
using Spectre.Console;

public class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHost();
        var menuHandler = host.Services.GetRequiredService<MenuHandler>();

        // Display welcome banner
        DisplayWelcomeBanner();

        // Run the main menu loop
        await menuHandler.RunAsync();

        AnsiConsole.MarkupLine("\n[bold blue]Thanks for using RegexCraft! 🎯[/]");
    }

    private static IHost CreateHost()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddRegexCraftCore();
                services.AddTransient<MenuHandler>();
                services.AddTransient<PatternTester>();
                services.AddTransient<PatternLibraryBrowser>();
                services.AddTransient<PatternExplainer>();
                services.AddTransient<CodeGenerator>();
                services.AddTransient<PerformanceTester>();
            })
            .Build();
    }

    public static void DisplayWelcomeBanner()
    {
        var rule = new Rule("[bold blue]RegexCraft[/]")
        {
            Justification = Justify.Center
        };

        AnsiConsole.Write(rule);
        AnsiConsole.MarkupLine("[dim]Interactive Regex Testing & Building Tool[/]");
        AnsiConsole.WriteLine();
    }
}