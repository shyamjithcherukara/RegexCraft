declare global {
  interface Window {
    gtag: (...args: any[]) => void;
  }
}

export const useAnalytics = () => {
  const trackPageView = (page: string) => {
    if (typeof window !== 'undefined' && window.gtag) {
      window.gtag('config', 'G-3VHWQPK0B7', {
        page_path: page,
      });
    }
  };

  const trackEvent = (action: string, category: string, label?: string, value?: number) => {
    if (typeof window !== 'undefined' && window.gtag) {
      window.gtag('event', action, {
        event_category: category,
        event_label: label,
        value: value,
      });
    }
  };

  const trackTabChange = (tabName: string) => {
    trackEvent('tab_change', 'navigation', tabName);
  };

  const trackFormSubmission = (formName: string) => {
    trackEvent('form_submit', 'engagement', formName);
  };

  const trackApiCall = (endpoint: string, success: boolean) => {
    trackEvent('api_call', 'performance', endpoint, success ? 1 : 0);
  };

  const trackError = (errorType: string, _errorMessage: string) => {
    trackEvent('error', 'errors', errorType, 1);
  };

  return {
    trackPageView,
    trackEvent,
    trackTabChange,
    trackFormSubmission,
    trackApiCall,
    trackError,
  };
}; 