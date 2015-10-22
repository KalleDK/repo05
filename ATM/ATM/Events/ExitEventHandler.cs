﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;

namespace ATM.Events
{
    public class ExitEventHandler : IEventHandler
    {
        private readonly IEventController _eventController;
        private List<string> _tagsList;
        private static readonly int _timeout = 10;

        public ExitEventHandler(IEventController eventController)
        {
            _eventController = eventController;
            _tagsList = new List<string>();
        }
        public void CheckForEvent(List<Plane> activePlanes)
        {
            // Remove all planes from the saved list
            foreach (var plane in activePlanes)
            {
                _tagsList.Remove(plane.Tag);
            }

            // Planes left must have left the airspace
            foreach (var tag in _tagsList)
            {
                var e = new AtmEvent
                {
                    EventCatagory = AtmEvent.Category.Information,
                    EventType = AtmEvent.EventTypes.Enter,
                    Tags = {tag},
                    Timesstamp = DateTime.Now,
                };
                _eventController.RaiseEvent(e, _timeout);
            }

            // Update list for next time
            _tagsList = activePlanes.Select(plane => plane.Tag).ToList();
        }
    }
}