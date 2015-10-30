using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.Events
{
    public class AtmEventExit : AtmEventTimed
    {
        public override string ToString()
        {
            return "Enter";
        }
    }

    public class ExitEventHandler : TimedEventHandlerBase
    {
        private List<string> _tagsList;
        private const int Timeout = 10*1000;

        public ExitEventHandler(IEventController eventController) : base(eventController)
        {
            _tagsList = new List<string>();
        }

        public override void CheckForEvent(List<Plane> activePlanes)
        {
            // Remove all planes from the saved list
            foreach (var plane in activePlanes)
            {
                _tagsList.Remove(plane.Tag);
            }

            // Planes left must have left the airspace
            foreach (var tag in _tagsList)
            {
                var e = new AtmEventExit()
                {
                    Level = Levels.Information,
                    Tags = {tag},
                    TimeStamp = DateTime.Now,
                    Timeout = Timeout,
                };
                RaiseEvent(e);
            }
            
            // Update list for next time
            _tagsList = activePlanes.Select(plane => plane.Tag).ToList();
        }

    }
}