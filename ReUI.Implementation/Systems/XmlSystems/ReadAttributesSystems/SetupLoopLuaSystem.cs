using System;
using ReUI.Implementation.Helpers;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class SetupLoopLuaSystem : AbstractSetupAttributeSystem
    {
        protected override Type[] EnsureTypes => new [] { typeof(LoopType)};

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            if(!xml.HasAttribute("Itteration") || !xml.HasAttribute("Collection"))
                throw new ArgumentException("Loop element require code for Itteration and Collection");

            var itt = entity.Need<LuaCodeLoopItteration>();
            var col = entity.Need<LuaCodeLoopCollection>();

            itt.Value = xml.Attributes["Itteration"];
            col.Value = xml.Attributes["Collection"];

            entity.ReplaceInstance(itt).ReplaceInstance(col);
        }
    }
}