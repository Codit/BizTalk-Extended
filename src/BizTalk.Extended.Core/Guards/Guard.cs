using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalk.Extended.Core.Guards
{
    public static class Guard
    {
        [DebuggerStepThrough]
        public static void NotNull<T>(T value, string paramName) where T : class
        {
            if (paramName == null)
            {
                throw new ArgumentNullException("paramName");
            }

            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        [DebuggerStepThrough]
        public static void NotNullOrEmpty(string value, string paramName)
        {
            Guard.NotNull(paramName, "paramName");
            Guard.NotNull(value, paramName);

            if (value.Length == 0)
            {
                throw new ArgumentException("Value cannot be empty", paramName);
            }
        }

        [DebuggerStepThrough]
        public static void Against(bool condition, string paramName)
        {
            Guard.NotNull(paramName, "paramName");

            if (condition)
            {
                throw new ArgumentException(paramName);
            }
        }

        [DebuggerStepThrough]
        public static void Against<TException>(bool condition) where TException : Exception, new()
        {
            if (condition)
            {
                throw new TException();
            }
        }
    }
}
