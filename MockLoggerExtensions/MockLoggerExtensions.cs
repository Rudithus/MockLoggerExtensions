using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;

namespace MockLoggerExtensions
{
    public static class MockLoggerExtensions
    {
        public static void VerifyErrorLogged<T, TException>(this Mock<ILogger<T>> logger, TException ex)
            where TException : Exception
        {
            logger.Verify(l => l.Log(
                          LogLevel.Error,
                          It.IsAny<EventId>(),
                          It.IsAny<FormattedLogValues>(),
                          ex,
                          It.IsAny<Func<object, Exception, string>>()
                          ), Times.Once()
                      );
        }

        public static void VerifyErrorLogged<T, TException>(this Mock<ILogger<T>> logger, string exceptionMessage)
            where TException : Exception
        {
            logger.Verify(l => l.Log(
                          LogLevel.Error,
                          It.IsAny<EventId>(),
                          It.IsAny<FormattedLogValues>(),
                          It.IsAny<Exception>(),
                          It.Is<Func<object, Exception, string>>(a => a.Invoke(It.IsAny<object>(), It.IsAny<Exception>()) == exceptionMessage)
                          ), Times.Once()
                      );
        }
        public static void VerifyErrorLogged<T>(this Mock<ILogger<T>> logger, string exceptionMessage)
        {
            logger.Verify(l => l.Log(
                          LogLevel.Error,
                          It.IsAny<EventId>(),
                          It.Is<FormattedLogValues>(a => a.ToString() == exceptionMessage),
                          It.IsAny<Exception>(),
                          It.IsAny<Func<object, Exception, string>>()
                          )
                      );
        }
    }
}
