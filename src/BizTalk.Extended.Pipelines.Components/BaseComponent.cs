using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalk.Extended.Pipelines.Components
{
    public abstract class BaseComponent : IBaseComponent, IPersistPropertyBag,
        Microsoft.BizTalk.Component.Interop.IComponent, IComponentUI
    {
        /// <summary>
        /// Gets or sets a value to enable (true) or disable (false) the
        /// pipeline component.
        /// </summary>
        [Browsable(true)]
        [Description("Enables (true) or disables (false) the pipeline component.")]
        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set { this._isEnabled = value; }
        }

        #region IComponentUI members

        [Browsable(false)]
        public IntPtr Icon
        {
            get { return IntPtr.Zero; }
        }

        public IEnumerator Validate(object obj)
        {
            return null;
        }

        #endregion

        #region IBaseComponent Members
        /// <summary>
        /// Description of the component
        /// </summary>
        [Browsable(false)]
        public abstract string Description { get; }

        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        public abstract string Name { get; }

        /// <summary>
        /// Version of the component
        /// </summary>
        [Browsable(false)]
        public abstract string Version { get; }

        #endregion

        #region IPersistPropertyBag Members

        public void GetClassID(out Guid classID)
        {
            classID = GetType().GUID;
        }

        public virtual void InitNew()
        {
            IsEnabled = true;
        }

        public virtual void Load(IPropertyBag propertyBag, int errorLog)
        {
            IsEnabled = ReadProperty(propertyBag, "IsEnabled", this.IsEnabled);
        }

        public virtual void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            WriteProperty(propertyBag, "IsEnabled", IsEnabled);
        }

        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="pb">Property bag</param>
        /// <param name="propName">Name of property</param>
        /// <returns>Value of the property</returns>
        protected object ReadProperty(IPropertyBag pb, string propName, object defaultValue)
        {
            object val = defaultValue;

            try
            {
                pb.Read(propName, out val, 0);
            }
            catch (ArgumentException)
            {
                val = defaultValue;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return val;
        }

        protected T ReadProperty<T>(IPropertyBag pb, string propName, T defaultValue)
        {
            object value = ReadProperty(pb, propName, null);

            if (value != null)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }

            return defaultValue;
        }

        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        protected void WriteProperty(IPropertyBag pb, string propName, object val)
        {
            if (pb != null)
            {
                pb.Write(propName, ref val);
            }
        }

        #endregion

        #region IComponent Members

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            try
            {
                IBaseMessage returnMessage = null;

                if (IsEnabled)
                {
                    returnMessage = ExecuteInternal(pContext, pInMsg);
                }
                else
                {
                    returnMessage = pInMsg;
                }

                return returnMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Derived classes implement
        /// </summary>
        protected abstract IBaseMessage ExecuteInternal(IPipelineContext pContext, IBaseMessage pInMsg);

        #endregion

        #region CustomPropertyMethods

        /// <summary>
        /// Adds a property to the context based on the values in the BaseProperty
        /// </summary>
        /// <param name="pInMsg"></param>
        /// <param name="prop"></param>
        public void AddPropertyToContext(IBaseMessage pInMsg, BaseProperty prop)
        {
            if (prop.Promote)
            {
                pInMsg.Context.Promote(prop.ToPropertyName, prop.ToPropertyNamespace, prop.ToPropertyValue);
            }
            else
            {
                pInMsg.Context.Write(prop.ToPropertyName, prop.ToPropertyNamespace, prop.ToPropertyValue);
            }
        }

        /// <summary>
        /// Copy a property from a certrain property to a certain property, based on the values in the BaseProperty
        /// </summary>
        /// <param name="pInMsg"></param>
        /// <param name="prop"></param>
        /// <param name="removeOriginal"></param>
        public void CopyPropertyFromTo(IBaseMessage pInMsg, BaseProperty prop, bool removeOriginal)
        {
            object fromProperty = ReadPropertyFromContext(pInMsg, prop);

            if (fromProperty != null)
            {
                if (prop.Promote)
                {
                    pInMsg.Context.Promote(prop.ToPropertyName, prop.ToPropertyNamespace, fromProperty.ToString());
                }
                else
                {
                    pInMsg.Context.Write(prop.ToPropertyName, prop.ToPropertyNamespace, fromProperty.ToString());
                }

                if (removeOriginal)
                    RemovePropertyFromContext(pInMsg, prop);
            }
        }

        /// <summary>
        /// Removes a property from the Context by writing null to it
        /// </summary>
        /// <param name="pInMsg"></param>
        /// <param name="prop"></param>
        public void RemovePropertyFromContext(IBaseMessage pInMsg, BaseProperty prop)
        {
            //Write null to the FromProperty to Remove it
            pInMsg.Context.Write(prop.FromPropertyName, prop.FromPropertyNamespace, null);
        }

        /// <summary>
        /// Standard method to read values from the Contextproperty specified in the BaseProperty
        /// </summary>
        /// <param name="pInMsg"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public object ReadPropertyFromContext(IBaseMessage pInMsg, BaseProperty prop)
        {
            return pInMsg.Context.Read(prop.FromPropertyName, prop.FromPropertyNamespace);
        }

        #endregion

    }
}
