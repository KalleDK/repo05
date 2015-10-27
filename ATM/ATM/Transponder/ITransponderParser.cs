using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Transponder
{
    interface ITransponderParser
    {
        List<Plane> ParseRawData(List<string> rawData);
    }
}
