using System;

namespace Hyperdrive.Main.Domain.Exceptions;

/// <summary>
/// Represents a <see cref="BrokerException"/> class. Inherits <see cref="Exception"/>
/// </summary>
public class BrokerException : Exception
{
    /// <summary>
    /// Initializes a new Instance of <see cref="BrokerException"/>
    /// </summary>
    public BrokerException() : base() { }

    /// <summary>
    /// Initializes a new Instance of <see cref="ServiceException"/>
    /// </summary>
    /// <param name="message">Instance of <see cref="string"/></param>
    public BrokerException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new Instance of <see cref="ServiceException"/>
    /// </summary>
    /// <param name="message">Instance of <see cref="string"/></param>
    /// <param name="innerException">Instance of <see cref="Exception"/>></param>
    public BrokerException(string message, Exception innerException) : base(message, innerException) { }
}
