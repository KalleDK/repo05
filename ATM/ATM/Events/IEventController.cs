using System;
using System.Collections.Generic;

namespace ATM.Events
{
    public interface IEventController : IObservable<IEventController>
    {
        IEnumerable<AtmEventBase> ActiveAtmEvents { get; }
    }
}
