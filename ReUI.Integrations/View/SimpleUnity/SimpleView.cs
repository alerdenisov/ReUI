using System;
using ReUI.Api;
using Uniful;
using UnityEngine;

namespace ReUI.Integrations.View
{
    public class SimpleView : MonoBehaviour, IView
    {
        private IViewProvider _manager;

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
            GetObject()?.transform.SetParent(parentView.GetObject()?.transform, false);
        }
    }
}