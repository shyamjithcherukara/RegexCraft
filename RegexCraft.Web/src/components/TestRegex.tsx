import React, { useState } from 'react';
import { Button } from './ui/button';
import { Input } from './ui/input';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from './ui/card';
import { Search, CheckCircle, XCircle, Loader2 } from 'lucide-react';
import { api } from '../lib/api';
import { useAnalytics } from '../hooks/useAnalytics';
import type { TestRegexResponse } from '../lib/api';

const TestRegex: React.FC = () => {
  const [pattern, setPattern] = useState('');
  const [testString, setTestString] = useState('');
  const [loading, setLoading] = useState(false);
  const [result, setResult] = useState<TestRegexResponse | null>(null);
  const [error, setError] = useState<string | null>(null);
  const { trackFormSubmission, trackApiCall, trackError } = useAnalytics();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!pattern.trim() || !testString.trim()) {
      setError('Please enter both pattern and test string');
      return;
    }

    setLoading(true);
    setError(null);
    setResult(null);

    // Track form submission
    trackFormSubmission('test_regex');

    try {
      const response = await api.testRegex({
        pattern: pattern.trim(),
        testString: testString.trim()
      });
      setResult(response);
      trackApiCall('/api/test-regex', true);
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'An error occurred';
      setError(errorMessage);
      trackApiCall('/api/test-regex', false);
      trackError('api_error', errorMessage);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="space-y-6">
      <Card>
        <CardHeader>
          <CardTitle className="flex items-center space-x-2">
            <Search className="h-5 w-5" />
            <span>Test Regex Pattern</span>
          </CardTitle>
          <CardDescription>
            Enter a regex pattern and test string to see if they match
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
                    <Search className="mr-2 h-4 w-4" />
                    Test Pattern
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
              <XCircle className="h-5 w-5" />
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
              {result.isMatch ? (
                <CheckCircle className="h-5 w-5 text-green-600" />
              ) : (
                <XCircle className="h-5 w-5 text-red-600" />
              )}
              <span>Test Results</span>
            </CardTitle>
          </CardHeader>
          <CardContent className="space-y-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <h4 className="font-medium mb-2">Pattern</h4>
                <code className="block p-2 bg-muted rounded text-sm break-all">
                  {pattern}
                </code>
              </div>
              <div>
                <h4 className="font-medium mb-2">Test String</h4>
                <code className="block p-2 bg-muted rounded text-sm break-all">
                  {testString}
                </code>
              </div>
            </div>

            <div className="flex items-center space-x-2">
              <span className="font-medium">Match:</span>
              {result.isMatch ? (
                <span className="flex items-center space-x-1 text-green-600">
                  <CheckCircle className="h-4 w-4" />
                  <span>Yes</span>
                </span>
              ) : (
                <span className="flex items-center space-x-1 text-red-600">
                  <XCircle className="h-4 w-4" />
                  <span>No</span>
                </span>
              )}
            </div>

            {result.matches && result.matches.length > 0 && (
              <div>
                <h4 className="font-medium mb-2">Matches</h4>
                <div className="space-y-1">
                  {result.matches.map((match, index) => (
                    <div key={index} className="p-2 bg-muted rounded text-sm">
                      {match}
                    </div>
                  ))}
                </div>
              </div>
            )}

            {result.groups && result.groups.length > 0 && (
              <div>
                <h4 className="font-medium mb-2">Groups</h4>
                <div className="space-y-2">
                  {result.groups.map((group, index) => (
                    <div key={index} className="p-2 bg-muted rounded text-sm">
                      {Object.entries(group).map(([key, value]) => (
                        <div key={key} className="flex justify-between">
                          <span className="font-medium">{key}:</span>
                          <span>{value}</span>
                        </div>
                      ))}
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

export default TestRegex; 