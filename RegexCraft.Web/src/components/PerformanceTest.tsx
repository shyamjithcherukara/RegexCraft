import React, { useState } from 'react';
import { Button } from './ui/button';
import { Input } from './ui/input';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from './ui/card';
import { BarChart3, Loader2, Clock, Zap } from 'lucide-react';
import { api } from '../lib/api';
import type { PerformanceTestResponse } from '../lib/api';

const PerformanceTest: React.FC = () => {
  const [pattern, setPattern] = useState('');
  const [testString, setTestString] = useState('');
  const [iterations, setIterations] = useState('100');
  const [loading, setLoading] = useState(false);
  const [result, setResult] = useState<PerformanceTestResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!pattern.trim() || !testString.trim()) return;

    setLoading(true);
    setError(null);
    setResult(null);

    try {
      const response = await api.performanceTest({
        pattern: pattern.trim(),
        testString: testString.trim(),
        iterations: parseInt(iterations, 10) || 100,
      });
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
            <BarChart3 className="h-5 w-5" />
            <span>Performance Test</span>
          </CardTitle>
          <CardDescription>
            Test the performance of your regex pattern with multiple iterations
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit} className="space-y-6">
            <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
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
                <label htmlFor="testString" className="text-sm font-medium text-gray-700 dark:text-gray-300">
                  Test String
                </label>
                <Input
                  id="testString"
                  value={testString}
                  onChange={(e) => setTestString(e.target.value)}
                  placeholder="Enter text to test against the pattern"
                  className="w-full"
                  required
                />
              </div>
              <div className="space-y-3">
                <label htmlFor="iterations" className="text-sm font-medium text-gray-700 dark:text-gray-300">
                  Iterations
                </label>
                <Input
                  id="iterations"
                  type="number"
                  value={iterations}
                  onChange={(e) => setIterations(e.target.value)}
                  placeholder="100"
                  min="1"
                  max="10000"
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
                    Testing...
                  </>
                ) : (
                  <>
                    <BarChart3 className="mr-2 h-4 w-4" />
                    Run Performance Test
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
              <BarChart3 className="h-5 w-5" />
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
              <BarChart3 className="h-5 w-5" />
              <span>Performance Results</span>
            </CardTitle>
          </CardHeader>
          <CardContent className="space-y-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <h4 className="font-medium mb-2">Pattern</h4>
                <code className="block p-2 bg-muted rounded text-sm break-all">
                  {result.pattern}
                </code>
              </div>
              <div>
                <h4 className="font-medium mb-2">Test String</h4>
                <code className="block p-2 bg-muted rounded text-sm break-all">
                  {result.testString}
                </code>
              </div>
            </div>

            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              <div className="p-3 border rounded-lg">
                <div className="flex items-center space-x-2 mb-1">
                  <Clock className="h-4 w-4 text-muted-foreground" />
                  <span className="text-sm font-medium">Single Execution</span>
                </div>
                <p className="text-lg font-semibold">
                  {result.singleExecutionTimeMs.toFixed(2)} ms
                </p>
              </div>

              <div className="p-3 border rounded-lg">
                <div className="flex items-center space-x-2 mb-1">
                  <BarChart3 className="h-4 w-4 text-muted-foreground" />
                  <span className="text-sm font-medium">Average Time</span>
                </div>
                <p className="text-lg font-semibold">
                  {result.averageExecutionTimeMs.toFixed(2)} ms
                </p>
              </div>

              <div className="p-3 border rounded-lg">
                <div className="flex items-center space-x-2 mb-1">
                  <Zap className="h-4 w-4 text-muted-foreground" />
                  <span className="text-sm font-medium">Operations/sec</span>
                </div>
                <p className="text-lg font-semibold">
                  {result.operationsPerSecond.toFixed(2)}
                </p>
              </div>

              <div className="p-3 border rounded-lg">
                <div className="flex items-center space-x-2 mb-1">
                  <span className="text-sm font-medium">Match</span>
                </div>
                <p className={`text-lg font-semibold ${result.isMatch ? 'text-green-600' : 'text-red-600'}`}>
                  {result.isMatch ? 'Yes' : 'No'}
                </p>
              </div>
            </div>

            <div>
              <h4 className="font-medium mb-2">Performance Rating</h4>
              <p className="text-sm text-muted-foreground">{result.performanceRating}</p>
            </div>
          </CardContent>
        </Card>
      )}
    </div>
  );
};

export default PerformanceTest; 