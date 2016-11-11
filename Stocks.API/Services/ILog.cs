using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;

namespace Stocks.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILog
    {
        void Log(TraceEventType e, string message);
    }
}
