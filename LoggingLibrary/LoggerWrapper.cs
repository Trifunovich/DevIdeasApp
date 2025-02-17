﻿using System;
using Microsoft.Extensions.Logging;

namespace LoggingLibrary
{
  public class LoggerWrapper : ILogger
  {
    /// <summary>Writes a log entry.</summary>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="eventId">Id of the event.</param>
    /// <param name="state">The entry to be written. Can be also an object.</param>
    /// <param name="exception">The exception related to this entry.</param>
    /// <param name="formatter">Function to create a <see cref="T:System.String" /> message of the <paramref name="state" /> and <paramref name="exception" />.</param>
    /// <typeparam name="TState">The type of the object to be written.</typeparam>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
      throw new NotImplementedException();
    }

    /// <summary>Checks if the given <paramref name="logLevel" /> is enabled.</summary>
    /// <param name="logLevel">level to be checked.</param>
    /// <returns>
    /// <see langword="true" /> if enabled; <see langword="false" /> otherwise.</returns>
    public bool IsEnabled(LogLevel logLevel)
    {
      throw new NotImplementedException();
    }

    /// <summary>Begins a logical operation scope.</summary>
    /// <param name="state">The identifier for the scope.</param>
    /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
    /// <returns>A disposable object that ends the logical operation scope on dispose.</returns>
    public IDisposable BeginScope<TState>(TState state)
    {
      throw new NotImplementedException();
    }
  }
}