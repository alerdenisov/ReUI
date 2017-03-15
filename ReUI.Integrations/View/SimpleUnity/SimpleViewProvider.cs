using System;
using System.Collections.Generic;
using ReUI.Api;
using Uniful;
using UnityEngine;

namespace ReUI.Integrations.View
{

    public class SimpleViewProvider : IViewProvider
    {
        private readonly Dictionary<Guid, SimpleView> _views = new Dictionary<Guid, SimpleView>();

        public IView GetByIdentity(Guid id)
        {
            SimpleView view = null;
            return _views.TryGetValue(id, out view) ? view : null;
        }

        public IView CreateView(GameObject prefabOrInstance, bool realInstance, bool resetTransform)
        {
            var view = SimpleView.Create(prefabOrInstance, realInstance, resetTransform);
            _views.Add(view.Id, view);
            return view;
        }

        public IView CreateChild(Guid parentId, GameObject prefabOrInstance, bool realInstance, bool resetTransform)
        {
            var parent = _views[parentId];
            var view = CreateView(prefabOrInstance, realInstance, resetTransform);

            view.SetParent(parent);//.GetObject().transform.SetParent(parent.GetObject().transform, false);

            if (resetTransform)
            {
                view.ResetTransform();
            }
            return view;
        }
    }
}