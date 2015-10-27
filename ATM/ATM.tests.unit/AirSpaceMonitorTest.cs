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

        private Plane Testplane_In;

        private Plane Testplane_Out;

        private Coordinate _min;

        private Coordinate _max;

        [SetUp]
        public void SetUp()
        {
            _min = new Coordinate() {X = 1000, Y = 1000, Z = 10};

            _max = new Coordinate() {X = 10000, Y = 10000, Z=100};

            Testplane_In = new Plane() {Compass = null,Position = new Coordinate[1], Speed = null, Tag = "TestFly Inde"};

            Testplane_Out = new Plane() { Compass = null, Position = new Coordinate[1], Speed = null, Tag = "TestFly Ude" };

            Testplane_In.Position[0] = new Coordinate() {X=1200, Y=4000, Z=42};

            Testplane_Out.Position[0] = new Coordinate() {X=900, Y=1000, Z=10};

            uut = new AirSpaceMonitor() {Max = _max, Min= _min};

        }


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