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

        public TransponderMonitor()
        {
            _logger = new Logger(typeof (TransponderMonitor));
            _eventReceiver = TransponderReceiver.TransponderReceiverFactory.CreateTransponderDataReceiver();
            _eventReceiver.TransponderDataReady += TransponderListener;

        }

        public IDisposable Subscribe(IObserver<bool> observer)
        {
            throw new NotImplementedException();
        }

        public List<Plane> GetPlanes()
        {
            throw new NotImplementedException();
        }

        private void TransponderListener(List<string> transponderData)
        {
            _logger.Debug("=== New transponder data ===");
            foreach (var entry in transponderData)
            {
                _logger.Debug(entry);
            }
        }
    }
}