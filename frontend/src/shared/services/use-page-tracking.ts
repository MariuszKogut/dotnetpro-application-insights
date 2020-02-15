import { useApplicationInsightsContext } from "./analytics";

export const usePageTracking = () => {
  const {instance} = useApplicationInsightsContext();
  instance.trackPageView();
}
