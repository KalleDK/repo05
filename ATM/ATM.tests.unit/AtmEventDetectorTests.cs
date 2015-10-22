using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Events;
using ATM.Models;
using NSubstitute;
using NUnit.Framework;

namespace ATM.tests.unit
{
    [TestFixture]
    class AtmEventDetectorTests
    {
        private EnterEventHandler uut;
        private IEventController fakeEventController;
        [SetUp]
        public void AtmEventDetectorTestsSetup()
        {
            fakeEventController = Substitute.For<IEventController>();
            uut = new EnterEventHandler(fakeEventController);
        }

        [Test]
        public void CheckForEvent_NewPlane_EventRaised()
        {
            var testPlane = new Plane() {Tag = "XX1212"};
            var testList = new List<Plane> {testPlane};

            uut.CheckForEvent(testList);



            fakeEventController.Received().RaiseAtmEvent(Arg.Any<AtmEvent>());

        }
    }
}
