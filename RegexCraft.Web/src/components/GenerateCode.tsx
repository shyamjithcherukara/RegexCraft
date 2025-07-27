import React, { useState } from 'react';
import { Button } from './ui/button';
import { Input } from './ui/input';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from './ui/card';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from './ui/select';
import { Code, Loader2, Copy } from 'lucide-react';
import { api } from '../lib/api';
import type { GenerateCodeResponse } from '../lib/api';

const GenerateCode: React.FC = () => {
  const [pattern, setPattern] = useState('');
  const [language, setLanguage] = useState('javascript');
  const [loading, setLoading] = useState(false);
  const [result, setResult] = useState<GenerateCodeResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!pattern.trim()) return;

    setLoading(true);
    setError(null);
    setResult(null);

    try {
      const response = await api.generateCode({
        pattern: pattern.trim(),
        sampleText: undefined,
        language: language.trim() || undefined,
      });
      setResult(response);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setLoading(false);
    }
  };

  const copyToClipboard = (text: string) => {
    navigator.clipboard.writeText(text);
  };

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle className="flex items-center space-x-2">
            <Code className="h-5 w-5" />
            <span>Generate Code</span>
          </CardTitle>
          <CardDescription>
            Generate code snippets for your regex pattern in various programming languages
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit} className="space-y-6">
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
              <div className="space-y-3">
                <label htmlFor="pattern" className="text-sm font-medium text-gray-700 dark:text-gray-300">
                  Regex Pattern
                </label>
                <Input
                  id="pattern"
                  value={pattern}
                  onChange={(e) => setPattern(e.target.value)}
                  placeholder="e.g., [a-z]+"
                  className="w-full"
                  required
                />
              </div>
              <div className="space-y-3">
                <label htmlFor="language" className="text-sm font-medium text-gray-700 dark:text-gray-300">
                  Programming Language
                </label>
                <Select value={language} onValueChange={setLanguage}>
                  <SelectTrigger className="w-full">
                    <SelectValue placeholder="Select language" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="javascript">JavaScript</SelectItem>
                    <SelectItem value="python">Python</SelectItem>
                    <SelectItem value="java">Java</SelectItem>
                    <SelectItem value="csharp">C#</SelectItem>
                    <SelectItem value="php">PHP</SelectItem>
                    <SelectItem value="ruby">Ruby</SelectItem>
                    <SelectItem value="go">Go</SelectItem>
                    <SelectItem value="rust">Rust</SelectItem>
                  </SelectContent>
                </Select>
              </div>
            </div>
            <div className="flex justify-center">
              <Button type="submit" disabled={loading} className="bg-gradient-to-r from-purple-600 via-pink-600 to-orange-500 hover:from-purple-700 hover:via-pink-700 hover:to-orange-600 text-white font-medium px-8 py-2 rounded-lg transition-all duration-200 hover:shadow-lg hover:scale-105 active:scale-95">
                {loading ? (
                  <>
                    <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                    Generating...
                  </>
                ) : (
                  <>
                    <Code className="mr-2 h-4 w-4" />
                    Generate Code
                  </>
                )}
              </Button>
            </div>
          </form>
        </CardContent>
      </Card>

      {error && (
        <Card className="border-destructive">
          <CardContent className="pt-6">
            <div className="flex items-center space-x-2 text-destructive">
              <Code className="h-5 w-5" />
              <span className="font-medium">Error</span>
            </div>
            <p className="mt-2 text-sm">{error}</p>
          </CardContent>
        </Card>
      )}

      {result && (
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center space-x-2">
              <Code className="h-5 w-5" />
              <span>Generated Code Snippets</span>
            </CardTitle>
          </CardHeader>
          <CardContent className="space-y-4">
            {result.snippets && result.snippets.length > 0 ? (
              result.snippets.map((snippet, index) => (
                <div key={index} className="border rounded-lg p-4">
                  <div className="flex items-center justify-between mb-3">
                    <h4 className="font-medium text-primary">{snippet.language}</h4>
                    <Button
                      variant="outline"
                      size="sm"
                      onClick={() => copyToClipboard(snippet.code)}
                      className="h-8"
                    >
                      <Copy className="h-4 w-4 mr-1" />
                      Copy
                    </Button>
                  </div>
                  <p className="text-sm text-muted-foreground mb-3">
                    {snippet.description}
                  </p>
                  <pre className="bg-muted p-3 rounded text-sm overflow-x-auto">
                    <code>{snippet.code}</code>
                  </pre>
                </div>
              ))
            ) : (
              <p className="text-muted-foreground">No code snippets generated.</p>
            )}
          </CardContent>
        </Card>
      )}
    </div>
  );
};

export default GenerateCode; 