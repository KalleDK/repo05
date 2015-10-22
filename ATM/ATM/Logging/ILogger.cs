using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace ATM.Logging
{
    public interface ILogger
    {
        void Info(string line);
        void Warning(string line);
    }
}