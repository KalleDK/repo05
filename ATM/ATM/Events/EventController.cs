using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.Events
{
    public class EventController : IEventController
    {
        private readonly List<IEventHandler> _eventHandlers;        

        public EventController(List<IEventHandler> eventHandlers)
        {
            _eventHandlers = eventHandlers;
        }

        public IEnumerable<AtmEventBase> ActiveAtmEvents
        {
            get { return _eventHandlers.SelectMany(p => p.ActiveAtmEvents); }
        }

        public void RaiseAtmEvent(AtmEventBase e)
        {
            throw new System.NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<IEventController> observer)
        {
            throw new NotImplementedException();
        }
    }
}