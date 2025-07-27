using Spectre.Console;

namespace RegexCraft.Console.UI;

public class MenuHandler
{
    private readonly PatternTester _patternTester;
    private readonly PatternLibraryBrowser _libraryBrowser;
    private readonly PatternExplainer _explainer;
    private readonly CodeGenerator _codeGenerator;
    private readonly PerformanceTester _performanceTester;

    public MenuHandler(
        PatternTester patternTester,
        PatternLibraryBrowser libraryBrowser,
        PatternExplainer explainer,
        CodeGenerator codeGenerator,
        PerformanceTester performanceTester)
    {
        _patternTester = patternTester;
        _libraryBrowser = libraryBrowser;
        _explainer = explainer;
        _codeGenerator = codeGenerator;
        _performanceTester = performanceTester;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold green]What would you like to do?[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                            "🎯 Test Pattern",
                            "📚 Browse Pattern Library",
                            "🔍 Explain Pattern",
                            "🛠️ Generate Code",
                            "⚡ Performance Test",
                            "❌ Exit"
                    }));

            try
            {
                switch (choice)
                {
                    case "🎯 Test Pattern":
                        await _patternTester.RunAsync();
                        break;

                    case "📚 Browse Pattern Library":
                        await _libraryBrowser.RunAsync();
                        break;

                    case "🔍 Explain Pattern":
                        await _explainer.RunAsync();
                        break;

                    case "🛠️ Generate Code":
                        await _codeGenerator.RunAsync();
                        break;

                    case "⚡ Performance Test":
                        await _performanceTester.RunAsync();
                        break;

                    case "❌ Exit":
                        return;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
            System.Console.ReadKey();
            AnsiConsole.Clear();
            Program.DisplayWelcomeBanner();
        }
    }
}