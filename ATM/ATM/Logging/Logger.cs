using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace ATM.Logging
{
    class Logger : ILogger
    {
        private readonly log4net.ILog _log4Net;
        public static void Start()
        {
            BasicConfigurator.Configure();
        }
        public Logger(System.Type classtype)
        {
            _log4Net = LogManager.GetLogger(classtype);
        }
        public void Info(string line)
        {
            _log4Net.Info(line);
        }
        public void Warning(string line)
        {
            _log4Net.Warn(line);
        }
    }
}
