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
        private List<IObserver<List<Plane>>> _observers; 

        public TransponderMonitor()
        {
            _logger = new Logger(typeof (TransponderMonitor));
            _eventReceiver = TransponderReceiver.TransponderReceiverFactory.CreateTransponderDataReceiver();
            _eventReceiver.TransponderDataReady += TransponderListener;
            _transParser = new TransponderParser();
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
            foreach (var entry in transponderData)
            {
                _logger.Debug(entry);
            }
            SendMessage(_transParser.ParseRawData(transponderData));
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