﻿namespace AuthenticationService.Api.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogError(Exception exception);
    }
}
