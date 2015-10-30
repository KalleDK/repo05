using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Events
{
    public interface IEventController : IObservable<IEventController>
    {
        IEnumerable<AtmEventBase> ActiveAtmEvents { get; }
    }
}
