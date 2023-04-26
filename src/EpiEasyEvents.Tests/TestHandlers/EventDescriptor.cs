using System;
using EPiServer.Core;

namespace EpiEasyEvents.Tests.TestHandlers;

public record EventDescriptor(IContentData Content, EventArgs EventArgs);
