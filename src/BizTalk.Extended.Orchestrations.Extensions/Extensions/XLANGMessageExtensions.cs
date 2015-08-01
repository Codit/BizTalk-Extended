using BizTalk.Extended.Core.Exceptions;
using BizTalk.Extended.Core.Guards;
using Microsoft.XLANGs.BaseTypes;
using System;

namespace BizTalk.Extended.Core.Extensions
{
    public static class XLANGMessageExtensions
    {
        public static void WriteContextProperty<TContextProperty>(this XLANGMessage msg, object value) where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(msg, "msg");

            var contextProperty = new TContextProperty();
            object actualValue = value;

            if (typeof(TContextProperty).IsEnum || value is Enum)
            {
                actualValue = value.ToString();
            }

            msg.SetPropertyValue(typeof(TContextProperty), actualValue);
        }

        public static TExpected ReadContextProperty<TContextProperty, TExpected>(this XLANGMessage msg)
            where TContextProperty : MessageContextPropertyBase, new()
        {
            return ReadContextProperty<TContextProperty, TExpected>(msg, true);
        }

        public static TExpected ReadContextProperty<TContextProperty, TExpected>(this XLANGMessage msg, bool isMandatory)
            where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(msg, "msg");

            var contextProperty = new TContextProperty();
            object value = msg.GetPropertyValue(typeof(TContextProperty));

            if (value == null && isMandatory)
            {
                throw new ContextPropertyNotFoundException(contextProperty.Name.Name, contextProperty.Name.Namespace);
            }

            if (typeof(TExpected).IsEnum)
            {
                return (TExpected)Enum.Parse(typeof(TExpected), value as string);
            }

            return (TExpected)value;
        }
    }
}
