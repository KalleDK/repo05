﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.AirSpace;
using ATM.Models;
using NUnit.Framework;
using ATM.Transponder;
using NSubstitute;
using TransponderReceiver;

namespace ATM.tests.unit.TransponderUnitTest
{
    [TestFixture]
    class TransponderMonitorUnitTests
    {
        private TransponderMonitor _uut;
        private ITransponderReceiver _fakeTransReceiver;
        private ITransponderParser _fakeTransParser;
        private IAirSpaceMonitor _fakeAirSpaceMonitor;
        private const string CorrectFormatData = "ATR423;39045;12932;21000;20151006213456789";

        [SetUp]
        public void Setup()
        {
            _fakeTransReceiver = Substitute.For<ITransponderReceiver>();
            _fakeTransParser = Substitute.For<ITransponderParser>();
            _fakeAirSpaceMonitor = Substitute.For<IAirSpaceMonitor>();
            _uut = new TransponderMonitor(_fakeTransReceiver, _fakeTransParser, _fakeAirSpaceMonitor);
        }

        [Test]
        public void EventHandler_EventIsRaised_HandlerCalled()
        {
            _fakeTransReceiver.TransponderDataReady += Raise.Event<TransponderDataReadyHandler>(new List<string>() {CorrectFormatData});
            _fakeTransParser.Received().ParseRawData(Arg.Any<List<string>>());
        }

        [Test]
        public void Subscribe_CalledWithObserver_ReturnsIDisposable()
        {
            IObserver<List<Plane>> fakeObserver = Substitute.For<IObserver<List<Plane>>>();
            var unsubcriber = _uut.Subscribe(fakeObserver);
            Assert.That(unsubcriber, Is.InstanceOf<System.IDisposable>());
        }

        [Test]
        public void SubcriberGetCalled_EventRaised_SubscriberOnNextCalled()
        {
            IObserver<List<Plane>> fakeObserver = Substitute.For<IObserver<List<Plane>>>();
            var unsubcriber = _uut.Subscribe(fakeObserver);
            _fakeTransReceiver.TransponderDataReady += Raise.Event<TransponderDataReadyHandler>(new List<string>() { CorrectFormatData });

            fakeObserver.Received().OnNext(Arg.Any<List<Plane>>());
        }

        [Test]
        public void SubcriberUnsubscribes_OnNext_IsNotCalled()
        {
            IObserver<List<Plane>> fakeObserver = Substitute.For<IObserver<List<Plane>>>();

            var unsubcriber = _uut.Subscribe(fakeObserver);
            unsubcriber.Dispose();

            _fakeTransReceiver.TransponderDataReady += Raise.Event<TransponderDataReadyHandler>(new List<string>() { CorrectFormatData });

            fakeObserver.DidNotReceive().OnNext(Arg.Any<List<Plane>>());
        }
    }
}
