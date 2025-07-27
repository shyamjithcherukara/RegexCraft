import React, { useState, useEffect } from 'react';
import { Button } from './ui/button';
import { Input } from './ui/input';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from './ui/card';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from './ui/select';
import { Library, Search, Loader2, Copy, AlertCircle } from 'lucide-react';
import { api } from '../lib/api';
import type { Pattern, PatternLibraryResponse } from '../lib/api';

// Mock data for testing
const mockPatterns: Pattern[] = [
  {
    id: '1',
    name: 'Email Validation',
    pattern: '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$',
    description: 'Validates email addresses',
    category: 'Validation',
    examples: ['user@example.com', 'test.email@domain.co.uk']
  },
  {
    id: '2',
    name: 'Phone Number',
    pattern: '^\\+?[1-9]\\d{1,14}$',
    description: 'Validates international phone numbers',
    category: 'Validation',
    examples: ['+1234567890', '1234567890']
  },
  {
    id: '3',
    name: 'URL Validation',
    pattern: '^https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)$',
    description: 'Validates URLs',
    category: 'Validation',
    examples: ['https://example.com', 'http://www.test.org/path']
  }
];

const PatternLibrary: React.FC = () => {
  const [query, setQuery] = useState('');
  const [category, setCategory] = useState<string | undefined>(undefined);
  const [categories, setCategories] = useState<string[]>([]);
  const [patterns, setPatterns] = useState<Pattern[]>(mockPatterns);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [useMockData, setUseMockData] = useState(true);
  const [categoriesLoading, setCategoriesLoading] = useState(true);

  useEffect(() => {
    loadInitialData();
  }, []);

  const loadInitialData = async () => {
    setLoading(true);
    setError(null);

    try {
      // Try to load from API
      const patternsResponse = await api.getPatterns();
      setPatterns(patternsResponse.patterns || []);
      setUseMockData(false);
      setError(null);
    } catch (err) {
      console.warn('API not available, using mock data');
      setPatterns(mockPatterns);
      setUseMockData(true);
      setError('Using demo data - API not connected');
    } finally {
      setLoading(false);
    }

    // Load categories from API
    await loadCategories();
  };

  const loadCategories = async () => {
    setCategoriesLoading(true);
    try {
      const categoriesResponse = await api.getPatternCategories();
      
      // Handle the API response structure
      let categoriesArray: string[] = [];
      if (typeof categoriesResponse === 'object' && categoriesResponse !== null) {
        if ('categories' in categoriesResponse && Array.isArray(categoriesResponse.categories)) {
          categoriesArray = categoriesResponse.categories;
        } else if (Array.isArray(categoriesResponse)) {
          categoriesArray = categoriesResponse;
        }
      }
      
      setCategories(categoriesArray);
    } catch (err) {
      console.warn('Failed to load categories from API, using defaults');
      setCategories(['Validation', 'Matching', 'Extraction']);
    } finally {
      setCategoriesLoading(false);
    }
  };

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    if (!query.trim() && !category) {
      // Reset to all patterns
      setPatterns(useMockData ? mockPatterns : patterns);
      return;
    }

    setLoading(true);
    
    if (useMockData) {
      // Filter mock data
      let filteredPatterns = mockPatterns;
      
      if (query.trim()) {
        const searchQuery = query.toLowerCase();
        filteredPatterns = filteredPatterns.filter(pattern => 
          pattern.name.toLowerCase().includes(searchQuery) ||
          pattern.description.toLowerCase().includes(searchQuery)
        );
      }
      
      if (category) {
        filteredPatterns = filteredPatterns.filter(pattern => 
          pattern.category === category
        );
      }
      
      setPatterns(filteredPatterns);
      setLoading(false);
    } else {
      // Use API search
      loadPatternsFromAPI();
    }
  };

  const loadPatternsFromAPI = async () => {
    try {
      let response: PatternLibraryResponse;
      
      if (query.trim()) {
        response = await api.searchPatterns(query.trim());
      } else if (category) {
        response = await api.getPatternsByCategory(category);
      } else {
        response = await api.getPatterns();
      }
      
      setPatterns(response.patterns || []);
    } catch (err) {
      setError('Failed to search patterns');
    } finally {
      setLoading(false);
    }
  };

  const handleCategoryChange = (value: string) => {
    setCategory(value === 'all' ? undefined : value);
    if (value !== 'all') {
      handleSearch(new Event('submit') as any);
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
            <Library className="h-5 w-5" />
            <span>Pattern Library</span>
          </CardTitle>
          <CardDescription>
            Browse and search through a collection of useful regex patterns
            {useMockData && (
              <span className="block text-sm text-orange-600 mt-1">
                ⚠️ Using demo data - API not connected
              </span>
            )}
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSearch} className="space-y-6">
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
              <div className="space-y-3">
                <label htmlFor="query" className="text-sm font-medium text-gray-700 dark:text-gray-300">
                  Search Patterns
                </label>
                <Input
                  id="query"
                  value={query}
                  onChange={(e) => setQuery(e.target.value)}
                  placeholder="Search patterns by name or description..."
                  className="w-full"
                />
              </div>
              <div className="space-y-3">
                <label htmlFor="category" className="text-sm font-medium text-gray-700 dark:text-gray-300">
                  Category
                </label>
                <Select value={category || 'all'} onValueChange={handleCategoryChange} disabled={categoriesLoading}>
                  <SelectTrigger className="w-full">
                    <SelectValue placeholder={categoriesLoading ? "Loading categories..." : "All Categories"} />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="all">All Categories</SelectItem>
                    {categories.map((cat) => (
                      <SelectItem key={cat} value={cat}>
                        {cat}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
            </div>
            <div className="flex justify-center">
              <Button type="submit" disabled={loading} className="bg-gradient-to-r from-purple-600 via-pink-600 to-orange-500 hover:from-purple-700 hover:via-pink-700 hover:to-orange-600 text-white font-medium px-8 py-2 rounded-lg transition-all duration-200 hover:shadow-lg hover:scale-105 active:scale-95">
                {loading ? (
                  <>
                    <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                    Searching...
                  </>
                ) : (
                  <>
                    <Search className="mr-2 h-4 w-4" />
                    Search Patterns
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
              <AlertCircle className="h-5 w-5" />
              <span className="font-medium">Error</span>
            </div>
            <p className="mt-2 text-sm">{error}</p>
            <Button 
              onClick={loadInitialData} 
              variant="outline" 
              size="sm" 
              className="mt-3"
            >
              Try Again
            </Button>
          </CardContent>
        </Card>
      )}

      {loading && (
        <Card>
          <CardContent className="pt-6">
            <div className="flex items-center justify-center space-x-2">
              <Loader2 className="h-5 w-5 animate-spin" />
              <span>Loading patterns...</span>
            </div>
          </CardContent>
        </Card>
      )}

      {!loading && patterns.length === 0 && !error && (
        <Card>
          <CardContent className="pt-6">
            <div className="text-center">
              <Library className="h-12 w-12 text-gray-400 mx-auto mb-2" />
              <p className="text-muted-foreground">No patterns found.</p>
              <p className="text-sm text-muted-foreground mt-1">
                Try adjusting your search criteria or browse all patterns.
              </p>
            </div>
          </CardContent>
        </Card>
      )}

      {!loading && patterns.length > 0 && (
        <div className="space-y-4">
          <div className="flex items-center justify-between">
            <h3 className="text-lg font-medium">Found {patterns.length} pattern(s)</h3>
          </div>
          {patterns.map((pattern, index) => (
            <Card key={pattern.id || `pattern-${index}`}>
              <CardHeader>
                <div className="flex items-start justify-between">
                  <div>
                    <CardTitle className="text-lg">{pattern.name}</CardTitle>
                    <CardDescription className="mt-2">
                      {pattern.description}
                    </CardDescription>
                  </div>
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => copyToClipboard(pattern.pattern)}
                    className="h-8"
                  >
                    <Copy className="h-4 w-4 mr-1" />
                    Copy
                  </Button>
                </div>
              </CardHeader>
              <CardContent className="space-y-4">
                <div>
                  <h4 className="font-medium mb-2">Pattern</h4>
                  <code className="block p-3 bg-muted rounded text-sm break-all">
                    {pattern.pattern}
                  </code>
                </div>

                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div>
                    <h4 className="font-medium mb-2">Category</h4>
                    <span className="inline-block px-2 py-1 bg-primary/10 text-primary rounded text-sm">
                      {pattern.category}
                    </span>
                  </div>

                  {pattern.examples && pattern.examples.length > 0 && (
                    <div>
                      <h4 className="font-medium mb-2">Examples</h4>
                      <div className="space-y-1">
                        {pattern.examples.map((example, exampleIndex) => (
                          <code key={`${pattern.id || `pattern-${index}`}-example-${exampleIndex}`} className="block text-xs bg-muted px-2 py-1 rounded">
                            {example}
                          </code>
                        ))}
                      </div>
                    </div>
                  )}
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      )}
    </div>
  );
};

export default PatternLibrary; 