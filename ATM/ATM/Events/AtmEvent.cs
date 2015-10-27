using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}
