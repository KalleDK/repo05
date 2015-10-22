using System;
using System.Collections.Generic;
using ATM.Models;
using TransponderReceiver;

namespace ATM.Transponder
{
    public class TransponderMonitor : ITransponderMonitor
    {
        private ITransponderReceiver _eventReceiver;

        public TransponderMonitor()
        {
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
            foreach (var entry in transponderData)
            {
                Console.WriteLine(entry);
            }
        }
    }
}