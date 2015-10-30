using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.Events
{
    public class AtmEventEnter : AtmEventTimed
    {
        public override string ToString()
        {
            return "Enter";
        }
    }

    public class EnterEventHandler : TimedEventHandlerBase
    {
        private List<string> _tagsList;
        private const int Timeout = 10 * 1000;

        public EnterEventHandler(IEventController eventController) : base(eventController)
        {
            _tagsList = new List<string>();

        }
        public override void CheckForEvent(List<Plane> activePlanes)
        {
            // Find alle the planes that are new to out list
            foreach (var plane in activePlanes.Where(plane => !_tagsList.Contains(plane.Tag)))
            {
                var e = new AtmEventEnter
                {
                    Level = Levels.Information,
                    Tags = { plane.Tag },
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