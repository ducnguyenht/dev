using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace NAS.GUI.Pattern
{
    public class IncompatibleTransitionException : Exception
    {
        public IncompatibleTransitionException()
            : base() { }

        public IncompatibleTransitionException(string message)
            : base(message) { }

        public IncompatibleTransitionException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public IncompatibleTransitionException(string message, Exception innerException)
            : base(message, innerException) { }

        public IncompatibleTransitionException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected IncompatibleTransitionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}