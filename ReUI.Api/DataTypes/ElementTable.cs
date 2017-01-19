using System;
using Rentitas;
using UnityEngine;

namespace ReUI.Api
{
    [ExposeToLua]
    public class ElementTable
    {
        protected readonly Entity<IUIPool> Element;
        protected readonly Pool<IUIPool> Ui;
        protected readonly IViewProvider ViewProvider;
        private string _name;

        public ElementTable(Entity<IUIPool> element, Pool<IUIPool> uiPool)
        {
            Element = element;
            Ui = uiPool;
            ViewProvider = uiPool.Get<ViewProvider>().Value;

            _name = element.Get<Name>().Value;
        }

        public override string ToString()
        {
            return "ElementTable: " + _name;
        }

        protected Guid _id() => Element.Need<Element>().Id;
        protected Entity<IUIPool> _parent() => Ui.GetParent(Element);
        public string getId() => _id().ToString();
        public string getName() => _name;//Element.Need<Api.Name>().Value;

        public ElementTable parent() => new ElementTable(_parent(), Ui);

        public Rect rect2 => (ViewProvider.GetByIdentity(Element.Get<ViewLink>().Id).GetObject()?.transform as RectTransform).rect;

        public Rect rect
        {
            get
            {
                if (!Element.Has<ViewLink>()) return default(Rect);
                var view = ViewProvider.GetByIdentity(Element.Get<ViewLink>().Id);

                var gameObject = view.GetObject();
                if (!gameObject) return default(Rect);

                var rectTransform = gameObject.transform as RectTransform;
                if (!rectTransform) return default(Rect);

                return rectTransform.rect;
            }
        }

        public void setAnchor(string data)
        {
            AnchorType type = AnchorType.MiddleCenter;
            EnumConverter<AnchorType>.Convert(data, ref type);
            var anchor = AnchorTypeVectro4Converter.Convert(type);
            Element.SetAttribute<Anchor, Vector4>(anchor);
        }

        public void setDisabled(bool flag)
        {
            Element.Toggle<Disabled>(flag);
        }

        public void setPivot(string data)
        {
            PivotType type = PivotType.MiddleCenter;
            EnumConverter<PivotType>.Convert(data, ref type);
            var pivot = PivotTypeVector2Converter.Convert(type);
            Element.SetAttribute<Pivot, Vector2>(pivot);
        }

        public void setPosition(float x, float y)
        {
            Element.SetAttribute<Position, Vector2>(new Vector2(x, y));
        }

        public void setSize(float x, float y)
        {
            Element.SetAttribute<Size, Vector2>(new Vector2(x, y));
        }

        public void setMargin(float x, float y)
        {
            Element.SetAttribute<Margin, Vector4>(new Vector4(x, y, x, y));
        }

        public void setColor(string stringColor)
        {
            UnityEngine.Color color = UnityEngine.Color.white;
            if (ColorConverter.Convert(stringColor, ref color))
                Element.SetAttribute<Api.Color, UnityEngine.Color>(color);
        }

        public void setBrightness(float v)
        {
            Element.SetAttribute<Color, UnityEngine.Color>(new UnityEngine.Color(v,v,v,1));
        }

        public void setColor32(UnityEngine.Color color)
        {
            Element.SetAttribute<Color, UnityEngine.Color>(color);
        }

        public void setResource(string path)
        {
            Element.SetAttribute<Resource, string>(path);
        }

        public void setText(string content)
        {
            Element.SetAttribute<Text, string>(content);
        }
    }
}