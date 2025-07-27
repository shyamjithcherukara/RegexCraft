import { useState, useEffect } from 'react';
import { Button } from './components/ui/button';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from './components/ui/card';
import { Tabs, TabsContent, TabsList, TabsTrigger } from './components/ui/tabs';
import { Moon, Sun, Zap, Search, Code, BarChart3, Library, Activity } from 'lucide-react';
import { useAnalytics } from './hooks/useAnalytics';
import TestRegex from './components/TestRegex';
import ExplainRegex from './components/ExplainRegex';
import GenerateCode from './components/GenerateCode';
import PerformanceTest from './components/PerformanceTest';
import PatternLibrary from './components/PatternLibrary';
import ApiHealth from './components/ApiHealth';

function App() {
  const [theme, setTheme] = useState<'light' | 'dark'>('dark');
  const [activeTab, setActiveTab] = useState('test');
  const { trackTabChange, trackPageView } = useAnalytics();

  useEffect(() => {
    const savedTheme = localStorage.getItem('theme') as 'light' | 'dark' | null;
    if (savedTheme) {
      setTheme(savedTheme);
    }
  }, []);

  useEffect(() => {
    const root = window.document.documentElement;
    root.classList.remove('light', 'dark');
    root.classList.add(theme);
    localStorage.setItem('theme', theme);
  }, [theme]);

  useEffect(() => {
    // Track initial page view
    trackPageView('/');
  }, [trackPageView]);

  const toggleTheme = () => {
    setTheme(theme === 'light' ? 'dark' : 'light');
  };

  const handleTabChange = (value: string) => {
    setActiveTab(value);
    trackTabChange(value);
  };

  const tabs = [
    { id: 'test', label: 'Test Regex', icon: Search },
    { id: 'explain', label: 'Explain Regex', icon: Zap },
    { id: 'generate', label: 'Generate Code', icon: Code },
    { id: 'performance', label: 'Performance', icon: BarChart3 },
    { id: 'library', label: 'Pattern Library', icon: Library },
    { id: 'health', label: 'API Health', icon: Activity },
  ];

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 via-white to-gray-100 dark:from-gray-900 dark:via-gray-800 dark:to-gray-900">
      {/* Header with glass effect */}
      <header className="sticky top-0 z-50 glass-effect border-b border-gray-200/50 dark:border-gray-700/50">
        <div className="container mx-auto px-4 py-4">
          <div className="flex items-center justify-between">
            <div className="flex items-center space-x-3">
              <div className="w-10 h-10 bg-gradient-to-r from-purple-600 via-pink-600 to-orange-500 rounded-lg flex items-center justify-center">
                <Zap className="w-6 h-6 text-white" />
              </div>
              <div>
                <h1 className="text-2xl font-bold bg-gradient-to-r from-purple-600 via-pink-600 to-orange-500 bg-clip-text text-transparent">
                  RegexCraft
                </h1>
                <p className="text-sm text-gray-600 dark:text-gray-400">Advanced Regex Testing Tool</p>
              </div>
            </div>
            
            <Button
              onClick={toggleTheme}
              variant="ghost"
              size="icon"
              className="rounded-full w-10 h-10 hover:bg-gray-100 dark:hover:bg-gray-800 transition-all duration-200"
            >
              {theme === 'light' ? (
                <Moon className="h-5 w-5" />
              ) : (
                <Sun className="h-5 w-5" />
              )}
            </Button>
          </div>
        </div>
      </header>

      {/* Main content */}
      <main className="container mx-auto px-4 py-8">
        <div className="max-w-6xl mx-auto">
          <Card className="card-hover glass-effect border-0 shadow-2xl">
            <CardHeader className="text-center pb-8">
              <CardTitle className="text-3xl font-bold bg-gradient-to-r from-gray-900 to-gray-700 dark:from-white dark:to-gray-300 bg-clip-text text-transparent">
                Regex Testing Suite
              </CardTitle>
              <CardDescription className="text-lg text-gray-600 dark:text-gray-400">
                Test, explain, and generate regex patterns with advanced features
              </CardDescription>
            </CardHeader>
            
            <CardContent className="px-6 pb-8">
              <Tabs value={activeTab} onValueChange={handleTabChange} className="w-full">
                {/* Refined Tab List */}
                <div className="flex justify-center mb-8">
                  <TabsList className="inline-flex h-14 items-center justify-center rounded-xl bg-gray-100/80 dark:bg-gray-800/80 p-1 shadow-lg border border-gray-200/50 dark:border-gray-700/50">
                    {tabs.map((tab) => {
                      const IconComponent = tab.icon;
                      return (
                        <TabsTrigger
                          key={tab.id}
                          value={tab.id}
                          className="inline-flex items-center justify-center whitespace-nowrap rounded-lg px-4 py-2 text-sm font-medium ring-offset-background transition-all focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 data-[state=active]:bg-white dark:data-[state=active]:bg-gray-900 data-[state=active]:text-purple-600 dark:data-[state=active]:text-purple-400 data-[state=active]:shadow-md hover:bg-gray-200/50 dark:hover:bg-gray-700/50 hover:text-purple-600 dark:hover:text-purple-400"
                        >
                          <IconComponent className="w-4 h-4 mr-2" />
                          <span className="hidden sm:inline">{tab.label}</span>
                        </TabsTrigger>
                      );
                    })}
                  </TabsList>
                </div>
                
                <div className="mt-8">
                  <TabsContent value="test" className="mt-0">
                    <TestRegex />
                  </TabsContent>
                  <TabsContent value="explain" className="mt-0">
                    <ExplainRegex />
                  </TabsContent>
                  <TabsContent value="generate" className="mt-0">
                    <GenerateCode />
                  </TabsContent>
                  <TabsContent value="performance" className="mt-0">
                    <PerformanceTest />
                  </TabsContent>
                  <TabsContent value="library" className="mt-0">
                    <PatternLibrary />
                  </TabsContent>
                  <TabsContent value="health" className="mt-0">
                    <ApiHealth />
                  </TabsContent>
                </div>
              </Tabs>
            </CardContent>
          </Card>
        </div>
      </main>
    </div>
  );
}

export default App;
