using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Events
{
    class SeparationEventHandler : EventHandlerBase
    {
        private List<ConflictingPlanes> _tagsList;

        public SeparationEventHandler(IEventController eventController) : base(eventController)
        {
            _tagsList = new List<ConflictingPlanes>();

        }

        public override void CheckForEvent(List<Plane> activePlanes)
        {
            //
            List<ConflictingPlanes> cList = SeparationPlanes(activePlanes);

            foreach (var cPlanes in cList)
            {
                if (!_tagsList.Contains(cPlanes))
                {
                    var e = new AtmEvent()
                    {
                        EventCatagory = AtmEvent.Category.Warning,
                        EventType = AtmEvent.EventTypes.Seperation,
                        Tags = {cPlanes.Tag1, cPlanes.Tag2},
                        Timesstamp = DateTime.Now
                    };
                    RaiseEvent(e);
                    _tagsList.Add(cPlanes);
                } 
            }

            foreach (var cPlane in _tagsList.Where(cPlane => !cList.Contains(cPlane)))
            {
                var cTags = new List<string>() {cPlane.Tag1,cPlane.Tag2};
               
                foreach (var Event in ActiveAtmEvents)
                {
                    if (Event.Tags == cTags)
                    {
                        _tagsList.Remove(cPlane);
                        RemoveEvent(Event);
                    }
                }
            }
        }

        public List<ConflictingPlanes> SeparationPlanes(List<Plane> activePlanes)
        {
            // A List of every conflicting planes
            List<ConflictingPlanes> cList = new List<ConflictingPlanes>();

            // Copy of activePlanes
            List<Plane> activePlanesCopy = new List<Plane>(activePlanes);

            // Find every conflicting plane
            foreach (var plane1 in activePlanesCopy)
            {
                Coordinate coordinate1 = plane1.Positions.First().Coordinate;
                activePlanes.Remove(activePlanes.First());

                foreach (var plane2 in activePlanesCopy)
                {
                    Coordinate coordinate2 = plane2.Positions.First().Coordinate;

                    int diffX = Math.Abs(coordinate1.X - coordinate2.X);
                    int diffY = Math.Abs(coordinate1.Y - coordinate2.Y);
                    int diffZ = Math.Abs(coordinate1.Z - coordinate2.Z);


                    if (diffX < 5000 && diffY < 5000 && diffZ < 300)
                    {
                        ConflictingPlanes cPlanes = new ConflictingPlanes();
                        cPlanes.Tag1 = plane1.Tag;
                        cPlanes.Tag2 = plane2.Tag;

                        cList.Add(cPlanes);
                    }
                }
            }

            return cList;
        }


        public class ConflictingPlanes
        {
            public string Tag1 { get; set; }
            public string Tag2 { get; set; }
        }
    }
}
