using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Events
{
    public interface IEventHandler : IObserver<List<Plane>>, IObservable<EventChange>
    {
        IEnumerable<AtmEventBase> ActiveAtmEvents { get; } 
    }
}
