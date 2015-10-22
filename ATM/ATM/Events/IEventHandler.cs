using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Events
{
    interface IEventHandler
    {
        void CheckForEvent(List<Plane> activePlanes);
    }
}
