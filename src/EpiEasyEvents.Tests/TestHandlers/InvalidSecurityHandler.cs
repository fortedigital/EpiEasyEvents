using System;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using Forte.EpiEasyEvents;

namespace EpiEasyEvents.Tests.TestHandlers;

public class InvalidSecurityHandler : IContentSecurityEventHandler<PageData, ContentSecurityEventArg>
{
    public void Handle(PageData content, ContentSecurityEventArg eventArgs)
    {
        throw new InvalidOperationException("This handler should not be called");
    }
}
