using RegexCraft.Core.Services.Contracts;
using Spectre.Console;

namespace RegexCraft.Console.UI;

public class PatternExplainer
{
    private readonly IRegexExplainerService _explainerService;

    public PatternExplainer(IRegexExplainerService explainerService)
    {
        _explainerService = explainerService;
    }

    public async Task RunAsync()
    {
        AnsiConsole.MarkupLine("[bold yellow]🔍 Pattern Explainer[/]");
        AnsiConsole.WriteLine();

        while (true)
        {
            var pattern = AnsiConsole.Ask<string>("[green]Enter regex pattern to explain:[/]");

            if (string.IsNullOrWhiteSpace(pattern))
            {
                AnsiConsole.MarkupLine("[red]Pattern cannot be empty![/]");
                continue;
            }

            await Task.Run(() =>
            {
                AnsiConsole.Status()
                    .Start("Analyzing pattern...", ctx =>
                    {
                        var explanation = _explainerService.ExplainPattern(pattern);
                        DisplayExplanation(explanation);
                    });
            });

            if (!AnsiConsole.Confirm("[dim]Explain another pattern?[/]"))
                break;

            AnsiConsole.WriteLine();
        }
    }

    private void DisplayExplanation(RegexCraft.Core.Models.RegexExplanation explanation)
    {
        AnsiConsole.WriteLine();

        // Display summary
        var summaryPanel = new Panel($"[cyan]{Markup.Escape(explanation.Summary)}[/]")
         .Header("📝 Summary")
         .BorderColor(Color.Aqua)
         .Padding(1, 0, 1, 0);
        AnsiConsole.Write(summaryPanel);

        AnsiConsole.WriteLine();

        // Display detailed breakdown
        if (explanation.Parts.Count > 0)
        {
            var table = new Table()
                 .Border(TableBorder.Rounded)
                 .BorderColor(Color.Green);

            table.AddColumn(new TableColumn("[bold]Component[/]").Width(20));
            table.AddColumn(new TableColumn("[bold]Explanation[/]").Width(60));

            table.AddColumn("[bold]Component[/]");
            table.AddColumn("[bold]Explanation[/]");

            
            foreach (var part in explanation.Parts)
            {
                table.AddRow(
                    $"[yellow]{Markup.Escape(part.Component)}[/]",
                    $"[dim]{Markup.Escape(part.Explanation)}[/]"
                );
            }

            var detailPanel = new Panel(table)
                        .Header("🔍 Detailed Breakdown")
                        .BorderColor(Color.Green)
                        .Padding(1, 0, 1, 0);

            AnsiConsole.Write(detailPanel);
        }

        // Display the pattern with syntax highlighting
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold underline]Pattern:[/]");
        AnsiConsole.MarkupLine($"  [green]{Markup.Escape(explanation.Pattern)}[/]");
    }
}