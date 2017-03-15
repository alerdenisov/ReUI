using System;
using Rentitas;
using UnityEngine;

namespace ReUI.Api
{
    public interface IViewProvider
    {
        IView GetByIdentity(Guid id);
        IView CreateView(GameObject prefabOrInstance, bool realInstance, bool resetTransform);
        IView CreateChild(Guid parentId, GameObject prefabOrInstance, bool realInstance, bool resetTransform);
    }

    public interface IView
    {
        Guid Id { get; }

        void Destroy();
        void SetActive(bool active);
        void SetName(string value);

        bool InScene();
        // GameObject GetObject();

        void SetParent(IView parentView);
        void LinkTo(Entity<IUIPool> entity);

        T RequireComponent<T>() where T : UnityEngine.Component;
        void RemoveComponent<T>() where T : UnityEngine.Component;

        void ResetTransform();

        Rect Rect { get; set; }

        void SetOrder(int order);
        void SetPosition(Vector2 pos);
        void SetAnchor(Vector4 anc);
        void SetOffset(Vector4 offset);
        void SetPivot(Vector2 pvt);
        void SetSize(Vector2 size);

        void SetSprite(UnityEngine.Sprite sprite);

        void SetTexture(UnityEngine.Texture texture);

        #region Text attributes
        void SetTextAlignment(UnityEngine.TextAnchor anchor);
        void SetText(string content);
        void SetFontSize(int size);
        void SetLineHeight(float height);
        void SetFont(UnityEngine.Font height);
        #endregion
    }
}