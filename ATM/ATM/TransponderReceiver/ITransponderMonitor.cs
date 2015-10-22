using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.TransponderReceiver
{
    interface ITransponderMonitor : IObservable<bool>
    {
        List<Plane> GetPlanes();
    }
}
