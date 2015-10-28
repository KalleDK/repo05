using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATM.Events
{
    public class AtmEvent
    {
        public enum EventTypes
        {
            Enter,
            Left,
            Seperation,
        }

        public enum Category
        {
            Warning,
            Information,
        }

        public EventTypes EventType;
        public Category EventCatagory;
        public DateTime Timesstamp;
        public List<string> Tags = new List<string>();

    }

    public abstract class AtmEventBase
    {
        protected IEventController EventController;
        public string EventCatagory { get; set; }
        public DateTime TimeStamp { get; set; }


    }

    public class AtmEventTimed : AtmEventBase
    {
        public int Timeout;
     }

    public class AtmEventEnter : AtmEventTimed
    {
        public string Tag { get; set; }
    }


    public class AtmEventSeperation : AtmEventBase
    {
        public List<string> Tags { get; set; }

    }
}
