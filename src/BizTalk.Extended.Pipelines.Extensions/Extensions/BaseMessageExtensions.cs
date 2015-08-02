using System;

using Microsoft.XLANGs.BaseTypes;

using BizTalk.Extended.Core.Exceptions;
using BizTalk.Extended.Core.Guards;

namespace Microsoft.BizTalk.Message.Interop
{
    public static class BaseMessageExtensions
    {
        /// <summary>
        /// Promotes a property in the the context of the message
        /// </summary>
        /// <typeparam name="TContextProperty">Type of the target property</typeparam>
        /// <param name="message">BizTalk pipeline message</param>
        /// <param name="value">Requested value of the property</param>
        public static void PromoteContextProperty<TContextProperty>(this IBaseMessage message, object value) where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(message, "message");

            var contextProperty = new TContextProperty();

            PromoteContextProperty(message, contextProperty.Name.Name, contextProperty.Name.Namespace, value);
        }

        /// <summary>
        /// Promotes a property in the the context of the message
        /// </summary>
        /// <param name="msg">BizTalk pipeline message</param>
        /// <param name="name">Name of the property</param>
        /// <param name="ns">Namespace of the property</param>
        /// <param name="value">Requested value of the property</param>
        public static void PromoteContextProperty(this IBaseMessage message, string name, string ns, object value)
        {
            Guard.NotNull(message, "message");
            Guard.NotNullOrWhitespace(name, "name");
            Guard.NotNullOrWhitespace(ns, "ns");

            message.Context.Promote(name, ns, value);
        }

        /// <summary>
        /// Writes to the context of the message
        /// </summary>
        /// <typeparam name="TContextProperty">Type of the target property</typeparam>
        /// <param name="message">BizTalk pipeline message</param>
        /// <param name="value">Requested value of the property</param>
        public static void WriteContextProperty<TContextProperty>(this IBaseMessage message, object value) where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(message, "message");
            Guard.Against(message.Context == null, "message");

            var contextProperty = new TContextProperty();
            object actualValue = value;

            if (typeof(TContextProperty).IsEnum || value is Enum)
            {
                actualValue = value.ToString();
            }

            WriteContextProperty(message, contextProperty.Name.Name, contextProperty.Name.Namespace, actualValue);
        }

        /// <summary>
        /// Writes to the context of the message
        /// </summary>
        /// <param name="msg">BizTalk pipeline message</param>
        /// <param name="name">Name of the property</param>
        /// <param name="ns">Namespace of the property</param>
        /// <param name="value">Requested value of the property</param>
        public static void WriteContextProperty(this IBaseMessage message, string name, string ns, object value)
        {
            Guard.NotNullOrWhitespace(name, "name");
            Guard.NotNullOrWhitespace(ns, "ns");
            Guard.NotNull(message, "message");
            Guard.Against(message.Context == null, "message");

            message.Context.Write(name, ns, value);
        }

        /// <summary>
        /// Reads a mandatory property in the context of the message
        /// </summary>
        /// <typeparam name="TContextProperty">Type of the target property</typeparam>
        /// <typeparam name="TExpected">Expected type of the value</typeparam>
        /// <param name="message">BizTalk pipeline message</param>
        /// <returns>Value from the property, if present</returns>
        /// <exception cref="BizTalk.Extended.Core.Exceptions.ContextPropertyNotFoundException">Thrown when a mandatory property is not present</exception>
        public static TExpected ReadContextProperty<TContextProperty, TExpected>(this IBaseMessage message)
            where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(message, "message");

            return ReadContextProperty<TContextProperty, TExpected>(message, true);
        }

        /// <summary>
        /// Reads a property in the context of the message
        /// </summary>
        /// <typeparam name="TContextProperty">Type of the target property</typeparam>
        /// <typeparam name="TExpected">Expected type of the value</typeparam>
        /// <param name="message">BizTalk pipeline message</param>
        /// <param name="isMandatory">Indication if it is mandatory for the property to be present</param>
        /// <returns>Value from the property, if present</returns>
        /// <exception cref="BizTalk.Extended.Core.Exceptions.ContextPropertyNotFoundException">Thrown when a mandatory property is not present</exception>
        public static TExpected ReadContextProperty<TContextProperty, TExpected>(this IBaseMessage message, bool isMandatory) where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(message, "message");

            var contextProperty = new TContextProperty();

            return ReadContextProperty<TExpected>(message, contextProperty.Name.Name, contextProperty.Name.Namespace, isMandatory);
        }

        /// <summary>
        /// Reads a property in the context of the message
        /// </summary>
        /// <typeparam name="TExpected">Expected type of the value</typeparam>
        /// <param name="message">BizTalk pipeline message</param>
        /// <param name="name">Name of the property</param>
        /// <param name="ns">Namespace of the property</param>
        /// <returns>Value from the property, if present</returns>
        public static TExpected ReadContextProperty<TExpected>(this IBaseMessage message, string name, string ns)
        {
            Guard.NotNull(message, "message");
            Guard.NotNullOrWhitespace(name, "name");

            return ReadContextProperty<TExpected>(message, name, ns, true);
        }

        /// <summary>
        /// Reads a property in the context of the message
        /// </summary>
        /// <typeparam name="TExpected">Expected type of the value</typeparam>
        /// <param name="message">BizTalk pipeline message</param>
        /// <param name="name">Name of the property</param>
        /// <param name="ns">Namespace of the property</param>
        /// <param name="isMandatory">Indication if it is mandatory for the property to be present</param>
        /// <returns>Value from the property, if present</returns>
        /// <exception cref="BizTalk.Extended.Core.Exceptions.ContextPropertyNotFoundException">Thrown when a mandatory property is not present</exception>
        public static TExpected ReadContextProperty<TExpected>(this IBaseMessage message, string name, string ns, bool isMandatory)
        {
            Guard.NotNull(message, "msg");
            Guard.Against(message.Context == null, "msg");
            Guard.NotNullOrEmpty(name, "name");
            Guard.NotNullOrEmpty(ns, "ns");

            object value = message.Context.Read(name, ns);
            if (value == null && isMandatory)
            {
                throw new ContextPropertyNotFoundException(name, ns);
            }

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
