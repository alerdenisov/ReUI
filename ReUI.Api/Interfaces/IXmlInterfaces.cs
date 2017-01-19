using System.Collections.Generic;

namespace ReUI.Api
{
    public interface IXmlDocument {
        IXmlNode RootNode { get; }
    }
    public interface IXmlNode {
        string Name { get; }
        IEnumerable<IXmlNode> SubNodes { get; }
        IEnumerable<IXmlAttribute> Attributes { get; }
        string Value { get; }
    }

    public interface IXmlAttribute
    {
        string Name { get; }
        string Value { get; }
    }
}