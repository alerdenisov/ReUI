using Rentitas;

namespace ReUI.Api
{

    public class LoopType : ILoopComponents, IFlag { }

    public class LoopItem : ILoopComponents, IAttributeValue<string>
    {
        public string Value { get; set; }
    }

    public class LuaCodeLoopItteration : ILuaCodeComponent, ILoopComponents, IAttributeValue<string>
    {
        public string Value { get; set; }
    }

    public class LuaCodeLoopCollection : ILuaCodeComponent, ILoopComponents, IAttributeValue<string>
    {
        public string Value { get; set; }
    }
}