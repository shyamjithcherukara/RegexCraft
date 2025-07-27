using RegexCraft.Core.Models;
using RegexCraft.Core.Services.Contracts;
using Spectre.Console;

namespace RegexCraft.Console.UI;

public class PatternLibraryBrowser
{
    private readonly IPatternLibraryService _libraryService;
    private readonly IRegexTestingService _testingService;

    public PatternLibraryBrowser(IPatternLibraryService libraryService, IRegexTestingService testingService)
    {
        _libraryService = libraryService;
        _testingService = testingService;
    }

    public async Task RunAsync()
    {
        AnsiConsole.MarkupLine("[bold yellow]📚 Pattern Library Browser[/]");
        AnsiConsole.WriteLine();

        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]How would you like to browse?[/]")
                    .AddChoices(new[]
                    {
                            "📁 Browse by Category",
                            "🔍 Search Patterns",
                            "📋 View All Patterns",
                            "🔙 Back to Main Menu"
                    }));

            switch (choice)
            {
                case "📁 Browse by Category":
                    await BrowseByCategoryAsync();
                    break;

                case "🔍 Search Patterns":
                    await SearchPatternsAsync();
                    break;

                case "📋 View All Patterns":
                    await ViewAllPatternsAsync();
                    break;

                case "🔙 Back to Main Menu":
                    return;
            }

            AnsiConsole.WriteLine();
        }
    }

    private async Task BrowseByCategoryAsync()
    {
        var categories = await _libraryService.GetCategoriesAsync();

        var selectedCategory = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]Select a category:[/]")
                .AddChoices(categories)
                .AddChoices("🔙 Back"));

        if (selectedCategory == "🔙 Back") return;

        var patterns = await _libraryService.GetPatternsByCategoryAsync(selectedCategory);
        await DisplayPatternsAsync(patterns, $"📁 {selectedCategory}");
    }

    private async Task SearchPatternsAsync()
    {
        var searchTerm = AnsiConsole.Ask<string>("[cyan]Enter search term:[/]");
        var patterns = await _libraryService.SearchPatternsAsync(searchTerm);

        if (patterns.Length == 0)
        {
            AnsiConsole.MarkupLine($"[yellow]No patterns found matching '{searchTerm}'[/]");
            return;
        }

        await DisplayPatternsAsync(patterns, $"🔍 Search: '{searchTerm}'");
    }

    private async Task ViewAllPatternsAsync()
    {
        var patterns = await _libraryService.GetAllPatternsAsync();
        await DisplayPatternsAsync(patterns, "📋 All Patterns");
    }

    private async Task DisplayPatternsAsync(PatternLibraryItem[] patterns, string title)
    {
        if (patterns.Length == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No patterns found.[/]");
            return;
        }

        var selectedPattern = AnsiConsole.Prompt(
            new SelectionPrompt<PatternLibraryItem>()
                .Title($"[bold]{title}[/] - Select a pattern:")
                .UseConverter(p => $"{p.Name} - {p.Description}")
                .AddChoices(patterns)
                .PageSize(10));

        await DisplayPatternDetailsAsync(selectedPattern);
    }

    private async Task DisplayPatternDetailsAsync(PatternLibraryItem pattern)
    {
        AnsiConsole.WriteLine();

        // Create pattern details table
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue);

        table.AddColumn(new TableColumn("[bold]Property[/]").Width(12));
        table.AddColumn(new TableColumn("[bold]Value[/]").Width(60));

        table.AddColumn("[bold]Property[/]");
        table.AddColumn("[bold]Value[/]");

        table.AddRow("Name", $"[yellow]{pattern.Name}[/]");
        table.AddRow("Category", $"[cyan]{pattern.Category}[/]");
        table.AddRow("Description", $"[dim]{pattern.Description}[/]");
        table.AddRow("Pattern", $"[green]{Markup.Escape(pattern.Pattern)}[/]");

        var panel = new Panel(table)
                 .Header("📋 Pattern Details")
                 .BorderColor(Color.Blue)
                 .Padding(1, 0, 1, 0);

        AnsiConsole.Write(panel);

        // Display examples
        if (pattern.Examples.Length > 0)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold underline]Examples:[/]");

            foreach (var example in pattern.Examples)
            {
                var result = _testingService.TestPattern(pattern.Pattern, example);
                var status = result.IsMatch ? "[green]✅[/]" : "[red]❌[/]";
                AnsiConsole.MarkupLine($"  • {status} [yellow]'{Markup.Escape(example)}'[/]");
            }
        }

        // Ask if user wants to test with custom input
        if (AnsiConsole.Confirm("[dim]Test this pattern with your own input?[/]"))
        {
            var testString = AnsiConsole.Ask<string>("[cyan]Enter test string:[/]");
            var result = _testingService.TestPattern(pattern.Pattern, testString);

            var status = result.IsMatch ? "[green]✅ Match![/]" : "[red]❌ No match[/]";
            AnsiConsole.MarkupLine($"\nResult: {status}");

            if (result.IsMatch)
            {
                AnsiConsole.MarkupLine($"Execution time: [cyan]{result.ExecutionTime.TotalMilliseconds:F2}ms[/]");
            }
        }
    }
}