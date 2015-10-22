using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using log4net;
using log4net.Config;

namespace ATM.Logging
{
    public interface ILogger
    {
        void Info(string line);
        void Warning(string line);
    }
}