using EPiServer.Core;
using EPiServer.DataAbstraction;
using Forte.EpiEasyEvents.EventHandlers;

namespace EpiEasyEvents.Tests.TestHandlers;

public class SecurityEventsHandler : BaseTestHandler, IContentSecuritySavedHandler<PageData>
{
    public void Handle(PageData content, ContentSecurityEventArg eventArgs)
    {
        RaisedEvents.Add(new(content, eventArgs));
    }
}
