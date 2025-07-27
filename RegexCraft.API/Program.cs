using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RegexCraft.API.Extensions;
using RegexCraft.API.Models;
using RegexCraft.Core.Services.Contracts;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRegexCraftApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = "RegexCraft API";
    options.DarkMode = true;
    options.Theme = "mars";
});

app.UseCors("RegexCraftPolicy");
app.UseRateLimiter();

var api = app.MapGroup("/api")
    .RequireRateLimiting("RegexApi")
    .WithTags("RegexCraft API")
    .WithOpenApi();

// Test regex pattern
api.MapPost("/regex/test", async (
    RegexTestRequest request,
    IValidator<RegexTestRequest> validator,
    IRegexTestingService testingService) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    var result = testingService.TestPattern(request.Pattern, request.TestString);
    return Results.Ok(result.ToResponse());
})
.WithName("TestRegexPattern")
.WithSummary("Test a regex pattern against a string")
.WithDescription("Tests if a regex pattern matches against the provided test string and returns detailed match information.")
.Produces<RegexTestResponse>(200)
.Produces<ValidationProblemDetails>(400);

// Explain regex pattern
api.MapPost("/regex/explain", async (
    PatternExplanationRequest request,
    IValidator<PatternExplanationRequest> validator,
    IRegexExplainerService explainerService) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    var explanation = explainerService.ExplainPattern(request.Pattern);
    return Results.Ok(explanation.ToResponse());
})
.WithName("ExplainRegexPattern")
.WithSummary("Get detailed explanation of a regex pattern")
.WithDescription("Breaks down a regex pattern into its components and provides plain English explanations.")
.Produces<PatternExplanationResponse>(200)
.Produces<ValidationProblemDetails>(400);

// Generate code snippets
api.MapPost("/regex/generate-code", async (
    CodeGenerationRequest request,
    IValidator<CodeGenerationRequest> validator,
    ICodeGeneratorService codeGeneratorService) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    var snippets = string.IsNullOrEmpty(request.Language)
        ? codeGeneratorService.GenerateCode(request.Pattern, request.SampleText)
        : [codeGeneratorService.GenerateCodeForLanguage(request.Pattern, request.Language, request.SampleText)];

    return Results.Ok(new CodeGenerationResponse
    {
        Snippets = snippets.Select(s => s.ToDto()).ToArray()
    });
})
.WithName("GenerateCodeSnippets")
.WithSummary("Generate code snippets for regex pattern")
.WithDescription("Generates code snippets in various programming languages for the given regex pattern.")
.Produces<CodeGenerationResponse>(200)
.Produces<ValidationProblemDetails>(400);

// Performance test
api.MapPost("/regex/performance", async (
    PerformanceTestRequest request,
    IValidator<PerformanceTestRequest> validator,
    IRegexTestingService testingService) =>
{
    var validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    var singleTest = testingService.TestPattern(request.Pattern, request.TestString);
    var avgTime = testingService.MeasurePerformance(request.Pattern, request.TestString, request.Iterations);

    var rating = avgTime.TotalMilliseconds switch
    {
        < 0.01 => "Excellent",
        < 0.1 => "Very Good",
        < 1 => "Good",
        < 10 => "Moderate",
        _ => "Slow"
    };

    return Results.Ok(new PerformanceTestResponse
    {
        Pattern = request.Pattern,
        TestString = request.TestString,
        Iterations = request.Iterations,
        SingleExecutionTimeMs = singleTest.ExecutionTime.TotalMilliseconds,
        AverageExecutionTimeMs = avgTime.TotalMilliseconds,
        OperationsPerSecond = 1000 / avgTime.TotalMilliseconds,
        IsMatch = singleTest.IsMatch,
        PerformanceRating = rating
    });
})
.WithName("PerformanceTestRegex")
.WithSummary("Performance test a regex pattern")
.WithDescription("Measures the performance of a regex pattern over multiple iterations.")
.Produces<PerformanceTestResponse>(200)
.Produces<ValidationProblemDetails>(400);

// Validate regex pattern
api.MapGet("/regex/validate", (
    string pattern,
    IRegexTestingService testingService) =>
{
    var isValid = testingService.IsValidPattern(pattern);
    return Results.Ok(new { Pattern = pattern, IsValid = isValid });
})
.WithName("ValidateRegexPattern")
.WithSummary("Validate if a regex pattern is syntactically correct")
.WithDescription("Checks if the provided regex pattern has valid syntax.")
.Produces<object>(200);

// Get all patterns
api.MapGet("/patterns", async (IPatternLibraryService libraryService) =>
{
    var patterns = await libraryService.GetAllPatternsAsync();
    return Results.Ok(new PatternLibraryResponse
    {
        Patterns = patterns.Select(p => p.ToDto()).ToArray(),
        Count = patterns.Length
    });
})
.WithName("GetAllPatterns")
.WithSummary("Get all regex patterns from the library")
.WithDescription("Returns all available regex patterns with their descriptions and examples.")
.Produces<PatternLibraryResponse>(200);

// Get pattern categories
api.MapGet("/patterns/categories", async (IPatternLibraryService libraryService) =>
{
    var categories = await libraryService.GetCategoriesAsync();
    return Results.Ok(new { Categories = categories, Count = categories.Length });
})
.WithName("GetPatternCategories")
.WithSummary("Get all pattern categories")
.WithDescription("Returns all available pattern categories.")
.Produces<object>(200);

// Get patterns by category
api.MapGet("/patterns/category/{category}", async (
    string category,
    IPatternLibraryService libraryService) =>
{
    var patterns = await libraryService.GetPatternsByCategoryAsync(category);
    return Results.Ok(new PatternLibraryResponse
    {
        Patterns = patterns.Select(p => p.ToDto()).ToArray(),
        Count = patterns.Length
    });
})
.WithName("GetPatternsByCategory")
.WithSummary("Get patterns by category")
.WithDescription("Returns all regex patterns in the specified category.")
.Produces<PatternLibraryResponse>(200);

// Search patterns
api.MapGet("/patterns/search", async (
    string query,
    IPatternLibraryService libraryService) =>
{
    var patterns = await libraryService.SearchPatternsAsync(query);
    return Results.Ok(new PatternLibraryResponse
    {
        Patterns = patterns.Select(p => p.ToDto()).ToArray(),
        Count = patterns.Length
    });
})
.WithName("SearchPatterns")
.WithSummary("Search patterns by keyword")
.WithDescription("Searches for regex patterns by name, description, or category.")
.Produces<PatternLibraryResponse>(200);

// Health check
api.MapGet("/health", () => Results.Ok(new
{
    Status = "Healthy",
    Timestamp = DateTime.UtcNow,
    Version = "1.0.0"
}))
.WithName("HealthCheck")
.WithSummary("API health check")
.WithDescription("Returns the health status of the API.")
.Produces<object>(200);

app.Run();