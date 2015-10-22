using System.Collections.Generic;
using System.ComponentModel;
using ATM.Models;

namespace ATM.Events
{
    public abstract class EventHandlerBase : IEventHandler
    {
        private IEventController _controller;
        private readonly List<AtmEvent> _activAtmEvents;

        protected EventHandlerBase(IEventController controller)
        {
            _controller = controller;
            _activAtmEvents = new List<AtmEvent>();
        }

        protected void RaiseEvent(AtmEvent e)
        {
            _activAtmEvents.Add(e);
            _controller.RaiseAtmEvent(e);
        }

        protected void RemoveEvent(AtmEvent e)
        {
            _activAtmEvents.Remove(e);
        }

        public abstract void CheckForEvent(List<Plane> activePlanes);

        public IEnumerable<AtmEvent> ActiveAtmEvents => _activAtmEvents;
    }
}