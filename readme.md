# EpiEasyEvents

Simple helper project to make implementing handlers for EpiServer events easier. Version 2 is targetting EpiServer 12 (.Net Core based)

# Installation
`dotnet add package Forte.EpiEasyEvents`

# How to use

First, add two calls in your Startup class
```cs
public void ConfigureServices(IServiceCollection services)
{
    // (...)
    services.AddEpiEasyEvents(this.GetType().Assembly); 
    // (...)
}
```
(You need to pass an assembly (or list of assemblies) that you want to be scanned for implementations of `IContentEventHandler`)

and
```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // (...)
    app.UseEpiEasyEvents();
    // (...)
}
```


Then create a class that implements any of the generic event handler interfaces in `Forte.EpiEasyEvents.EventHandlers` namespace. Each interface has a generic argument being the content type for the event.

Each interface derives from `IContentEventHandler<TContentType, TEventArgsType>` and has single method to implement: 
```cs
    void Handle(TContentType content, TEventArgsType eventArgs);
```


Example:

```cs
    // handler that will be fired when StandardPage was published
   public class ContentPublishedHandler : IPublishedContentHandler<StandardPage> 
    {
        public ContentPublishedHandler()
        {
        }
        
        public void Handle(NewsArticlePage content, SaveContentEventArgs eventArgs)
        {
            // ...
        }
    }
```

# Settings

In some cases, you may need to disable events handling. To disable events handling dynamically create new instance of DisabledEventsHandlerScope **(and remember to call Dispose() on it or wrap with using block)** from Forte.EpiEasyEvents namespace or set property IsHandlingDisabled from Forte.EpiEasyEvents.Configuration namespace to true.


```cs
using(new Forte.EpiEasyEvents.DisabledEventsHandlerScope()) {
    //...code here without events handling
}
```

