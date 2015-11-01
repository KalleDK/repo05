using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ATM.Events;
using ATM.Models;
using ATM.tests.unit.Tools;
using ATM.Time;
using NUnit.Framework;

namespace ATM.tests.unit.EventHandlerUnitTest
{
    public class AtmEnterEventTests
    {
        protected static readonly DateTime FixedDateTimeNow = new DateTime(2015, 01, 28, 17, 49, 15, 500, DateTimeKind.Local);

        protected static readonly List<Plane> PlaneList = new List<Plane>
        {
            PlaneFactory.MakePlane("XX1234", 10, 10, 10, FixedDateTimeNow),
            PlaneFactory.MakePlane("YY9876", 20, 20, 20, FixedDateTimeNow),
            PlaneFactory.MakePlane("RR1234", 30, 30, 30, FixedDateTimeNow),
            PlaneFactory.MakePlane("TT9876", 40, 40, 40, FixedDateTimeNow),
        };

      
        protected static void Show(string title, object o)
        {
            Console.WriteLine("{0}\n===================", title);
            Console.WriteLine(ObjectDumper.ToString(o));
        }

        protected static void ShowForeach(string title, IEnumerable<object> os)
        {
            Console.WriteLine("{0}\n===================", title);
            foreach (var o in os)
            {
                Console.WriteLine(ObjectDumper.ToString(o));
            }
        }

        protected void EnterPlanes(IEnumerable<Plane> planes, bool print = false)
        {
            if (print)
            {
                Show("Planes Arriving", planes);
            }
            uut.CheckForEvent(planes);
        }
        

        protected void ShowActiveEvents()
        {
            ShowForeach("Active Events", uut.ActiveAtmEvents);
        }

        protected EnterEventHandler uut;

    }

    [TestFixture]
    internal class AtmEnterEventSimpleTests : AtmEnterEventTests
    {
        protected static List<string> TagList { get { return PlaneList.Select(p => p.Tag).ToList(); } }

        [SetUp]
        public void AtmEnterEventSimpleTestsSetup()
        {
            TimeProvidor.Set(FixedDateTimeNow);
            uut = new EnterEventHandler();

        }

        [TearDown]
        public void AtmEnterEventSimpleTestsTearDown()
        {
            TimeProvidor.Reset();
        }

        public static IEnumerable PlanesMatrix
        {
            get
            {
                for (var i = 0; i <= PlaneList.Count; ++i)
                {
                    yield return new TestCaseData(PlaneList.Take(i));
                }
            }
        }

        [Test, TestCaseSource(nameof(PlanesMatrix))]
        public void CheckForEvent_NewPlanes_CorrectNumberOfEvents(IEnumerable<Plane> planes )
        {
            var enumerable = planes as IList<Plane> ?? planes.ToList();

            EnterPlanes(enumerable, true);
            ShowActiveEvents();

            Assert.That(uut.ActiveAtmEvents.Count(), Is.EqualTo(enumerable.Count));
        }

        [Test]
        [TestCaseSource(nameof(PlanesMatrix))]
        public void CheckForEvent_NewPlanes_AllTagsAreCorrect(IEnumerable<Plane> planes )
        {
            var enumerable = planes as IList<Plane> ?? planes.ToList();

            EnterPlanes(enumerable, true);
            ShowActiveEvents();

            Assert.That(uut.ActiveAtmEvents.Select(p => p.Tags[0]), Is.EquivalentTo(enumerable.Select(p => p.Tag)));
        }
        
        [Test]
        public void CheckForEvent_NewPlanesMultiple_AllTagsAreCorrect()
        {
            EnterPlanes(PlaneList.Take(2), true);
            EnterPlanes(PlaneList.Skip(2).Take(2), true);
            ShowActiveEvents();

            Assert.That(uut.ActiveAtmEvents.Select(p => p.Tags[0]), Is.EquivalentTo(PlaneList.Select(p => p.Tag)));
        }

        [Test]
        public void CheckForEvent_NewPlanesMultiple_CorrectNumberOfEvents()
        {
            EnterPlanes(PlaneList.Take(2), true);
            EnterPlanes(PlaneList.Skip(2).Take(2), true);
            ShowActiveEvents();

            Assert.That(uut.ActiveAtmEvents.Count(), Is.EqualTo(PlaneList.Count));
        }

        [Test]
        public void CheckForEvent_SamePlanesArrives_CorrectNumberOfEvents()
        {
            EnterPlanes(PlaneList, true);
            EnterPlanes(PlaneList, true);
            ShowActiveEvents();

            Assert.That(uut.ActiveAtmEvents.Count(), Is.EqualTo(PlaneList.Count));
        }
        
    }
    
