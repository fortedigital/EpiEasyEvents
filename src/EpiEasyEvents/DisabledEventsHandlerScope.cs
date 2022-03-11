using System;

namespace Forte.EpiEasyEvents
{
    public class DisabledEventsHandlerScope : IDisposable
    {
        private readonly bool _previousValue;

        public DisabledEventsHandlerScope()
        {
            _previousValue = EventsHandlerScopeConfiguration.IsHandlingDisabled;
            EventsHandlerScopeConfiguration.IsHandlingDisabled = true;
        }

        public void Dispose()
        {
            EventsHandlerScopeConfiguration.IsHandlingDisabled = _previousValue;
        }
    }
}
