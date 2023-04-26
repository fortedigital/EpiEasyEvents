using EPiServer;
using EPiServer.Core;
using Forte.EpiEasyEvents.EventHandlers;

namespace EpiEasyEvents.Tests.TestHandlers;

public class ContentDataHandler : BaseTestHandler, IContentPublishedHandler<IContentData>
{
    public void Handle(IContentData content, SaveContentEventArgs eventArgs)
    {
        RaisedEvents.Add(new(content, eventArgs));
    }
}
