using System;
using Rentitas;
using ReUI.Api;
using ReUI.Implementation.Behaviours;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ReUI.Implementation
{
    public class CreateUIViewsSystem : ISetPool<IUIPool>, IInitializeSystem, IExecuteSystem
    {
        private Pool<IUIPool> _uiPool;
        private IViewProvider _viewPool;
        private Group<IUIPool> _elements;
        private GameObject _canvas;
        private GameObject _viewPrefab;
        private IView _canvasEntity;

        public void SetPool(Pool<IUIPool> pool)
        {
            _viewPool = pool.Get<ViewProvider>().Value;//.Get<IViewPool>();
            _uiPool = pool;

            // TODO Move to content provider!
            _viewPrefab = new GameObject("<Element />", typeof(RectTransform));//, typeof(UIElement));
            _viewPrefab.SetActive(false);
            _viewPrefab.hideFlags = HideFlags.HideAndDontSave;
        }

        public void Initialize()
        {
            _elements = _uiPool.GetGroup(Matcher.AllOf(typeof (Element)).NoneOf(typeof (ViewLink)));

            var uiRootGO = new GameObject("Canvas", typeof(RectTransform));
            var canvas = uiRootGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//            canvas.pixelPerfect = true;

            var caster = uiRootGO.AddComponent<GraphicRaycaster>();
            var scaler = uiRootGO.AddComponent<CanvasScaler>();

            _canvasEntity = _viewPool.CreateView(uiRootGO, true, false);
            _canvasEntity.SetActive(true);
        }

        public void Execute()
        {
            // TODO: Refactor this
            foreach (var entity in _elements.GetEntities())
            {
                IView view = null;
                if (entity.Has<Parent>())
                {
                    var parent = _uiPool.GetElement(entity.Get<Parent>().Id);
                    if (parent == null || !parent.Has<ViewLink>())
                        continue;

                    view = _viewPool.CreateChild(
                        parent.Get<ViewLink>().Id,
                        GetViewObject(entity.Get<Element>().Id)
                        , true, false);


                    view.SetActive(false);//Toggle<AOFG.View.Api.Disabled>(true);
                }
                else
                {
                    view = _viewPool.CreateChild(
                        _canvasEntity.Id,
                        GetViewObject(entity.Get<Element>().Id)
                        , true, false);

                    view.SetActive(false);
                }

                view.LinkTo(entity);
                entity.Add<ViewLink>(v => v.Id = view.Id);
            }
        }

        private GameObject GetViewObject(Guid id)
        {
            // TODO Pool it!
            var viewgo = GameObject.Instantiate(_viewPrefab);
//            viewgo.GetComponent<UIElement>().Setup(id);

            return viewgo;
        }
    }
}