using System;
using System.Diagnostics;

namespace BizTalk.Extended.Core.Guards
{
    public static class Guard
    {
        [DebuggerStepThrough]
        public static void NotNull<T>(T value, string paramName) where T : class
        {
            Guard.NotNullOrWhitespace(paramName, "paramName");

            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        [DebuggerStepThrough]
        public static void NotNullOrEmpty(string value, string paramName)
        {
            Guard.NotNullOrWhitespace(paramName, "paramName");
            Guard.NotNull(value, paramName);

            if (value.Length == 0)
            {
                throw new ArgumentException("Value cannot be empty", paramName);
            }
        }

        [DebuggerStepThrough]
        public static void NotNullOrWhitespace(string value, string paramName)
        {
            if (paramName == null)
            {
                throw new ArgumentNullException("paramName");
            }

            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (string.IsNullOrWhiteSpace(paramName))
            {
                throw new ArgumentException("paramName");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(paramName);
            }
        }

        [DebuggerStepThrough]
        public static void Against(bool condition, string paramName)
        {
            Guard.NotNullOrWhitespace(paramName, "paramName");

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
