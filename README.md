ElmahR.Api.Nancy
================

Extended [**ElmahR**](https://bitbucket.org/wasp/elmahr/wiki/Home) dashboard using a [**Nancy**](http://nancyfx.org/) endpoint to receive errors from any application capable of sendind HTTP POST JSON data.

##Install Steps##

Install Nuget package to web project
```csharp
 Install-Package ElmahR.Api.Nancy
```
Add following code to route registrations
```csharp
 routes.IgnoreRoute("nancyapi/{*pathInfo}");
```
Delete nancy hadler declarations under both system.web <httpHandlers> and system.webserver <handlers> sections on **web.config** file.

```xml
 <add verb="*" type="Nancy.Hosting.Aspnet.NancyHttpRequestHandler" path="*" />
```

##API Client Demo##
```csharp
  /*Creates sample data*/
  ErrorListSubmitRequest errorListSubmitRequest = SampleRequestFixture.CreateErrorListSubmitRequest();

  IElmahRApiClient apiClient = new ElmahRNancyClient("http://localhost:60944/");
  ErrorSubmitResponse response = apiClient.Post(errorListSubmitRequest);

  Console.WriteLine("Status : {0}, Message : {1}", response.Status, response.StatusMessage);
```
