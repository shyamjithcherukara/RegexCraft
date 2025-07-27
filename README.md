# RegexCraft 🔮

> A comprehensive .NET Core solution for testing, building, and understanding regular expressions with both console interface and modern Web API

[![.NET](https://img.shields.io/badge/.NET-9.0-purple.svg)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()
[![Live Demo](https://img.shields.io/badge/Demo-Live-brightgreen.svg)](https://regexcraft.cleanstack.dev/)
[![API Docs](https://img.shields.io/badge/API-Scalar-orange.svg)](https://regexcraftapi.cleanstack.dev/scalar/v1)

![RegexCraft Banner](docs/images/regexcraft-banner.png)

## 🌐 Live Demo

🚀 **Try RegexCraft Online:**
- **Web Demo**: [https://regexcraft.cleanstack.dev/](https://regexcraft.cleanstack.dev/)
- **API Documentation**: [https://regexcraftapi.cleanstack.dev/scalar/v1](https://regexcraftapi.cleanstack.dev/scalar/v1)

Experience the full power of RegexCraft without any installation! Test regex patterns, explore the pattern library, and generate code snippets directly in your browser.

## 🌟 Why RegexCraft?

### **Immediate Value**
- **🚀 [Try it now](https://regexcraft.cleanstack.dev/)** - No installation required
- **📚 Learn by doing** - Interactive pattern exploration
- **⚡ Fast & reliable** - Optimized for performance
- **🛠️ Developer-friendly** - Built by developers, for developers

## ✨ Features

### 🎯 **Console Application**
- **Interactive Pattern Testing** - Test regex patterns against sample text with real-time feedback
- **Comprehensive Pattern Library** - 25+ pre-built patterns across 6 categories
- **Regex Explainer** - Break down complex patterns into understandable components
- **Multi-Language Code Generation** - Export working code for C#, JavaScript, Python, Java, and PHP
- **Performance Metrics** - Track execution time and optimize your patterns
- **Beautiful Console UI** - Rich, colored interface powered by Spectre.Console

### 🌐 **Web API**
- **RESTful Endpoints** - Modern minimal API architecture
- **Beautiful Documentation** - Interactive Scalar API documentation
- **Rate Limiting** - Built-in protection against abuse
- **Input Validation** - Comprehensive request validation with FluentValidation
- **CORS Support** - Ready for frontend integration
- **Health Monitoring** - Health check endpoints for monitoring

![API Documentation](docs/images/scalar-api-docs.png)

## 🚀 Quick Start

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
- Git

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/regexcraft.git
   cd regexcraft
   ```

2. **Build the solution**
   ```bash
   dotnet build
   ```

3. **Run the Console Application**
   ```bash
   dotnet run --project RegexCraft.Console
   ```

4. **Run the Web API** (in a separate terminal)
   ```bash
   dotnet run --project RegexCraft.Api
   ```
   
   Navigate to: `http://localhost:5000/scalar/v1` for API documentation

> 💡 **Prefer not to install?** Try our [live demo](https://regexcraft.cleanstack.dev/) or explore the [API documentation](https://regexcraftapi.cleanstack.dev/scalar/v1) online!

## 📖 Usage Guide

### 🖥️ Console Application

#### Main Menu
```
╔══════════════════════════════════════╗
║            RegexCraft v1.0           ║
║     Regex Testing & Building Tool    ║
╚══════════════════════════════════════╝

Choose an option:
[1] 🎯 Test Pattern
[2] 📚 Browse Pattern Library
[3] 🔍 Explain Pattern
[4] 🛠️ Generate Code
[5] ⚡ Performance Test
[6] ❌ Exit
```

#### 1. Pattern Testing
Interactive testing with detailed match information:

![Console Testing](docs/images/console-testing.png)

```
Enter regex pattern: ^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$
Enter test string: user@example.com

✅ MATCH FOUND!
┌─────────────────┬─────────────────────────────────┐
│ Property        │ Value                           │
├─────────────────┼─────────────────────────────────┤
│ Pattern         │ ^[a-zA-Z0-9._%+-]+@[a-zA-Z...  │
│ Test String     │ user@example.com                │
│ Is Match        │ ✅ Yes                          │
│ Match Count     │ 1                               │
│ Execution Time  │ 0.23ms                          │
└─────────────────┴─────────────────────────────────┘
```

#### 2. Pattern Library
Browse 25+ pre-built patterns across categories:

- **Email & Web** - Email addresses, URLs, domains
- **Phone Numbers** - US and international formats
- **Dates & Time** - Various date/time formats
- **Financial** - Credit cards, currencies, tax IDs
- **Network** - IP addresses, MAC addresses, ports
- **Text Processing** - Words, usernames, passwords

#### 3. Pattern Explanation
Get detailed breakdowns of regex components:

```
Pattern: ^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$

📝 Summary
Matches the entire string that consists of multiple components

🔍 Detailed Breakdown
┌──────────────────────┬─────────────────────────────────────────┐
│ Component            │ Explanation                             │
├──────────────────────┼─────────────────────────────────────────┤
│ ^                    │ Start of string anchor                  │
│ [a-zA-Z0-9._%+-]+   │ One or more alphanumeric characters... │
│ @                    │ Literal @ symbol                        │
│ [a-zA-Z0-9.-]+      │ Domain name characters                  │
│ \.                   │ Literal dot (escaped)                   │
│ [a-zA-Z]{2,}        │ Top-level domain (2+ letters)          │
│ $                    │ End of string anchor                    │
└──────────────────────┴─────────────────────────────────────────┘
```

#### 4. Code Generation
Export patterns to multiple programming languages:

**C#**
```csharp
using System.Text.RegularExpressions;

var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
var regex = new Regex(pattern);
bool isMatch = regex.IsMatch("user@example.com");
```

**JavaScript**
```javascript
const pattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/g;
const isMatch = pattern.test("user@example.com");
```

**Python**
```python
import re

pattern = r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$'
is_match = re.match(pattern, "user@example.com") is not None
```

#### 5. Performance Testing
Benchmark your patterns with detailed metrics:

```
⚡ Performance Results
┌─────────────────┬─────────────────┐
│ Metric          │ Value           │
├─────────────────┼─────────────────┤
│ Iterations      │ 1,000           │
│ Average Time    │ 0.0234ms        │
│ Operations/sec  │ 42,735          │
│ Performance     │ ⚡ Excellent     │
└─────────────────┴─────────────────┘
```

### 🌐 Web API

The Web API provides all console functionality through RESTful endpoints with beautiful Scalar documentation.

#### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/regex/test` | Test regex pattern against string |
| `POST` | `/api/regex/explain` | Get pattern explanation |
| `POST` | `/api/regex/generate-code` | Generate code snippets |
| `POST` | `/api/regex/performance` | Performance test pattern |
| `GET` | `/api/regex/validate` | Validate pattern syntax |
| `GET` | `/api/patterns` | Get all patterns |
| `GET` | `/api/patterns/categories` | Get pattern categories |
| `GET` | `/api/patterns/category/{category}` | Get patterns by category |
| `GET` | `/api/patterns/search?query={term}` | Search patterns |
| `GET` | `/api/health` | API health check |

#### Example API Usage

**Test a Pattern**
```bash
curl -X POST "https://regexcraftapi.cleanstack.dev/api/regex/test" \
  -H "Content-Type: application/json" \
  -d '{
    "pattern": "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
    "testString": "user@example.com"
  }'
```

> 🌐 **Try it live**: Use our [interactive API documentation](https://regexcraftapi.cleanstack.dev/scalar/v1) to test endpoints directly in your browser!

**Response**
```json
{
  "pattern": "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
  "testString": "user@example.com",
  "isMatch": true,
  "matchCount": 1,
  "executionTimeMs": 0.234,
  "isValid": true,
  "matches": [
    {
      "value": "user@example.com",
      "index": 0,
      "length": 16,
      "groups": []
    }
  ]
}
```

**Generate Code**
```bash
curl -X POST "https://regexcraftapi.cleanstack.dev/api/regex/generate-code" \
  -H "Content-Type: application/json" \
  -d '{
    "pattern": "\\d{3}-\\d{2}-\\d{4}",
    "language": "python",
    "sampleText": "123-45-6789"
  }'
```

> 📖 **Complete API Reference**: Explore all endpoints, request/response schemas, and try them live at [regexcraftapi.cleanstack.dev/scalar/v1](https://regexcraftapi.cleanstack.dev/scalar/v1)

## 🏗️ Project Structure

```
RegexCraft/
├── RegexCraft.Core/              # Core business logic
│   ├── Models/                   # Data models
│   │   ├── RegexTestResult.cs
│   │   ├── PatternLibraryItem.cs
│   │   ├── RegexExplanation.cs
│   │   └── CodeSnippet.cs
│   ├── Services/                 # Business services
│   │   ├── IRegexTestingService.cs
│   │   ├── RegexTestingService.cs
│   │   ├── PatternLibraryService.cs
│   │   ├── RegexExplainerService.cs
│   │   └── CodeGeneratorService.cs
│   └── Data/
│       └── patterns.json         # Pattern library data
├── RegexCraft.Console/           # Console application
│   ├── Program.cs
│   ├── UI/
│   │   ├── MenuHandler.cs
│   │   ├── PatternTester.cs
│   │   ├── PatternLibraryBrowser.cs
│   │   ├── PatternExplainer.cs
│   │   ├── CodeGenerator.cs
│   │   └── PerformanceTester.cs
│   └── appsettings.json
├── RegexCraft.Api/               # Web API
│   ├── Program.cs
│   ├── Models/                   # DTOs
│   │   ├── RegexTestRequest.cs
│   │   ├── CodeGenerationRequest.cs
│   │   ├── PatternExplanationRequest.cs
│   │   └── PerformanceTestRequest.cs
│   └── Extensions/
│       ├── MappingExtensions.cs
│       ├── ValidationExtensions.cs
│       └── ServiceExtensions.cs
├── RegexCraft.Tests/             # Unit tests
└── docs/                         # Documentation
    └── images/                   # Screenshots
```

## 🧪 Pattern Library

| Category | Count | Examples |
|----------|-------|----------|
| **Email & Web** | 3 | Email validation, URL matching, domain names |
| **Phone Numbers** | 2 | US format, international format |
| **Dates & Time** | 3 | ISO dates, US format, 24-hour time |
| **Financial** | 3 | Credit cards, currency, SSN |
| **Network** | 4 | IPv4, IPv6, MAC addresses, ports |
| **Text Processing** | 7 | Usernames, passwords, hex colors, HTML tags |

### Sample Patterns

```regex
# Email Address
^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$

# US Phone Number
^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$

# Strong Password
^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$

# IPv4 Address
^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$

# Credit Card
^\d{4}[-\s]?\d{4}[-\s]?\d{4}[-\s]?\d{4}$
```

## 🛠️ Development

### Building from Source

```bash
# Clone repository
git clone https://github.com/yourusername/regexcraft.git
cd regexcraft

# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run tests
dotnet test

# Run console application
dotnet run --project RegexCraft.Console

# Run API (separate terminal)
dotnet run --project RegexCraft.Api
```

### Dependencies

#### Core Dependencies
- **.NET 8.0** - Core framework
- **Newtonsoft.Json** - JSON serialization
- **xUnit** - Unit testing framework

#### Console Dependencies
- **Spectre.Console** - Rich console UI
- **Microsoft.Extensions.Hosting** - Dependency injection
- **Microsoft.Extensions.DependencyInjection** - Service container

#### API Dependencies
- **Scalar.AspNetCore** - API documentation
- **FluentValidation.AspNetCore** - Request validation
- **Microsoft.AspNetCore.RateLimiting** - Rate limiting

### Performance Benchmarks

| Pattern Type | Avg. Execution Time | Performance Rating |
|--------------|--------------------|--------------------|
| Simple Email | 0.01ms | ⚡ Excellent |
| Complex URL | 0.15ms | 🟢 Very Good |
| Phone Number | 0.03ms | ⚡ Excellent |
| Strong Password | 0.12ms | 🟢 Very Good |
| IPv4 Address | 0.02ms | ⚡ Excellent |

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guidelines](#contributing-guidelines) below.

### Contributing Guidelines

#### 🚀 Getting Started

1. **Fork the repository**
   ```bash
   git fork https://github.com/yourusername/regexcraft.git
   ```

2. **Clone your fork**
   ```bash
   git clone https://github.com/yourusername/regexcraft.git
   cd regexcraft
   ```

3. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

4. **Set up development environment**
   ```bash
   dotnet restore
   dotnet build
   dotnet test
   ```

#### 📝 Development Guidelines

##### Code Style
- Follow [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Keep methods focused and small (< 20 lines preferred)

##### Testing Requirements
- **Unit Tests**: All new features must include unit tests
- **Test Coverage**: Maintain >80% code coverage
- **Integration Tests**: Add integration tests for API endpoints
- **Performance Tests**: Include performance tests for regex operations

##### Documentation
- Update README.md for new features
- Add XML documentation for public methods
- Include code examples in documentation
- Update API documentation if adding new endpoints

#### 🐛 Bug Reports

When reporting bugs, please include:

```markdown
**Describe the Bug**
A clear description of what the bug is.

**To Reproduce**
Steps to reproduce the behavior:
1. Go to '...'
2. Click on '....'
3. Scroll down to '....'
4. See error

**Expected Behavior**
What you expected to happen.

**Screenshots**
If applicable, add screenshots.

**Environment**
- OS: [e.g. Windows 11, macOS 13, Ubuntu 22.04]
- .NET Version: [e.g. 8.0.1]
- Project Version: [e.g. 1.0.0]

**Additional Context**
Any other context about the problem.
```

#### ✨ Feature Requests

For new features, please provide:

1. **Problem Statement**: What problem does this solve?
2. **Proposed Solution**: How should it work?
3. **Alternatives Considered**: What other approaches did you consider?
4. **Additional Context**: Screenshots, mockups, or examples

#### 🔧 Types of Contributions

##### 1. **Core Features**
- New regex services and functionality
- Performance optimizations
- Enhanced pattern library

##### 2. **UI/UX Improvements**
- Console interface enhancements
- Better error messages
- Improved user experience

##### 3. **API Enhancements**
- New endpoints
- Better request/response models
- Enhanced documentation

##### 4. **Documentation**
- Code documentation
- Usage examples
- Tutorial content

##### 5. **Testing**
- Unit tests
- Integration tests
- Performance benchmarks

#### 📋 Pull Request Process

1. **Before Starting**
   - Check existing issues and PRs
   - Discuss major changes in an issue first
   - Ensure your IDE is configured for the project

2. **Development Process**
   ```bash
   # Create feature branch
   git checkout -b feature/regex-optimizer
   
   # Make your changes
   # ... code changes ...
   
   # Run tests
   dotnet test
   
   # Ensure code builds
   dotnet build
   
   # Commit with descriptive message
   git commit -m "feat: add regex optimization service
   
   - Implements caching for compiled regex patterns
   - Adds performance metrics collection
   - Includes unit tests with 95% coverage
   
   Closes #123"
   ```

3. **Pull Request Template**
   ```markdown
   ## Description
   Brief description of changes
   
   ## Type of Change
   - [ ] Bug fix (non-breaking change)
   - [ ] New feature (non-breaking change)
   - [ ] Breaking change (fix or feature causing existing functionality to not work)
   - [ ] Documentation update
   
   ## Testing
   - [ ] Unit tests pass
   - [ ] Integration tests pass
   - [ ] Manual testing completed
   
   ## Checklist
   - [ ] Code follows style guidelines
   - [ ] Self-review completed
   - [ ] Documentation updated
   - [ ] Tests added/updated
   ```

4. **Review Process**
   - All PRs require at least one review
   - Address reviewer feedback promptly
   - Maintain a single commit per logical change
   - Keep PRs focused and reasonably sized

#### 🏆 Recognition

Contributors will be recognized in:
- README.md contributors section
- Release notes for significant contributions
- GitHub repository insights

#### 📞 Getting Help

- **Questions**: Open a GitHub Discussion
- **Bug Reports**: Create an Issue
- **Feature Ideas**: Start with a Discussion
- **Security Issues**: Email maintainers directly

## 📊 API Rate Limits

| Endpoint Type | Limit | Window |
|---------------|-------|--------|
| All Endpoints | 100 requests | 1 minute |
| Performance Tests | 10 requests | 1 minute |
| Bulk Operations | 20 requests | 1 minute |

## 🔒 Security

- No user data is stored or logged
- All operations are stateless
- Rate limiting prevents abuse
- Input validation on all endpoints
- No external network calls (except for API hosting)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [Spectre.Console](https://spectreconsole.net/) for the beautiful console UI
- [Scalar](https://github.com/scalar/scalar) for the modern API documentation
- [FluentValidation](https://fluentvalidation.net/) for robust input validation
- [.NET Team](https://dotnet.microsoft.com/) for the amazing framework
- [Regular Expression Community](https://regex101.com/) for inspiration and patterns

## 📞 Support

If you encounter any issues or have questions:

1. **🌐 Try the [live demo](https://regexcraft.cleanstack.dev/)** first
2. **📖 Check the [API documentation](https://regexcraftapi.cleanstack.dev/scalar/v1)**
3. **❓ Review the [FAQ](docs/FAQ.md)**
4. **🔍 Search [existing issues](https://github.com/yourusername/regexcraft/issues)**
5. **🆕 Create a [new issue](https://github.com/yourusername/regexcraft/issues/new)**
6. **💬 Start a [discussion](https://github.com/yourusername/regexcraft/discussions)**

## 🗺️ Roadmap

### Version 1.1 (Next Release)
- [ ] **Regex Debugger**: Step-through regex execution
- [ ] **Pattern Sharing**: Import/export pattern collections
- [ ] **Bulk Testing**: Test multiple strings at once
- [ ] **Regex Builder**: Visual regex construction tool

### Version 1.2 (Future)
- [ ] **Web Frontend**: React/Vue.js web interface
- [ ] **Pattern Analytics**: Usage statistics and insights
- [ ] **Team Collaboration**: Shared pattern libraries
- [ ] **CLI Tool**: Global dotnet tool installation

### Version 2.0 (Long Term)
- [ ] **AI Integration**: AI-powered pattern suggestions
- [ ] **Performance Profiler**: Advanced regex performance analysis
- [ ] **Plugin System**: Extensible architecture
- [ ] **Cloud Sync**: Pattern synchronization across devices

---

**Happy Regex Crafting!** 🎯✨

🌐 **Quick Links:**
- [Live Demo](https://regexcraft.cleanstack.dev/) - Try RegexCraft in your browser
- [API Documentation](https://regexcraftapi.cleanstack.dev/scalar/v1) - Interactive API reference
- [GitHub Repository](https://github.com/yourusername/regexcraft) - Source code and issues

---

*Made with ❤️ by Shyamjith Cherukara | [Portfolio](https://cleanstack.dev)  *
