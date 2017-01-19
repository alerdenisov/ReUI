using System.Collections.Generic;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation
{
    public class SetupElementTypeSystem : IReactiveSystem<IUIPool>
    {
        public static HashSet<string> _standardElements = new HashSet<string>
        {
            "Root",
            "GameObject",
            "Color",
            "Image",
            "Text",
            "Texture",
            "Loop"
        };

        public static Dictionary<string, Elements> _standardTypes = new Dictionary<string, Elements>()
        {
            {"Root",        Elements.Root},
            {"GameObject",  Elements.GameObject},
            {"Color",       Elements.Sprite},
            {"Image",       Elements.Sprite},
            {"Text",        Elements.Text},
            {"Texture",     Elements.RawImage},
            {"Loop",        Elements.Loop}  

        };

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var element in entities)
            {
                SetupType(element);
            }
        }

        private Entity<IUIPool> SetupType(Entity<IUIPool> element)
        {
            var xml = element.Get<XmlElement>();

            // Check element is reqular element or custom one
            var type = _standardElements.Contains(xml.Name) 
                ? _standardTypes[xml.Name] 
                : Elements.Embed;

            var viewType = element.Need<ElementType>();
            viewType.Value = type;

            if (type == Elements.Embed)
            {
                element.Add<Embed>(e => e.Name = xml.Name);
            }


            return element
                .Toggle<ScopeType>(type == Elements.Root)
                .Toggle<LoopType>(type == Elements.Loop)
                .Toggle<TextureType>(type == Elements.RawImage)
                .Toggle<SpriteType>(type == Elements.Sprite)
                .Toggle<TextType>(type == Elements.Text)
                .ReplaceInstance(viewType);
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (Element), typeof (XmlElement)).OnEntityAdded();
    }
}