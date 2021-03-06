﻿using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Models;
using ATM.Time;

namespace ATM.Events
{
    public class AtmEventSeperation : AtmEventBase
    {
        public override string EventType => "Seperation";
    }

    class SeparationEventHandler : EventHandlerBase
    {
        private readonly List<ConflictingPlanes> _tagsList;

        public SeparationEventHandler()
        {
            _tagsList = new List<ConflictingPlanes>();

        }

        public override void CheckForEvent(IEnumerable<Plane> activePlanes)
        {
            //
            List<ConflictingPlanes> cList = SeparationPlanes((List<Plane>)activePlanes);

            foreach (var cPlanes in cList)
            {
                if (!_tagsList.Contains(cPlanes))
                {
                    var e = new AtmEventSeperation()
                    {
                        Level = Levels.Warning,
                        Tags = {cPlanes.Tag1, cPlanes.Tag2},
                        TimeStamp = TimeProvidor.Now
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
