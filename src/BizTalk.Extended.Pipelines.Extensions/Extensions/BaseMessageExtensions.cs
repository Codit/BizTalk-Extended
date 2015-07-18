using BizTalk.Extended.Core.Exceptions;
using BizTalk.Extended.Core.Guards;
using Microsoft.XLANGs.BaseTypes;
using System;

namespace Microsoft.BizTalk.Message.Interop
{
    public static class BaseMessageExtensions
    {
        // TODO: introduce message extension interface to ensure that XLANG & BaseMessage have the same capabilities

                public static void PromoteContextProperty<TContextProperty>(this IBaseMessage msg, object value) where TContextProperty : MessageContextPropertyBase, new()
        {
            var contextProperty = new TContextProperty();

            PromoteContextProperty(msg, contextProperty.Name.Name, contextProperty.Name.Namespace, value);
        }

        public static void PromoteContextProperty(this IBaseMessage message, string name, string ns, object value)
        {
            Guard.NotNull(message, "message");
            Guard.NotNull(name, "name");
            Guard.NotNull(value, "value");
            Guard.NotNull(ns, "ns");

            message.Context.Promote(name, ns, value);
        }

        public static void WriteContextProperty<TContextProperty>(this IBaseMessage msg, object value) where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(msg, "msg");
            Guard.Against(msg.Context == null, "msg");

            var contextProperty = new TContextProperty();
            object actualValue = value;

            if (typeof(TContextProperty).IsEnum || value is Enum)
            {
                actualValue = value.ToString();
            }

            msg.Context.Write(contextProperty.Name.Name, contextProperty.Name.Namespace, actualValue);
        }

        public static TExpected ReadContextProperty<TContextProperty, TExpected>(this IBaseMessage msg)
            where TContextProperty : MessageContextPropertyBase, new()
        {
            return ReadContextProperty<TContextProperty, TExpected>(msg, true);
        }

        public static TExpected ReadContextProperty<TContextProperty, TExpected>(this IBaseMessage msg, bool isMandatory)
            where TContextProperty : MessageContextPropertyBase, new()
        {
            var contextProperty = new TContextProperty();

            return ReadContextProperty<TExpected>(msg, contextProperty.Name.Name, contextProperty.Name.Namespace, isMandatory);
        }

        public static T ReadContextProperty<T>(this IBaseMessage msg, string name, string ns)
        {
            return ReadContextProperty<T>(msg, name, ns, true);
        }

        public static T ReadContextProperty<T>(this IBaseMessage msg, string name, string ns, bool isMandatory)
        {
            Guard.NotNull(msg, "msg");
            Guard.Against(msg.Context == null, "msg");
            Guard.NotNullOrEmpty(name, "name");
            Guard.NotNullOrEmpty(ns, "ns");

            object value = msg.Context.Read(name, ns);
            if (value == null && isMandatory)
            {
                throw new ContextPropertyNotFoundException(name, ns);
            }

            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), value as string);
            }

            Type underlyingType = Nullable.GetUnderlyingType(typeof(T));
            if (underlyingType != null && underlyingType.IsEnum)
            {
                if (value == null)
                {
                    return default(T);
                }

                return (T)Enum.Parse(underlyingType, value as string);
            }

            return (T)value;
        }
    }
}
