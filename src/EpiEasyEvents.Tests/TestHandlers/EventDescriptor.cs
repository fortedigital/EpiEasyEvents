using System;
using EPiServer.Core;

namespace EpiEasyEvents.Tests.TestHandlers;

public record EventDescriptor(IContent Content, EventArgs EventArgs);
