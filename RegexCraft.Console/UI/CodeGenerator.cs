using RegexCraft.Core.Services.Contracts;
using Spectre.Console;

namespace RegexCraft.Console.UI;

public class CodeGenerator
{
    private readonly ICodeGeneratorService _codeGeneratorService;

    public CodeGenerator(ICodeGeneratorService codeGeneratorService)
    {
        _codeGeneratorService = codeGeneratorService;
    }

    public async Task RunAsync()
    {
        AnsiConsole.MarkupLine("[bold yellow]🛠️ Code Generator[/]");
        AnsiConsole.WriteLine();

        while (true)
        {
            var pattern = AnsiConsole.Ask<string>("[green]Enter regex pattern:[/]");

            if (string.IsNullOrWhiteSpace(pattern))
            {
                AnsiConsole.MarkupLine("[red]Pattern cannot be empty![/]");
                continue;
            }

            var sampleText = AnsiConsole.Ask<string>("[cyan]Enter sample text (optional):[/]", string.Empty);

            var languages = _codeGeneratorService.GetSupportedLanguages();
            var selectedLanguage = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Select programming language:[/]")
                    .AddChoices(languages)
                    .AddChoices("All Languages"));

            await Task.Run(() =>
            {
                AnsiConsole.Status()
                    .Start("Generating code...", ctx =>
                    {
                        if (selectedLanguage == "All Languages")
                        {
                            var allSnippets = _codeGeneratorService.GenerateCode(pattern, sampleText);
                            DisplayAllCodeSnippets(allSnippets);
                        }
                        else
                        {
                            var snippet = _codeGeneratorService.GenerateCodeForLanguage(pattern, selectedLanguage, sampleText);
                            DisplayCodeSnippet(snippet);
                        }
                    });
            });

            if (!AnsiConsole.Confirm("[dim]Generate code for another pattern?[/]"))
                break;

            AnsiConsole.WriteLine();
        }
    }

    private void DisplayAllCodeSnippets(RegexCraft.Core.Models.CodeSnippet[] snippets)
    {
        AnsiConsole.WriteLine();

        foreach (var snippet in snippets)
        {
            DisplayCodeSnippet(snippet);
            AnsiConsole.WriteLine();
        }
    }

    private void DisplayCodeSnippet(RegexCraft.Core.Models.CodeSnippet snippet)
    {
        var panel = new Panel($"[dim]{Markup.Escape(snippet.Description)}[/]\n\n[white]{Markup.Escape(snippet.Code)}[/]")
                    .Header($"💻 {snippet.Language}")
                    .BorderColor(Color.Blue)
                    .Padding(1, 0, 1, 0);

        AnsiConsole.Write(panel);
    }
}