using RegexCraft.Core.Services.Contracts;
using Spectre.Console;

namespace RegexCraft.Console.UI;

public class PerformanceTester
{
    private readonly IRegexTestingService _testingService;

    public PerformanceTester(IRegexTestingService testingService)
    {
        _testingService = testingService;
    }

    public async Task RunAsync()
    {
        AnsiConsole.MarkupLine("[bold yellow]⚡ Performance Tester[/]");
        AnsiConsole.WriteLine();

        while (true)
        {
            var pattern = AnsiConsole.Ask<string>("[green]Enter regex pattern:[/]");

            if (string.IsNullOrWhiteSpace(pattern))
            {
                AnsiConsole.MarkupLine("[red]Pattern cannot be empty![/]");
                continue;
            }

            var testString = AnsiConsole.Ask<string>("[cyan]Enter test string:[/]");
            var iterations = AnsiConsole.Ask<int>("[blue]Number of iterations:[/]", 1000);

            await Task.Run(() =>
            {
                AnsiConsole.Status()
                    .Start($"Running {iterations} iterations...", ctx =>
                    {
                        var avgTime = _testingService.MeasurePerformance(pattern, testString, iterations);
                        var singleTest = _testingService.TestPattern(pattern, testString);

                        DisplayPerformanceResults(pattern, testString, iterations, avgTime, singleTest);
                    });
            });

            if (!AnsiConsole.Confirm("[dim]Test another pattern's performance?[/]"))
                break;

            AnsiConsole.WriteLine();
        }
    }

    private void DisplayPerformanceResults(string pattern, string testString, int iterations, TimeSpan avgTime, RegexCraft.Core.Models.RegexTestResult singleTest)
    {
        AnsiConsole.WriteLine();

        var table = new Table()
                     .Border(TableBorder.Rounded)
                     .BorderColor(Color.Yellow);

        table.AddColumn(new TableColumn("[bold]Metric[/]").Width(15));
        table.AddColumn(new TableColumn("[bold]Value[/]").Width(40));

        table.AddColumn("[bold]Metric[/]");
        table.AddColumn("[bold]Value[/]");

        table.AddRow("Pattern", $"[dim]{pattern}[/]");
        table.AddRow("Test String", $"[dim]{testString}[/]");
        table.AddRow("Iterations", $"[cyan]{iterations:N0}[/]");
        table.AddRow("Single Execution", $"[green]{singleTest.ExecutionTime.TotalMilliseconds:F4}ms[/]");
        table.AddRow("Average Time", $"[yellow]{avgTime.TotalMilliseconds:F4}ms[/]");
        table.AddRow("Operations/sec", $"[blue]{(1000 / avgTime.TotalMilliseconds):F0}[/]");
        table.AddRow("Match Result", singleTest.IsMatch ? "[green]✅ Match[/]" : "[red]❌ No Match[/]");

        // Performance rating
        var rating = GetPerformanceRating(avgTime);
        table.AddRow("Performance", rating);

        var panel = new Panel(table)
                .Header("⚡ Performance Results")
                .BorderColor(Color.Yellow)
                .Padding(1, 0, 1, 0);

        AnsiConsole.Write(panel);

        // Display optimization tips
        if (avgTime.TotalMilliseconds > 1)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold yellow]💡 Optimization Tips:[/]");
            AnsiConsole.MarkupLine("  • Consider using more specific character classes");
            AnsiConsole.MarkupLine("  • Avoid excessive backtracking with possessive quantifiers");
            AnsiConsole.MarkupLine("  • Use anchors (^ $) when matching entire strings");
            AnsiConsole.MarkupLine("  • Consider breaking complex patterns into simpler ones");
        }
    }

    private string GetPerformanceRating(TimeSpan avgTime)
    {
        var ms = avgTime.TotalMilliseconds;
        return ms switch
        {
            < 0.01 => "[green]⚡ Excellent (< 0.01ms)[/]",
            < 0.1 => "[lime]🟢 Very Good (< 0.1ms)[/]",
            < 1 => "[yellow]🟡 Good (< 1ms)[/]",
            < 10 => "[orange1]🟠 Moderate (< 10ms)[/]",
            _ => "[red]🔴 Slow (> 10ms)[/]"
        };
    }
}