using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using ATM.Logging;

namespace ATM.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Start();
            Transponder.TransponderMonitor TestTrans = new Transponder.TransponderMonitor();
            Console.ReadKey();
        }
    }

    

}
