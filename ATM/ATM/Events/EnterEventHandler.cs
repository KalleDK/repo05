using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;
using ATM.Time;

namespace ATM.Events
{
    public class AtmEventEnter : AtmEventTimed
    {
        public override string EventType => "Enter";

    }

    public class EnterEventHandler : TimedEventHandlerBase
    {
        private List<string> _knownPlaneTags;
        public static int Timeout = 10 * 1000;

        public EnterEventHandler()
        {
            _knownPlaneTags = new List<string>();

        }

        private static AtmEventEnter NewEvent(string tag)
        {
            return new AtmEventEnter
            {
                Level = Levels.Information,
                Tags = new List<string> { tag },
                TimeStamp = TimeProvidor.Now,
                Timeout = Timeout,
            };
        }

        public override void CheckForEvent(IEnumerable<Plane> activePlanes)
        {
            // Select all active tags
            var activePlaneTags = activePlanes.Select(p => p.Tag).ToList();

            // Find alle the planes that are new to our list
            foreach (var newPlaneTag in activePlaneTags.Except(_knownPlaneTags))
            {
                // Raise event for all new planes
                RaiseEvent(NewEvent(newPlaneTag));
            }

            // Update list for next time (this alsp removes planes that have left the airspace)
            _knownPlaneTags = activePlaneTags;
        }
    }
}