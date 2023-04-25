using System.Collections.Generic;

namespace EpiEasyEvents.Tests.TestHandlers;

public abstract class BaseTestHandler
{
    public IList<EventDescriptor> RaisedEvents { get; } = new List<EventDescriptor>();
}
