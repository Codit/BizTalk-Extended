using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BizTalk.Extended.Core.Exceptions
{
     [Serializable]
    public class ContextPropertyNotFoundException : Exception
    {
        public string ContextPropertyName { get; set; }
        public string ContextPropertyNamespace { get; set; }

        public ContextPropertyNotFoundException()
        {
        }

        public ContextPropertyNotFoundException(string name, string @namespace)
        {
            ContextPropertyName = name;
            ContextPropertyNamespace = @namespace;
        }

        public ContextPropertyNotFoundException(string message)
            : base(message)
        {
        }

        public ContextPropertyNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ContextPropertyNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
