﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Transponder
{
    public interface ITransponderMonitor : IObservable<List<Plane>>
    {
 
    }
}
