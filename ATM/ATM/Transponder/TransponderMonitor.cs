using System;
using System.Collections.Generic;
using ATM.Models;
using TransponderReceiver;
using ATM.Logging;

namespace ATM.Transponder
{
    public class TransponderMonitor : ITransponderMonitor
    {
        private readonly ITransponderReceiver _eventReceiver;
        private readonly ILogger _logger;
        private readonly ITransponderParser _transParser;
        private readonly List<IObserver<List<PlaneObservation>>> _observers; 

        public TransponderMonitor(ITransponderReceiver eventReceiver, ITransponderParser transParser)
        {
            _logger = new Logger(typeof (TransponderMonitor));
            _eventReceiver = eventReceiver; //TransponderReceiverFactory.CreateTransponderDataReceiver();
            _eventReceiver.TransponderDataReady += TransponderListener;
            _transParser = transParser; // new TransponderParser();
            _observers = new List<IObserver<List<PlaneObservation>>>();

        }

        public IDisposable Subscribe(IObserver<List<PlaneObservation>> observer)
        {
            if(!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private void SendMessage(List<PlaneObservation> message)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(message);
            }
        }

        private void TransponderListener(List<string> transponderData)
        {
            _logger.Debug("=== New transponder data ===");
            foreach (var entry in transponderData)
            {
                _logger.Debug(entry);
            }
            if(transponderData.Count > 0)
                SendMessage(_transParser.ParseRawData(transponderData));
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<List<PlaneObservation>>> _observers;
            private IObserver<List<PlaneObservation>> _observer;

            public Unsubscriber(List<IObserver<List<PlaneObservation>>> observers, IObserver<List<PlaneObservation>> observer)
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