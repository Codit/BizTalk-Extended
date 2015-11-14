using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BizTalk.Extended.Core.Guards;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;

using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;

namespace BizTalk.Extended.Pipelines.General
{
    public abstract class PipelineComponent<T> : IBaseComponent,
                                                 IPersistPropertyBag,
                                                 IComponent,
                                                 IComponentUI
                                                 where T : PipelineComponent<T>
    {

        #region Public Properties
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
        #endregion

        #region Pipeline component
        /// <summary>
        /// Method that will contain the actual execute implementation.
        /// </summary>
        /// <param name="pipelineContext">Context of the pipeline.</param>
        /// <param name="message">message that passes through the pipeline.</param>
        /// <returns></returns>
        protected abstract IBaseMessage ExecuteInternal(IPipelineContext pipelineContext, IBaseMessage message);

        /// <summary>
        /// Public execute metod of the pipeline.
        /// </summary>
        /// <param name="pipelineContext"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public IBaseMessage Execute(IPipelineContext pipelineContext, IBaseMessage message)
        {
            Guard.NotNull(pipelineContext, "pipelineContext");
            Guard.NotNull(message, "message");

            IBaseMessage returnMessage = message;

            if (IsEnabled)
            {
                returnMessage = ExecuteInternal(pipelineContext, message);
            }

            return returnMessage;
        }
        #endregion

        #region IComponentUI Members
        public IntPtr Icon
        {
            get
            {
                return IntPtr.Zero;
            }
        }

        public virtual IEnumerator Validate(object projectSystem)
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

        public void InitNew()
        {
            IsEnabled = true;
        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            Guard.NotNull(propertyBag, "propertyBag");

            IsEnabled = LoadProperty(propertyBag, x => x.IsEnabled, IsEnabled);
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            Guard.NotNull(propertyBag, "propertyBag");

            SaveProperty(propertyBag, x => x.IsEnabled);
        }

        /// <summary>
        /// Loads the property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyBag">The property bag.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected virtual TProperty LoadProperty<TProperty>(IPropertyBag propertyBag, Expression<Func<T, TProperty>> propertyExpression, TProperty defaultValue)
        {
            Guard.NotNull(propertyBag, "propertyBag");
            Guard.NotNull(propertyExpression, "propertyExpression");

            object propertyValue = defaultValue;
            try
            {
                propertyBag.Read(((MemberExpression)propertyExpression.Body).Member.Name, out propertyValue, 0);
            }
            catch (ArgumentException)
            {
                return defaultValue;
            }

            if (propertyValue != null)
            {
                return (TProperty)propertyValue;
            }

            return propertyExpression.Compile()((T)this);
        }

        /// <summary>
        /// Reads the property.
        /// </summary>
        /// <param name="propertyBag">The property bag.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException"></exception>
        protected virtual object LoadProperty(IPropertyBag propertyBag, string propertyName, object defaultValue)
        {
            Guard.NotNull(propertyBag, "propertyBag");
            Guard.NotNull(propertyName, "propertyName");
            Guard.NotNull(defaultValue, "defaultValue");

            object value = defaultValue;

            try
            {
                propertyBag.Read(propertyName, out value, 0);

                if (value == null)
                    value = defaultValue;
            }
            catch (ArgumentException)
            {
                value = defaultValue;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return value;

        }

        /// <summary>
        /// Saves the property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyBag">The property bag.</param>
        /// <param name="propertyExpression">The property expression.</param>
        protected virtual void SaveProperty<TProperty>(IPropertyBag propertyBag, Expression<Func<T, TProperty>> propertyExpression)
        {
            Guard.NotNull(propertyBag, "propertyBag");
            Guard.NotNull(propertyExpression, "propertyExpression");

            object propValue = propertyExpression.Compile()((T)this);
            string propName = ((MemberExpression)propertyExpression.Body).Member.Name;

            propertyBag.Write(propName, ref propValue);
        }

        #endregion

    }
}
