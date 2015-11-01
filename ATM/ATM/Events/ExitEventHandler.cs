using System.Collections.Generic;
using System.Linq;
using ATM.Models;
using ATM.Time;

namespace ATM.Events
{
    public class AtmExitEvent : AtmEventTimed
    {
        public override string EventType => "Exit";
    }

    public class ExitEventHandler : TimedEventHandlerBase
    {
        private List<string> _knownPlaneTags;
        private const int Timeout = 10*1000;

        public ExitEventHandler()
        {
            _knownPlaneTags = new List<string>();
        }

        private static AtmExitEvent NewEvent(string tag)
        {
            return new AtmExitEvent
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

            // Find alle the planes that are only on our list
            foreach (var dismissedPlaneTag in _knownPlaneTags.Except(activePlaneTags))
            {
                // Raise event for all dismissed planes
                RaiseEvent(NewEvent(dismissedPlaneTag));
            }

            // Update list for next time (this also adds new planes that have arrived)
            _knownPlaneTags = activePlaneTags;
        }

    }
}