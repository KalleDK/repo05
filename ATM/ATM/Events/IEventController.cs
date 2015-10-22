using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Events
{
    public interface IEventController
    {
        void CheckForEvents(List<Plane> activePlanes);
        void RaiseEvent(AtmEvent raisedEvent, int timeout = -1);
    }
}
