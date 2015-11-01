using System;
using System.Collections.Generic;

namespace ATM.Events
{
    public enum Levels
    {
        Warning,
        Information,
    }

    public abstract class AtmEventBase
    {
        protected AtmEventBase()
        {
            Tags = new List<string>();
        }

        public Levels Level { get; set; }
        public abstract string EventType { get; }
        public DateTime TimeStamp { get; set; }
        public List<string> Tags { get; set; }
        
    }
    
}
