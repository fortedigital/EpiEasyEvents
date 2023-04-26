using System;
using EPiServer;
using EPiServer.Core;
using Forte.EpiEasyEvents;

namespace EpiEasyEvents.Tests.TestHandlers;

public class InvalidContentHandler : IContentEventHandler<PageData, ContentEventArgs>
{
    public void Handle(PageData content, ContentEventArgs eventArgs)
    {
        throw new InvalidOperationException("This handler should not be called");
    }
}
