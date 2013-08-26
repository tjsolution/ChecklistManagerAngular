using System;
using System.Runtime.Serialization;

namespace ChecklistManager.Repository
{
    /// <summary>
    /// Exception for signalling repository errors. 
    /// </summary>
    [Serializable]
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the RepositoryException class.
        /// </summary>
        public RepositoryException() { }
        /// <summary>
        /// Initializes a new instance of the RepositoryException class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RepositoryException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the RepositoryException class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public RepositoryException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Initializes a new instance of the RepositoryException class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected RepositoryException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }

        public static RepositoryException Create(string message, params object[] args)
        {
            if (args == null)
            {
                return new RepositoryException(message);
            }
            return new RepositoryException(string.Format(message, args));
        }
    }
}
