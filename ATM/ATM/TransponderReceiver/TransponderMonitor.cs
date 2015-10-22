using System;
using System.Collections.Generic;
using ATM.Models;

namespace ATM.TransponderReceiver
{
    public class TransponderMonitor : ITransponderMonitor
    {
        public IDisposable Subscribe(IObserver<bool> observer)
        {
            throw new NotImplementedException();
        }

        public List<Plane> GetPlanes()
        {
            throw new NotImplementedException();
        }
    }
}