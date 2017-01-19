using System;
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
        GameObject GetObject();
    }
}