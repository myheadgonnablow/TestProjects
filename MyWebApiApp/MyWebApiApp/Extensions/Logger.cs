using Microsoft.Extensions.Logging;
using System;

namespace MyWebApiApp.Extensions
{
    public static class Logger
    {
        public static void LogError(this ILogger logger, Exception e)
        {
            logger.LogError(default(EventId), e, e.Message);
        }
    }
}
