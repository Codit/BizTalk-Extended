using BizTalk.Extended.Core.Exceptions;
using BizTalk.Extended.Core.Guards;
using BizTalk.Extended.Core.Utilities;

namespace Microsoft.XLANGs.BaseTypes
{
    public static class XLANGMessageExtensions
    {
        /// <summary>
        /// Writes to the context of the message
        /// </summary>
        /// <typeparam name="TContextProperty">Type of the target property</typeparam>
        /// <param name="message">BizTalk pipeline message</param>
        /// <param name="value">Requested value of the property</param>
        public static void WriteContextProperty<TContextProperty>(this XLANGMessage message, object value) where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(message, "message");

            object actualValue = ContextPropertySerializer.SerializeToContextPropertyValue<TContextProperty>(value);

            message.SetPropertyValue(typeof(TContextProperty), actualValue);
        }

        /// <summary>
        /// Reads a mandatory property in the context of the message
        /// </summary>
        /// <typeparam name="TContextProperty">Type of the target property</typeparam>
        /// <typeparam name="TExpected">Expected type of the value</typeparam>
        /// <param name="message">BizTalk pipeline message</param>
        /// <returns>Value from the property, if present</returns>
        /// <exception cref="BizTalk.Extended.Core.Exceptions.ContextPropertyNotFoundException">Thrown when a mandatory property is not present</exception>
        public static TExpected ReadContextProperty<TContextProperty, TExpected>(this XLANGMessage message)
            where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(message, "message");

            return ReadContextProperty<TContextProperty, TExpected>(message, isMandatory: true);
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
        public static TExpected ReadContextProperty<TContextProperty, TExpected>(this XLANGMessage message, bool isMandatory) where TContextProperty : MessageContextPropertyBase, new()
        {
            Guard.NotNull(message, "message");

            object value = message.GetPropertyValue(typeof(TContextProperty));
            if (value == null && isMandatory)
            {
                var propertyInstance = new TContextProperty();
                throw new ContextPropertyNotFoundException(propertyInstance.Name.Name, propertyInstance.Name.Namespace);
            }

            return ContextPropertySerializer.DeserializeFromContextPropertyValue<TExpected>(value);
        }
    }
}
