# EpiEasyEvents

Simple helper to make implementing handlers for EpiServer events easier

# Installation
`Install-Package Forte.EpiEasyEvents`

# How to use

First, add new initializable module that will register all event handlers in EpiServer:
```cs
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class EventHandlersRegistrar : ContentEventHandlersModule
    {
        // provide all assemblies that contains event handlers
        public EventHandlersRegistrar():base(typeof(EventHandlersRegistrar).Assembly)
        {
        }
    }
```

Then create class that derives from `ContentEventHandler<T>`, where `T` is content type for which events you respond, overriding all methods for which you want to subscribe:

```cs
    public class StandardPageEvents : ContentEventHandler<StandardPage>
    {
        protected override void OnContentPublishing(StandardPage content)
        {
            // any logic here
        }
    }
```