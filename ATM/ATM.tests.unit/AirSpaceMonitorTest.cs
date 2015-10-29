using System.Collections.Generic;
using ATM.AirSpace;
using ATM.Models;
using NUnit.Framework;


namespace ATM.tests.unit
{
    [TestFixture]
    public class AirSpaceMonitorTest
    {
        private AirSpaceMonitor uut;

        [SetUp]
        public void SetUp()
        {
            var minCoordinate = new Coordinate {X = 1000, Y = 1000, Z = 10};

            var maxCoordinate = new Coordinate {X = 10000, Y = 10000, Z=100};

            var airspace = new AirspaceModel(minCoordinate,maxCoordinate);

            uut = new AirSpaceMonitor(airspace);
        }

        [Test]
        public void AirSpaceMonitor_Constructor_Correct

        [Test]
        public void AirSpaceMonitor_CheckAirSpace_ListDoesNotContainPlaneout()
        {
            List<Plane> testList = new List<Plane>();

            

            testList.Add(Testplane_In);

            testList.Add(Testplane_Out);

            testList = uut.CheckAirSpace(testList);


            CollectionAssert.DoesNotContain(testList,Testplane_Out);
        }
        
         
    }


}