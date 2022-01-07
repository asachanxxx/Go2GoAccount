using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Core.Exceptions
{
    [Serializable]
    public class UserLoginException : Exception
    {
        public UserLoginException()
        {
        }

        public UserLoginException(string message)
            : base(message)
        {
        }

        public UserLoginException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected UserLoginException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
