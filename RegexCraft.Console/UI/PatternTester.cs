using RegexCraft.Core.Services.Contracts;
using Spectre.Console;

namespace RegexCraft.Console.UI;

public class PatternTester
{
    private readonly IRegexTestingService _testingService;

    public PatternTester(IRegexTestingService testingService)
    {
        _testingService = testingService;
    }

    public async Task RunAsync()
    {
        AnsiConsole.MarkupLine("[bold yellow]🎯 Pattern Tester[/]");
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

            await Task.Run(() =>
            {
                AnsiConsole.Status()
                    .Start("Testing pattern...", ctx =>
                    {
                        var result = _testingService.TestPattern(pattern, testString);
                        DisplayTestResult(result);
                    });
            });

            if (!AnsiConsole.Confirm("[dim]Test another string with this pattern?[/]"))
            {
                if (!AnsiConsole.Confirm("[dim]Test a different pattern?[/]"))
                    break;
            }

            AnsiConsole.WriteLine();
        }
    }

    private void DisplayTestResult(RegexCraft.Core.Models.RegexTestResult result)
    {
        AnsiConsole.WriteLine();

        if (result.HasError)
        {
            var panelLocal = new Panel($"[red]{result.ErrorMessage}[/]")
                .Header("❌ Error")
                .BorderColor(Color.Red);
            AnsiConsole.Write(panelLocal);
            return;
        }

        // Create result table
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(result.IsMatch ? Color.Green : Color.Yellow);

        table.AddColumn(new TableColumn("[bold]Property[/]").Width(15));
        table.AddColumn(new TableColumn("[bold]Value[/]").Width(50));

        table.AddRow("Pattern", $"[dim]{Markup.Escape(result.Pattern)}[/]");
        table.AddRow("Test String", $"[dim]{Markup.Escape(result.TestString)}[/]");
        table.AddRow("Is Match", result.IsMatch ? "[green]✅ Yes[/]" : "[yellow]❌ No[/]");
        table.AddRow("Match Count", result.MatchCount.ToString());
        table.AddRow("Execution Time", $"[cyan]{result.ExecutionTime.TotalMilliseconds:F2}ms[/]");

        var title = result.IsMatch ? "✅ Match Found!" : "❌ No Match";
        var panel = new Panel(table)
                     .Header(new PanelHeader(title))
                     .BorderColor(result.IsMatch ? Color.Green : Color.Yellow)
                     .Padding(1, 0, 1, 0);

        AnsiConsole.Write(panel);

        // Display match details if any
        if (result.IsMatch && result.Matches.Length > 0)
        {
            AnsiConsole.WriteLine();

            var matchPanel = new Panel(
                string.Join("\n", result.Matches.Select((match, index) =>
                {
                    var matchInfo = $"[green]Match {index + 1}:[/] [yellow]'{Markup.Escape(match.Value)}'[/] at position [cyan]{match.Index}-{match.Index + match.Length - 1}[/]";
                    var groups = "";

                    if (match.Groups.Count > 1)
                    {
                        var groupLines = new List<string>();
                        for (int g = 1; g < match.Groups.Count; g++)
                        {
                            var group = match.Groups[g];
                            if (group.Success)
                            {
                                groupLines.Add($"  └─ [dim]Group {g}:[/] [yellow]'{Markup.Escape(group.Value)}'[/] at [cyan]{group.Index}-{group.Index + group.Length - 1}[/]");
                            }
                        }
                        groups = groupLines.Count > 0 ? "\n" + string.Join("\n", groupLines) : "";
                    }

                    return matchInfo + groups;
                }))
            )
            .Header("🎯 Match Details")
            .BorderColor(Color.Aqua)
            .Padding(1, 0, 1, 0);

            AnsiConsole.Write(matchPanel);
        }
    }
}