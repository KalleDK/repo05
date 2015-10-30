using System;
using System.Collections.Generic;
using ATM.AirSpace;
using ATM.Models;
using TransponderReceiver;
using ATM.Logging;

namespace ATM.Transponder
{
    public class TransponderMonitor : ITransponderMonitor
    {
        private readonly IAirSpaceMonitor _airSpaceMonitor;
        private readonly ITransponderReceiver _eventReceiver;
        private readonly ILogger _logger;
        private readonly ITransponderParser _transParser;
        private readonly List<IObserver<List<Plane>>> _observers; 

        public TransponderMonitor(ITransponderReceiver eventReceiver, ITransponderParser transParser, IAirSpaceMonitor airSpaceMonitor)
        {
            _logger = new Logger(typeof (TransponderMonitor));
            _eventReceiver = eventReceiver; //TransponderReceiverFactory.CreateTransponderDataReceiver();
            _eventReceiver.TransponderDataReady += TransponderListener;
            _transParser = transParser; // new TransponderParser();
            _airSpaceMonitor = airSpaceMonitor;
            _observers = new List<IObserver<List<Plane>>>();

        }

        public IDisposable Subscribe(IObserver<List<Plane>> observer)
        {
            if(!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private void SendMessage(List<Plane> message)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(message);
            }
        }

        private void TransponderListener(List<string> transponderData)
        {
            _logger.Debug("=== New transponder data ===");

            if (transponderData.Count > 0)
            {
                var observations = _transParser.ParseRawData(transponderData);
                var planeTracks = _airSpaceMonitor.CheckAirSpace(observations);
                SendMessage(planeTracks);
            }

        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<List<Plane>>> _observers;
            private IObserver<List<Plane>> _observer;

            public Unsubscriber(List<IObserver<List<Plane>>> observers, IObserver<List<Plane>> observer)
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