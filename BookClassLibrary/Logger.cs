using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BookClassLibrary
{
    public static class Logger
    {
        static Logger()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        public static NLog.Logger Log { get; private set; }
    }
}
