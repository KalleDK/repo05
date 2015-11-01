using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Time
{
    public abstract class TimeProvidor
    {
        private static DateTime? _fake;

        public static void Reset()
        {
            _fake = null;
        }

        public static void Set(DateTime fake)
        {
            _fake = fake;
        }

        public static DateTime Now
        {
            get
            {
                if (_fake == null)
                    return DateTime.Now;
                return (DateTime) _fake;
            }
        }

    }
}
