using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalk.Extended.Pipelines.Components
{
    /// <summary>
    /// Represents a Context Property to Promote or Write
    /// </summary>
    public class BaseProperty
    {
        private string _name = string.Empty;
        private string _toPropertyName = string.Empty;
        private string _toPropertyNamespace = string.Empty;
        private string _toPropertyValue = string.Empty;
        private string _fromProperty = string.Empty;
        private string _fromPropertyNamespace = string.Empty;
        private bool _promote = false;

        [Browsable(true)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [Browsable(true)]
        public string ToPropertyName
        {
            get { return _toPropertyName; }
            set { _toPropertyName = value; }
        }

        [Browsable(true)]
        public string ToPropertyNamespace
        {
            get { return _toPropertyNamespace; }
            set { _toPropertyNamespace = value; }
        }

        [Browsable(true)]
        public string ToPropertyValue
        {
            get { return _toPropertyValue; }
            set { _toPropertyValue = value; }
        }

        [Browsable(true)]
        public bool Promote
        {
            get { return _promote; }
            set { _promote = value; }
        }

        [Browsable(true)]
        public string FromPropertyName
        {
            get { return _fromProperty; }
            set { _fromProperty = value; }
        }

        [Browsable(true)]
        public string FromPropertyNamespace
        {
            get { return _fromPropertyNamespace; }
            set { _fromPropertyNamespace = value; }
        }
    }
}
