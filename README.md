# dotnetpro-application-insights
This ready-to-use example demonstrates end-to-end tracing with Azure Application Insights. The frontend is created with TypeScript & React. The backend has two micro services and is built with .NET Core. Distributed tracing is enabled and works from end to end.

# How to run
Please create an instance of Azure Application Insights and add your InstrumentationKey here

https://github.com/MariuszKogut/dotnetpro-application-insights/blob/6a4023812bf9216254a8121e8467e345a1acc00a/frontend/src/config.ts#L3

here

https://github.com/MariuszKogut/dotnetpro-application-insights/blob/b806f38ac09d225cc7a90e65f797614f855875e6/backend/HS.CustomerApp.CustomerHost/appsettings.json#L11

and finally here

https://github.com/MariuszKogut/dotnetpro-application-insights/blob/b806f38ac09d225cc7a90e65f797614f855875e6/backend/HS.CustomerApp.IdHost/appsettings.json#L11

Now you can run the application:

```
cd backend
dotnet restore
dotnet build

cd frontend
npm install
npm run start:all
```
