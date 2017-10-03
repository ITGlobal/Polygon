using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon.Connector.MicexBridge
{
    /// <summary>
    /// Attribute for properties which are parts of actual connection string passed to mtesrl.dll
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class ConnectionStringPropertyAttribute : Attribute
    {
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="name">Name of a parameter in connection string</param>
        public ConnectionStringPropertyAttribute(string name, bool required = false)
        {
            Name = name;
            Required = required;
        }

        public string Name { get; }

        public bool Required { get; }
    }
}
