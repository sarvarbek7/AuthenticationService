﻿namespace AuthenticationService.Api.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) 
        {
            this.logger = logger;
        }

        public void LogError(Exception exception)
        {
            this.logger.LogError(exception, exception.Message);
        }
    }
}
