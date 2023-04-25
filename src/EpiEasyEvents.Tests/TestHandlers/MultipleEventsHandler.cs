using EPiServer;
using EPiServer.Core;
using Forte.EpiEasyEvents.EventHandlers;

namespace EpiEasyEvents.Tests.TestHandlers;

public class MultipleEventsHandler : BaseTestHandler, IContentCreatedHandler<PageData>, IContentDeletedHandler<PageData>
{
    public void Handle(PageData content, ContentEventArgs eventArgs)
    {
        RaisedEvents.Add(new(content, eventArgs));
    }

    public void Handle(PageData content, MoveContentEventArgs eventArgs)
    {
        RaisedEvents.Add(new(content, eventArgs));
    }
}
