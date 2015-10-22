using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.Events
{
    public class EnterEventHandler : IEventHandler
    {
        private readonly IEventController _eventController;
        private List<string> _tagsList;
        private static readonly int _timeout = 10;

        public EnterEventHandler(IEventController eventController)
        {
            _eventController = eventController;
            _tagsList = new List<string>();
        }
        public void CheckForEvent(List<Plane> activePlanes)
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
                _eventController.RaiseEvent(e,_timeout);
            }

            // Update list for next time
            _tagsList = activePlanes.Select(plane => plane.Tag).ToList();
        }
    }
}