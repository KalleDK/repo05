using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.Events
{
    public class EnterEventHandler : EventHandlerBase
    {
        private List<string> _tagsList;
        private static readonly int _timeout = 10;
        private static System.Timers.Timer removeTimer;

        public EnterEventHandler(IEventController eventController) : base(eventController)
        {
            _tagsList = new List<string>();

        }
        public override void CheckForEvent(List<Plane> activePlanes)
        {
            // Find alle the planes that are new to out list
            foreach (var plane in activePlanes.Where(plane => !_tagsList.Contains(plane.Tag)))
            {
                var e = new AtmEvent
                {
                    EventCatagory = AtmEvent.Category.Information,
                    EventType = AtmEvent.EventTypes.Enter,
                    Tags = {plane.Tag},
                    Timesstamp = DateTime.Now,
                };
                RaiseEvent(e);
            }

            // Update list for next time
            _tagsList = activePlanes.Select(plane => plane.Tag).ToList();
        }
    }
}