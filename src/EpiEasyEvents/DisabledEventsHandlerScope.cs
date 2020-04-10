using System;

namespace Forte.EpiEasyEvents
{
    public class DisabledEventsHandlerScope : IDisposable
    {
        private readonly bool _previousValue;

        public DisabledEventsHandlerScope()
        {
            _previousValue = Configuration.IsHandlingDisabled;
            Configuration.IsHandlingDisabled = true;
        }

        public void Dispose()
        {
            Configuration.IsHandlingDisabled = _previousValue;
        }
    }
}
