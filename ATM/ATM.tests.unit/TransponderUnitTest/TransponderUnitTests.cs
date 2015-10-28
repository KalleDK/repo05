using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;
using NUnit.Framework;
using ATM.Transponder;

namespace ATM.tests.unit.TransponderUnitTest
{
    [TestFixture]
    class TransponderParserUnitTests
    {
        private const string WrongFormatDate = "782";
        private const string CorrectFormatDate = "20151014125634999";
        private const string WrongFormatData = "123;sdfsdf;123;";
        private const string CorrectFormatData = "ATR423;39045;12932;21000;20151006213456789";
        private TransponderParser uut;

        [SetUp]
        public void Setup()
        {
            uut = new TransponderParser();
        }

        [Test]
        [ExpectedException("System.NotSupportedException")]
        public void ParseRawDateTime_WrongDateFormat_Throws()
        {
            uut.ParseDateTime(WrongFormatDate);
        }

        [Test]
        public void ParseRawDateTime_CorrectDateFormat_ReturnsDateTime()
        {
            var result = uut.ParseDateTime(CorrectFormatDate);
            Assert.That(result,Is.TypeOf<DateTime>());
        }

        [Test]
        [ExpectedException("System.NotSupportedException")]
        public void ParseRawData_WrongNumberOfArguments_Throws()
        {
            var wrongList = new List<string>() {WrongFormatData};
            uut.ParseRawData(wrongList);
        }

        [Test]
        public void ParseRawData_CorrectNumberOfArgs_ReturnsPlaneObservationList()
        {
            var rightList = new List<string>() {CorrectFormatData};
            var parsed = uut.ParseRawData(rightList);
            Assert.That(parsed, Is.TypeOf<List<PlaneObservation>>());
            
        }

    }
}
