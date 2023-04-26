using EPiServer;
using EPiServer.Core;
using Forte.EpiEasyEvents.EventHandlers;

namespace EpiEasyEvents.Tests.TestHandlers;

public class PublishedHandler : BaseTestHandler, IContentPublishedHandler<PageData>
{
    public void Handle(PageData content, SaveContentEventArgs eventArgs)
    {
        RaisedEvents.Add(new(content, eventArgs));
    }
}
