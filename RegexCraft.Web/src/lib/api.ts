const API_BASE_URL = 'https://regexcraftapi.cleanstack.dev'; // Change this to your actual API URL

export interface TestRegexRequest {
  pattern: string;
  testString: string;
}

export interface TestRegexResponse {
  isMatch: boolean;
  matches: string[];
  groups: Record<string, string>[];
  error?: string;
}

export interface ExplainRegexRequest {
  pattern: string;
}

export interface ExplainRegexResponse {
  pattern: string;
  summary: string;
  parts: Array<{
    component: string;
    explanation: string;
    example?: string;
  }>;
}

export interface GenerateCodeRequest {
  pattern: string;
  sampleText?: string;
  language?: string;
}

export interface CodeSnippet {
  language: string;
  code: string;
  description: string;
}

export interface GenerateCodeResponse {
  snippets: CodeSnippet[];
}

export interface PerformanceTestRequest {
  pattern: string;
  testString: string;
  iterations: number;
}

export interface PerformanceTestResponse {
  pattern: string;
  testString: string;
  iterations: number;
  singleExecutionTimeMs: number;
  averageExecutionTimeMs: number;
  operationsPerSecond: number;
  isMatch: boolean;
  performanceRating: string;
}

export interface Pattern {
  id: string;
  name: string;
  pattern: string;
  description: string;
  category: string;
  examples: string[];
}

export interface PatternLibraryResponse {
  patterns: Pattern[];
}

export interface HealthCheckResponse {
  status: string;
  timestamp: string;
  version: string;
}

async function apiCall<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    headers: {
      'Content-Type': 'application/json',
      ...options.headers,
    },
    ...options,
  });

  if (!response.ok) {
    const error = await response.json().catch(() => ({ detail: 'Network error' }));
    throw new Error(error.detail || `HTTP ${response.status}`);
  }

  return response.json();
}

export const api = {
  testRegex: (data: TestRegexRequest): Promise<TestRegexResponse> => 
    apiCall<TestRegexResponse>('/api/test-regex', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    }),

  explainRegex: (pattern: string): Promise<ExplainRegexResponse> => 
    apiCall<ExplainRegexResponse>('/api/explain-regex', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ pattern })
    }),

  generateCode: (data: GenerateCodeRequest): Promise<GenerateCodeResponse> => 
    apiCall<GenerateCodeResponse>('/api/regex/generate-code', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    }),

  performanceTest: (data: PerformanceTestRequest): Promise<PerformanceTestResponse> => 
    apiCall<PerformanceTestResponse>('/api/regex/performance', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    }),

  getPatterns: (): Promise<PatternLibraryResponse> => 
    apiCall<PatternLibraryResponse>('/api/patterns'),

  searchPatterns: (query: string): Promise<PatternLibraryResponse> => 
    apiCall<PatternLibraryResponse>(`/api/patterns/search?q=${encodeURIComponent(query)}`),

  getPatternsByCategory: (category: string): Promise<PatternLibraryResponse> => 
    apiCall<PatternLibraryResponse>(`/api/patterns/category/${encodeURIComponent(category)}`),

  getPatternCategories: (): Promise<string[]> => 
    apiCall<string[]>('/api/patterns/categories'),

  healthCheck: (): Promise<HealthCheckResponse> => 
    apiCall<HealthCheckResponse>('/api/health'),
}; 