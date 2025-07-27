import React, { useState } from 'react';
import { Button } from './ui/button';
import { Input } from './ui/input';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from './ui/card';
import { Zap, Loader2, Info } from 'lucide-react';
import { api } from '../lib/api';
import type { ExplainRegexResponse } from '../lib/api';

const ExplainRegex: React.FC = () => {
  const [pattern, setPattern] = useState('');
  const [loading, setLoading] = useState(false);
  const [result, setResult] = useState<ExplainRegexResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!pattern.trim()) return;

    setLoading(true);
    setError(null);
    setResult(null);

    try {
      const response = await api.explainRegex(pattern.trim());
      setResult(response);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle className="flex items-center space-x-2">
            <Zap className="h-5 w-5" />
            <span>Explain Regex Pattern</span>
          </CardTitle>
          <CardDescription>
            Get a detailed explanation of what your regex pattern does
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit} className="space-y-6">
            <div className="space-y-4">
              <div className="space-y-3">
                <label htmlFor="pattern" className="text-sm font-medium text-gray-700 dark:text-gray-300">
                  Regex Pattern
                </label>
                <Input
                  id="pattern"
                  value={pattern}
                  onChange={(e) => setPattern(e.target.value)}
                  placeholder="Enter a regex pattern to explain..."
                  className="w-full"
                  required
                />
              </div>
            </div>
            <div className="flex justify-center">
              <Button type="submit" disabled={loading} className="bg-gradient-to-r from-purple-600 via-pink-600 to-orange-500 hover:from-purple-700 hover:via-pink-700 hover:to-orange-600 text-white font-medium px-8 py-2 rounded-lg transition-all duration-200 hover:shadow-lg hover:scale-105 active:scale-95">
                {loading ? (
                  <>
                    <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                    Explaining...
                  </>
                ) : (
                  <>
                    <Zap className="mr-2 h-4 w-4" />
                    Explain Pattern
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
              <Info className="h-5 w-5" />
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
              <Info className="h-5 w-5" />
              <span>Explanation</span>
            </CardTitle>
          </CardHeader>
          <CardContent className="space-y-6">
            <div>
              <h4 className="font-medium mb-2">Pattern</h4>
              <code className="block p-3 bg-muted rounded text-sm break-all">
                {result.pattern}
              </code>
            </div>

            <div>
              <h4 className="font-medium mb-2">Summary</h4>
              <p className="text-sm leading-relaxed">{result.summary}</p>
            </div>

            {result.parts && result.parts.length > 0 && (
              <div>
                <h4 className="font-medium mb-3">Detailed Breakdown</h4>
                <div className="space-y-3">
                  {result.parts.map((part, index) => (
                    <div key={index} className="p-3 border rounded-lg">
                      <div className="flex items-start justify-between mb-2">
                        <code className="text-sm font-medium bg-muted px-2 py-1 rounded">
                          {part.component}
                        </code>
                      </div>
                      <p className="text-sm text-muted-foreground mb-2">
                        {part.explanation}
                      </p>
                      {part.example && (
                        <div className="text-xs">
                          <span className="font-medium">Example: </span>
                          <code className="bg-muted px-1 py-0.5 rounded">
                            {part.example}
                          </code>
                        </div>
                      )}
                    </div>
                  ))}
                </div>
              </div>
            )}
          </CardContent>
        </Card>
      )}
    </div>
  );
};

export default ExplainRegex; 