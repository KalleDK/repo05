using System.Collections.Generic;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Events
{
    public abstract class TimedEventHandlerBase : EventHandlerBase
    {
        
        protected void RaiseEvent(AtmEventTimed e)
        {
            base.RaiseEvent(e);
            WaitAndRemoveEvent(e);
        }

        // Start new thread, wait and remove.
        private async void WaitAndRemoveEvent(AtmEventTimed e)
        {
            await Task.Delay(e.Timeout);
            RemoveEvent(e);
        }

        protected TimedEventHandlerBase(IEventController controller) : base(controller)
        {
        }
    }
}