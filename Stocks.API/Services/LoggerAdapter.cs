using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Stocks.Services
{
    public class LoggerAdapter : ILog
    {
        private readonly ILogger logger;
        public LoggerAdapter(ILogger logger) { this.logger = logger; }
        public void Log(TraceEventType e, string message) => logger.Log(ToLevel(e), 0, message, null, (s,_)=>s);

        private static LogLevel ToLevel(TraceEventType s) => s == TraceEventType.Warning ? LogLevel.Warning :
                                                               s == TraceEventType.Error ? LogLevel.Error :
                                                               LogLevel.Critical;


    }
}