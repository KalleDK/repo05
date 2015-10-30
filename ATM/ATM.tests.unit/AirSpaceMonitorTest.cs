using System.Collections.Generic;
using ATM.AirSpace;
using ATM.Models;
using NSubstitute;
using NUnit.Framework;


namespace ATM.tests.unit
{
    [TestFixture]
    public class AirSpaceMonitorTest
    {
        private AirSpaceMonitor uut;
        private PlaneObservation Plane_Out;
        private PlaneObservation Plane_In;
        
        [SetUp]
        public void Setup()
        {
            uut = new AirSpaceMonitor();

            Plane_Out = new PlaneObservation() {Tag = "Plane_Out", ObservedPosition = new Position() {Coordinate = new Coordinate(9999,10000,500)} }; 

            Plane_In = new PlaneObservation() {Tag = "Plane_In", ObservedPosition = new Position() {Coordinate = new Coordinate(10000,10000,500)} };
        }

        [Test]
        public void AirSpaceMonitor_CheckAirspace_ListIsNotEmpty()
        {
            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_In);

            Plane testPlane = new Plane() { Tag = Plane_In.Tag };

            List<Plane> testListPlanes = uut.CheckAirSpace(testListObservations);

            CollectionAssert.IsNotEmpty(testListPlanes);
        }

        [Test]
        public void AirSpaceMonitor_CheckAirspace_PlaneIsInList()
        {
            //Plane_out x coordinate is set in setup to be out of bound  

            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_Out);

            testListObservations.Add(Plane_In);

            List<Plane> testListPlanes = uut.CheckAirSpace(testListObservations);

            Plane testPlane = testListPlanes[0];

            CollectionAssert.Contains(testListPlanes, testPlane);
        }

        [Test]
        public void AirSpaceMonitor_CheckAirspace_XCoordinateIsOutOfMinBound()
        {
            //Plane_out x coordinate is set in setup to be out of bound  

            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_Out);

            List<Plane> testListPlanes = uut.CheckAirSpace(testListObservations);

            CollectionAssert.IsEmpty(testListPlanes);
        }

        [Test]
        public void AirSpaceMonitor_CheckAirSpace_XCoordinateIsOutOfMaxBound()
        {
            Plane_Out.ObservedPosition.Coordinate.X = 90001;

            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_Out);

            List<Plane> testListPlanes = uut.CheckAirSpace(testListObservations);

            CollectionAssert.IsEmpty(testListPlanes);
        }

        [Test]
        public void AirSpaceMonitor_CheckAirspace_YCoordinateIsOutOfMinBound()
        {

            Plane_Out.ObservedPosition.Coordinate.X = 10000;

            Plane_Out.ObservedPosition.Coordinate.Y = 9999;


            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_Out);


            List<Plane> testListPlanes = uut.CheckAirSpace(testListObservations);

            CollectionAssert.IsEmpty(testListPlanes);
        }

        [Test]
        public void AirSpaceMonitor_CheckAirSpace_YCoordinateIsOutOfMaxBound()
        {
            Plane_Out.ObservedPosition.Coordinate.Y = 90001;

            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_Out);


            List<Plane> testListPlanes = uut.CheckAirSpace(testListObservations);

            CollectionAssert.IsEmpty(testListPlanes);
        }

        [Test]
        public void AirSpace_Monitor_CheckAirSpace_ZCoordinateOutOfMinBound()
        {
            Plane_Out.ObservedPosition.Coordinate.Y = 10000;
            Plane_Out.ObservedPosition.Coordinate.Z = 499;


            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_Out);


            List<Plane> testListPlanes = uut.CheckAirSpace(testListObservations);

            CollectionAssert.IsEmpty(testListPlanes);
        }

        [Test]
        public void AirSpaceMonitor_CheckAirspace_ZCoordinateOutOfMaxBound()
        {
            Plane_Out.ObservedPosition.Coordinate.Z = 20001;

            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_Out);


            List<Plane> testListPlanes = uut.CheckAirSpace(testListObservations);

            CollectionAssert.IsEmpty(testListPlanes);
        }
<<<<<<< HEAD

        [Test]
        public void AirSpaceMonitor_CheckAirspace_PlaneXCoordinateInListUpdated()
        {
            
            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_In);

            List < Plane > testListPlanes = uut.CheckAirSpace(testListObservations);

            Plane_In.ObservedPosition.Coordinate.X = 15000;

            testListObservations.Clear();

            testListObservations.Add(Plane_In);

            testListPlanes = uut.CheckAirSpace(testListObservations);

            Assert.That(testListPlanes[0].Positions[0].Coordinate.X.Equals(15000));


        }

        


=======
>>>>>>> 2119c6964de46f60e079db93f1fc21489d7df94f
    }
}