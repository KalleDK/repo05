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

<<<<<<< HEAD
        private PlaneObservation Plane_Out;

        private PlaneObservation Plane_In;
        


=======
>>>>>>> 9b3cb7e404521f301e4c5d40614dc87a8772ad45
        [SetUp]
        public void Setup()
        {
<<<<<<< HEAD
            

            uut = new AirSpaceMonitor();

            Plane_Out = new PlaneObservation() {Tag = "Plane_Out", ObservedPosition = new Position() {Coordinate = new Coordinate(9000,10000,500)} }; 

            Plane_In = new PlaneObservation() {Tag = "Plane_In", ObservedPosition = new Position() {Coordinate = new Coordinate(10000,10000,500)} };

        }

        [Test]
        public void AirSpaceMonitor_CheckAirspaceWithXCoordinateOutOfBound_ListIsEmpty()
        {
          //Plane_out x coordinate is set in setup to be out of bound  

            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_Out);

            Plane testPlane = new Plane() {Tag = Plane_Out.Tag};

            List<Plane> testListPlanes =  uut.CheckAirSpace(testListObservations);

            CollectionAssert.IsEmpty(testListPlanes);
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
=======
            var minCoordinate = new Coordinate {X = 1000, Y = 1000, Z = 10};

            var maxCoordinate = new Coordinate {X = 10000, Y = 10000, Z=100};

            var airspace = new AirspaceModel(minCoordinate,maxCoordinate);

            uut = new AirSpaceMonitor(airspace);
        }

        [Test]
        public void AirSpaceMonitor_Constructor_Correct
>>>>>>> 9b3cb7e404521f301e4c5d40614dc87a8772ad45

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
        public void AirSpaceMonitor_CheckAirspaceWithYCoordinateOutOfBound_ListIsEmpty()
        {

            Plane_Out.ObservedPosition.Coordinate.X = 10000;

            Plane_Out.ObservedPosition.Coordinate.Y = 9000;


            List<PlaneObservation> testListObservations = new List<PlaneObservation>();

            testListObservations.Add(Plane_Out);


            List<Plane> testListPlanes = uut.CheckAirSpace(testListObservations);

            CollectionAssert.IsEmpty(testListPlanes);
        }


    }


}