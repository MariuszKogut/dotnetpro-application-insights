import { Context } from "react";
import { createContext, useContext } from "react";
import { ApplicationInsights, DistributedTracingModes } from "@microsoft/applicationinsights-web";
import { config } from "../../config";

interface ApplicationInsightsContextType {
  instance: ApplicationInsights;
}

const setupApplicationInsights = () => {
  const { instrumentationKey, roleName } = config.applicationInsights;

  const applicationInsights = new ApplicationInsights({
    config: {
      instrumentationKey,
      disableFetchTracking: false,
      enableDebugExceptions: true,
      enableCorsCorrelation: true,
      distributedTracingMode: DistributedTracingModes.AI
    }
  });
  applicationInsights.loadAppInsights();

  const telemetryInitializer = (envelope: any) => {
    if (envelope && envelope.tags) {
      envelope.tags["ai.cloud.role"] = roleName;
      envelope.tags["ai.cloud.roleInstance"] = window.location.origin;
    }
  };

  applicationInsights.addTelemetryInitializer(telemetryInitializer);
  applicationInsights.trackPageView();

  return applicationInsights;
}

export const ApplicationInsightsContext: Context<ApplicationInsightsContextType> = createContext<
  ApplicationInsightsContextType
  >({
  instance: setupApplicationInsights()
});

export const useApplicationInsightsContext = () =>
  useContext<ApplicationInsightsContextType>(ApplicationInsightsContext);
