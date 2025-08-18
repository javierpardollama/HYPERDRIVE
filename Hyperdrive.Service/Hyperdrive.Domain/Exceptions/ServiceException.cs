using System;

namespace Hyperdrive.Domain.Exceptions
{
    /// <summary>
    /// Represents a <see cref="ServiceException"/> class. Inherits <see cref="Exception"/>
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="ServiceException"/>
        /// </summary>
        public ServiceException() : base() { }

        /// <summary>
        /// Initializes a new Instance of <see cref="ServiceException"/>
        /// </summary>
        /// <param name="message">Instance of <see cref="string"/></param>
        public ServiceException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new Instance of <see cref="ServiceException"/>
        /// </summary>
        /// <param name="message">Instance of <see cref="string"/></param>
        /// <param name="innerException">Instance of <see cref="Exception"/>></param>
        public ServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
