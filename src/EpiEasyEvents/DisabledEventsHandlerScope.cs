using System;

namespace Forte.EpiEasyEvents
{
    public class DisabledEventsHandlerScope : IDisposable
    {
        private readonly bool _previousValue;

        public DisabledEventsHandlerScope()
        {
            _previousValue = DisabledEventsHandlerScopeConfiguration.IsHandlingDisabled;
            DisabledEventsHandlerScopeConfiguration.IsHandlingDisabled = true;
        }

        public void Dispose()
        {
            DisabledEventsHandlerScopeConfiguration.IsHandlingDisabled = _previousValue;
        }
    }
}
