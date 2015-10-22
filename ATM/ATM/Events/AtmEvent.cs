using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Events
{
    class AtmEvent
    {
        enum EventTypes
        {
            Enter,
            Left,
            Seperation,
        }

        enum Catagory
        {
            Warning,
            Information,
        }

        private EventTypes EventType;
        private Catagory EventCatagory;
        private DateTime _startTime;
        private List<string> Tags;

    }
}
