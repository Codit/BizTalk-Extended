using BizTalk.Extended.Core.Guards;
using Microsoft.XLANGs.BaseTypes;
using System;

namespace BizTalk.Extended.Core.Utilities
{
    public class ContextPropertySerializer
    {
        /// <summary>
        /// Serializes the value so it can be used in the message context.
        /// </summary>
        /// <typeparam name="TContextProperty"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object SerializeToContextPropertyValue<TContextProperty>(object value) where TContextProperty : MessageContextPropertyBase, new()
        {
            var contextProperty = new TContextProperty();
            object actualValue = value;

            if (typeof(TContextProperty).IsEnum || value is Enum)
            {
                actualValue = value.ToString();
            }

            return actualValue;
        }

        /// <summary>
        /// Deserializes the value from the message context to the required type.
        /// </summary>
        /// <typeparam name="TExpected"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TExpected DeserializeFromContextPropertyValue<TExpected>(object value)
        {
            if (typeof(TExpected).IsEnum)
            {
                return (TExpected)Enum.Parse(typeof(TExpected), value as string);
            }

            Type underlyingType = Nullable.GetUnderlyingType(typeof(TExpected));
            if (underlyingType != null && underlyingType.IsEnum)
            {
                if (value == null)
                {
                    return default(TExpected);
                }

                return (TExpected)Enum.Parse(underlyingType, value as string);
            }

            return (TExpected)value;
        }
    }
}
