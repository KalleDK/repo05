using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.Events
{
    public enum EventChange
    {
        Added,
        Removed
    }
    public abstract class EventHandlerBase : IEventHandler
    {
        private readonly object _atmeventLock = new object();
        private readonly List<AtmEventBase> _activAtmEvents;
        private readonly List<IObserver<EventChange>> _observers;

        protected EventHandlerBase()
        {
            _activAtmEvents = new List<AtmEventBase>();
            _observers = new List<IObserver<EventChange>>();
        }

        protected void RaiseEvent(AtmEventBase e)
        {
            lock (_atmeventLock)
            {
                _activAtmEvents.Add(e);
            }
            SendMessage(EventChange.Added);
        }

        protected void RemoveEvent(AtmEventBase e)
        {
            lock (_atmeventLock)
            {
                _activAtmEvents.Remove(e);
            }
            SendMessage(EventChange.Removed);
        }

        public abstract void CheckForEvent(IEnumerable<Plane> activePlanes);

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

        public IDisposable Subscribe(IObserver<EventChange> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private void SendMessage(EventChange message)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(message);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<EventChange>> _observers;
            private readonly IObserver<EventChange> _observer;

            public Unsubscriber(List<IObserver<EventChange>> observers, IObserver<EventChange> observer)
            {
                _observers = observers;
                _observer = observer;
            }
            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}