    [TestFixture]
    internal class AtmEnterEventTimedTests : AtmEnterEventTests
    {
        
        // Allowed Deviation
        protected static double Deviation = 0.01; // = 1%

        [SetUp]
        public void AtmEnterEventTimedTestsSetup()
        {
            uut = new EnterEventHandler();

            // We don't want to wait 10 sec each time ???
            EnterEventHandler.Timeout = 2000;
        }

        [Test]
        public void CheckForEvent_NewPlanesMultiple_FirstIsRemovedAfterTime()
        {

            var earlyarrivers = 2;

            Console.WriteLine("Correct Timout: {0}", EnterEventHandler.Timeout);

            var allowedDeviatedTime = (int)(EnterEventHandler.Timeout * (1 - Deviation));
            Console.WriteLine("Allowed Timout: {0}", allowedDeviatedTime);

            EnterPlanes(PlaneList.Take(earlyarrivers));

            Thread.Sleep(EnterEventHandler.Timeout / 2);

            EnterPlanes(PlaneList);

            Assert.That(() => uut.ActiveAtmEvents.Count(), Is.EqualTo(PlaneList.Count - earlyarrivers).After(allowedDeviatedTime));

        }

        [Test]
        public void CheckForEvent_NewPlanesMultiple_AllIsRemovedAfterTime()
        {

            const int earlyarrivers = 2;

            Console.WriteLine("Correct Timout: {0}", EnterEventHandler.Timeout);

            var allowedDeviatedTime = (int)(EnterEventHandler.Timeout * (1 + Deviation));
            Console.WriteLine("Allowed Timout: {0}", allowedDeviatedTime);

            var pollingInterval = EnterEventHandler.Timeout / 100;
            Console.WriteLine("Polling Interval: {0}", pollingInterval);

            EnterPlanes(PlaneList.Take(earlyarrivers));

            Thread.Sleep(EnterEventHandler.Timeout / 2);

            EnterPlanes(PlaneList);

            Assert.That(() => uut.ActiveAtmEvents, Is.Empty.After(allowedDeviatedTime, pollingInterval));

        }

        [Test]
        public void CheckForEvent_NewPlanes_IsRemovedAfterTime()
        {

            Console.WriteLine("Correct Timout: {0}", EnterEventHandler.Timeout);

            var allowedDeviatedTime = (int) (EnterEventHandler.Timeout*(1 + Deviation));
            Console.WriteLine("Allowed Timout: {0}", allowedDeviatedTime);

            var pollingInterval = EnterEventHandler.Timeout / 100;
            Console.WriteLine("Polling Interval: {0}", pollingInterval);

            EnterPlanes(PlaneList);

            Assert.That(() => uut.ActiveAtmEvents, Is.Empty.After(allowedDeviatedTime, pollingInterval));

        }

        [Test]
        public void CheckForEvent_NewPlanes_IsNotRemovedAfterTime()
        {

            Console.WriteLine("Correct Time To Keep: {0}", EnterEventHandler.Timeout);

            var allowedDeviatedTime = (int)(EnterEventHandler.Timeout * (1 - Deviation));
            Console.WriteLine("Allowed Time To Keep: {0}", allowedDeviatedTime);
       
            EnterPlanes(PlaneList);

            Assert.That(() => uut.ActiveAtmEvents.Count(), Is.EqualTo(PlaneList.Count).After(allowedDeviatedTime));

       
        }

        [Test, Timeout(50 * 1000)] // Timeout added incase while loop gets stuck
        public void CheckForEvent_NewPlanesArrivesAgainLate_NoNewEvent()
        {

            Console.WriteLine("Correct Time To Keep: {0}", EnterEventHandler.Timeout);

            var allowedDeviatedTime = (int)(EnterEventHandler.Timeout * (1 + Deviation));
            Console.WriteLine("Allowed Time To Keep: {0}", allowedDeviatedTime);

            var pollingInterval = EnterEventHandler.Timeout / 100;
            Console.WriteLine("Polling Interval: {0}", pollingInterval);

            EnterPlanes(PlaneList);

            // Stay here until all events are gone
            while (uut.ActiveAtmEvents.Any())
            { Thread.Sleep(pollingInterval); }
            

            // All planes is already in the airspace, no new events should arrive
            EnterPlanes(PlaneList);

            Assert.That(uut.ActiveAtmEvents, Is.Empty);

        }
    }
}
