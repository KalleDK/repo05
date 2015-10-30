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
        public Levels Level;
        public string EventType { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<string> Tags { get; set; }
        public abstract override string ToString();
        
    }
    
}
