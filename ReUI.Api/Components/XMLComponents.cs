using System;
using System.Collections.Generic;

namespace ReUI.Api
{
    /// <summary>
    /// Data of input xml
    /// </summary>
    public class XmlData : IXmlComponents
    {
        /// <summary>
        /// Raw text value of xml document
        /// Typically obtained by File.ReadAllText(xmlPath)
        /// </summary>
        public string Value;
    }

    /// <summary>
    /// Readed xml document (ready to parse)
    /// </summary>
    public class XmlDocument : IXmlComponents
    {
        /// <summary>
        /// Instance of readed xml file
        /// </summary>
        public IXmlNode Root;
    }

    /// <summary>
    /// Parsed xml ui element representation
    /// </summary>
    public class XmlElement : IXmlComponents
    {
        /// <summary>
        /// Element name (tag based)
        /// </summary>
        public string Name;

        /// <summary>
        /// Value of element
        /// </summary>
        public string Content;

        /// <summary>
        /// Code of ui element
        /// </summary>
        [Obsolete("Now code is stored in method named attribute", true)]
        public string Code;

        /// <summary>
        /// Dictionary of element attributes
        /// </summary>
        public Dictionary<string, string> Attributes;
    }
}