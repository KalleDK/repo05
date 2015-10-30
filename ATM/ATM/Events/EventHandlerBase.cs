using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.Events
{
    public abstract class EventHandlerBase : IEventHandler
    {
        private readonly IEventController _controller;

        private readonly object _atmeventLock = new object();
        private readonly List<AtmEventBase> _activAtmEvents;

        protected EventHandlerBase(IEventController controller)
        {
            _controller = controller;
            _activAtmEvents = new List<AtmEvent>();
        }

        protected void RaiseEvent(AtmEventBase e)
        {
            lock (_atmeventLock)
            {
                _activAtmEvents.Add(e);
            }
        }

        protected void RemoveEvent(AtmEventBase e)
        {
            lock (_atmeventLock)
            {
                _activAtmEvents.Remove(e);
            }
        }

        public abstract void CheckForEvent(List<Plane> activePlanes);

        public IEnumerable<AtmEventBase> ActiveAtmEvents
        {
            get
            {
                lock (_atmeventLock)
                {
                    return _activAtmEvents.ToList();
                }
            }
        }

        public void OnNext(List<Plane> value)
        {
            CheckForEvent(value);
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}