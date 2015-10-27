using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM.Transponder;

namespace ATM.tests.unit
{
    [TestFixture]
    class TransponderParserUnitTests
    {
        private const string WrongFormat = "782";
        private const string CorrectFormat = "20151014125634999";
        private TransponderParser uut;

        [SetUp]
        public void Setup()
        {
            uut = new TransponderParser();
        }

        [Test]
        [ExpectedException("System.NotSupportedException")]
        public void ParseRawDate_WrongDateFormat_Throws()
        {
            uut.ParseDateTime(WrongFormat);
        }

        [Test]
        public void ParseRawDate_CorrectDateFormat_ReturnsDateTime()
        {
            var result = uut.ParseDateTime(CorrectFormat);
            Assert.That(result,Is.TypeOf<DateTime>());
        }
    }
}
