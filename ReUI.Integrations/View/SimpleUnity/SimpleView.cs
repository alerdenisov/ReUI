using System;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;
using UnityEngine.UI;
using Font = UnityEngine.Font;
using Rect = UnityEngine.Rect;
using Sprite = UnityEngine.Sprite;
using Text = UnityEngine.UI.Text;
using Texture = UnityEngine.Texture;

namespace ReUI.Integrations.View
{
    public class SimpleView : MonoBehaviour, IView
    {
        private IViewProvider _manager;
        private RectTransform _cachedTransform;
        protected CachedComponent<Image> Sprite;// = new _cachedSprite;
        protected CachedComponent<RawImage> Image;
        protected CachedComponent<UnityEngine.UI.Text> Text;
        protected CachedComponent<UnityEngine.UI.InputField> Input;

        protected RectTransform RectTransform
        {
            get
            {
                if (_cachedTransform == null) _cachedTransform = GetObject().transform as RectTransform;
                return _cachedTransform;
            }
        }


        public static SimpleView Create(GameObject prefabOrInstance, bool realInstance, bool resetTransform)
        {
            GameObject instanceGo = null;
            instanceGo = !realInstance ? Instantiate(prefabOrInstance) : prefabOrInstance;

            var id = Guid.NewGuid();
            var go = instanceGo;
            go.name = "View " + id;

            go.SetActive(false);

            var instance = go.RequireComponent<SimpleView>();
            instance.Id = id;

            instance.Sprite = new CachedComponent<Image>(go);
            instance.Image = new CachedComponent<RawImage>(go);
            instance.Text = new CachedComponent<Text>(go);
            instance.Input = new CachedComponent<InputField>(go);

            return instance;
        }

        public Guid Id { get; private set; }

        public void Destroy()
        {
            if(gameObject)
                Destroy(gameObject);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetName(string value)
        {
            name = value;
        }

        public bool InScene()
        {
            return true;
        }

        public GameObject GetObject()
        {
            return gameObject;
        }

        public void SetParent(IView parentView)
        {
            var simpleView = parentView as SimpleView;
            if (simpleView == null)
            {
                throw new ArgumentException("parentView isn't SimpleView: " + parentView);
            }

            GetObject()?.transform.SetParent(simpleView.GetObject()?.transform, false);
        }

        public void LinkTo(Entity<IUIPool> entity)
        {
            Owner = entity;
        }

        public T RequireComponent<T>() where T : Component
        {
            return gameObject.RequireComponent<T>();
        }

        public void RemoveComponent<T>() where T : Component
        {
            Destroy(gameObject.GetComponent<T>());
        }

        public Entity<IUIPool> Owner { get; set; }

        public void ResetTransform()
        {
            GetObject()?.ResetTrasform();
        }

        public Rect Rect { get; set; }

        public void SetOrder(int order)
        {
            RectTransform.SetSiblingIndex(order);
        }

        public void SetPosition(Vector2 pos)
        {
            RectTransform.anchoredPosition = pos;
        }

        public void SetAnchor(Vector4 anc)
        {
            RectTransform.anchorMin = new Vector2(anc.x, anc.y);
            RectTransform.anchorMax = new Vector2(anc.z, anc.w);
        }

        public void SetOffset(Vector4 offset)
        {
            RectTransform.offsetMin = new Vector2(offset.x, offset.y);
            RectTransform.offsetMax = new Vector2(offset.z, offset.w);
        }

        public void SetPivot(Vector2 pvt)
        {
            RectTransform.pivot = pvt;
        }

        public void SetSize(Vector2 size)
        {
            RectTransform.sizeDelta = size;
        }

        public void SetSprite(Sprite sprite)
        {
            Sprite.Value.sprite = sprite;
        }

        public void SetTexture(Texture texture)
        {
            Image.Value.texture = texture;
        }

        public void SetTextAlignment(TextAnchor anchor)
        {
            Text.Value.alignment = anchor;
        }

        public void SetText(string content)
        {
            Text.Value.text = content;
        }

        public void SetFontSize(int size)
        {
            Text.Value.fontSize = size;
        }

        public void SetLineHeight(float height)
        {
            Text.Value.lineSpacing = height;
        }

        public void SetFont(Font font)
        {
            Text.Value.font = font;
        }
    }
}