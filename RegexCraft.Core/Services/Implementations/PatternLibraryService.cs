using Newtonsoft.Json;
using RegexCraft.Core.Models;
using RegexCraft.Core.Services.Contracts;
using System.Reflection;

namespace RegexCraft.Core.Services.Implementations;

public class PatternLibraryService : IPatternLibraryService
{

    private readonly Lazy<Task<PatternLibraryItem[]>> _patterns;

    public PatternLibraryService()
    {
        _patterns = new Lazy<Task<PatternLibraryItem[]>>(LoadPatternsAsync);
    }

    public async Task<PatternLibraryItem[]> GetAllPatternsAsync()
    {
        return await _patterns.Value;
    }

    public async Task<PatternLibraryItem[]> GetPatternsByCategoryAsync(string category)
    {
        var patterns = await _patterns.Value;
        return patterns.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToArray();
    }

    public async Task<string[]> GetCategoriesAsync()
    {
        var patterns = await _patterns.Value;
        return patterns.Select(p => p.Category).Distinct().OrderBy(c => c).ToArray();
    }

    public async Task<PatternLibraryItem?> GetPatternByNameAsync(string name)
    {
        var patterns = await _patterns.Value;
        return patterns.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<PatternLibraryItem[]> SearchPatternsAsync(string searchTerm)
    {
        var patterns = await _patterns.Value;
        return patterns.Where(p =>
            p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        ).ToArray();
    }

    private async Task<PatternLibraryItem[]> LoadPatternsAsync()
    {
        try
        {
            // Try to load from embedded resource first
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "RegexCraft.Core.Data.patterns.json";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<PatternLibraryData>(json);
                return data?.Patterns ?? GetDefaultPatterns();
            }
        }
        catch
        {
            // Fall back to default patterns if loading fails
        }

        return GetDefaultPatterns();
    }

    private PatternLibraryItem[] GetDefaultPatterns()
    {
        return new[]
        {
                // Email & Web
                new PatternLibraryItem
                {
                    Name = "Email Address",
                    Pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
                    Description = "Validates standard email addresses",
                    Category = "Email & Web",
                    Examples = new[] { "user@example.com", "test.email+tag@domain.co.uk", "user123@sub.domain.org" }
                },
                new PatternLibraryItem
                {
                    Name = "URL",
                    Pattern = @"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$",
                    Description = "Matches HTTP and HTTPS URLs",
                    Category = "Email & Web",
                    Examples = new[] { "https://example.com", "http://www.google.com/search?q=regex", "https://sub.domain.co.uk/path" }
                },
                new PatternLibraryItem
                {
                    Name = "Domain Name",
                    Pattern = @"^[a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?(\.[a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?)*$",
                    Description = "Validates domain names",
                    Category = "Email & Web",
                    Examples = new[] { "example.com", "sub.domain.org", "test-site.co.uk" }
                },

                // Phone Numbers
                new PatternLibraryItem
                {
                    Name = "US Phone Number",
                    Pattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                    Description = "US phone number in various formats",
                    Category = "Phone Numbers",
                    Examples = new[] { "(555) 123-4567", "555-123-4567", "555.123.4567", "5551234567" }
                },
                new PatternLibraryItem
                {
                    Name = "International Phone",
                    Pattern = @"^\+?[1-9]\d{1,14}$",
                    Description = "International phone number format",
                    Category = "Phone Numbers",
                    Examples = new[] { "+1234567890", "+44 20 7946 0958", "+91 98765 43210" }
                },

                // Dates & Time
                new PatternLibraryItem
                {
                    Name = "Date (YYYY-MM-DD)",
                    Pattern = @"^\d{4}-\d{2}-\d{2}$",
                    Description = "ISO date format",
                    Category = "Dates & Time",
                    Examples = new[] { "2023-12-25", "2024-01-01", "1999-02-28" }
                },
                new PatternLibraryItem
                {
                    Name = "Date (MM/DD/YYYY)",
                    Pattern = @"^(0[1-9]|1[0-2])\/(0[1-9]|[12]\d|3[01])\/\d{4}$",
                    Description = "US date format",
                    Category = "Dates & Time",
                    Examples = new[] { "12/25/2023", "01/01/2024", "02/28/1999" }
                },
                new PatternLibraryItem
                {
                    Name = "Time (24-hour)",
                    Pattern = @"^([01]\d|2[0-3]):([0-5]\d)$",
                    Description = "24-hour time format",
                    Category = "Dates & Time",
                    Examples = new[] { "14:30", "09:15", "23:59", "00:00" }
                },

                // Financial
                new PatternLibraryItem
                {
                    Name = "Credit Card",
                    Pattern = @"^\d{4}[-\s]?\d{4}[-\s]?\d{4}[-\s]?\d{4}$",
                    Description = "Credit card number format",
                    Category = "Financial",
                    Examples = new[] { "1234-5678-9012-3456", "1234 5678 9012 3456", "1234567890123456" }
                },
                new PatternLibraryItem
                {
                    Name = "Currency (USD)",
                    Pattern = @"^\$\d{1,3}(,\d{3})*(\.\d{2})?$",
                    Description = "US dollar currency format",
                    Category = "Financial",
                    Examples = new[] { "$123.45", "$1,234.56", "$1,000", "$12" }
                },
                new PatternLibraryItem
                {
                    Name = "SSN",
                    Pattern = @"^\d{3}-\d{2}-\d{4}$",
                    Description = "US Social Security Number",
                    Category = "Financial",
                    Examples = new[] { "123-45-6789", "987-65-4321" }
                },

                // Network
                new PatternLibraryItem
                {
                    Name = "IPv4 Address",
                    Pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
                    Description = "IPv4 address validation",
                    Category = "Network",
                    Examples = new[] { "192.168.1.1", "10.0.0.1", "255.255.255.255", "127.0.0.1" }
                },
                new PatternLibraryItem
                {
                    Name = "MAC Address",
                    Pattern = @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$",
                    Description = "MAC address format",
                    Category = "Network",
                    Examples = new[] { "00:1A:2B:3C:4D:5E", "AA-BB-CC-DD-EE-FF", "12:34:56:78:90:AB" }
                },

                // Text Processing
                new PatternLibraryItem
                {
                    Name = "Word",
                    Pattern = @"^\b[a-zA-Z]+\b$",
                    Description = "Single word (letters only)",
                    Category = "Text Processing",
                    Examples = new[] { "hello", "World", "regex" }
                },
                new PatternLibraryItem
                {
                    Name = "Username",
                    Pattern = @"^[a-zA-Z0-9_]{3,20}$",
                    Description = "Username (3-20 chars, alphanumeric + underscore)",
                    Category = "Text Processing",
                    Examples = new[] { "user123", "john_doe", "test_user_2024" }
                },
                new PatternLibraryItem
                {
                    Name = "Password (Strong)",
                    Pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                    Description = "Strong password (8+ chars, upper, lower, digit, special)",
                    Category = "Text Processing",
                    Examples = new[] { "MyPass123!", "StrongP@ssw0rd", "SecureKey$42" }
                }
            };
    }

    private class PatternLibraryData
    {
        public PatternLibraryItem[] Patterns { get; set; } = Array.Empty<PatternLibraryItem>();
    }
}
