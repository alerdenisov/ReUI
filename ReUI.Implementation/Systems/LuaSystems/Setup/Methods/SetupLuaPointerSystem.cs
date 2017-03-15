using System;
using System.Collections.Generic;
using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReUI.Implementation.Systems
{
    public class LuaPointer : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Pool<IUIPool> _pool;
        private Guid _elementId;

        void Start()
        {
            GetComponent<UnityEngine.UI.Graphic>().raycastTarget = true;
        }

        private void CreateEvent<T>(PointerEventData data) where T : IPointerComponent, new()
        {
            var entity = _pool.CreateEntity();
            var e = entity.CreateComponent<T>();
            e.Id = _elementId;
            e.Data = data;
            entity.AddInstance(e);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CreateEvent<PointerClick>(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CreateEvent<PointerOver>(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CreateEvent<PointerOut>(eventData);
        }

        public void Setup(Pool<IUIPool> uiPool, Guid id)
        {
            _pool = uiPool;
            _elementId = id;
        }
    }
    public class SetupLuaPointerSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
    {
        private Pool<IUIPool> _uiPool;
        private IViewProvider _view;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var pointer = entity.Need<LuaCompiledPointer>();
                var view = _view.GetByIdentity(entity.Get<ViewLink>().Id);
                var p = view.RequireComponent<LuaPointer>();
                p.Setup(_uiPool, entity.Get<Element>().Id);
                var compiled = entity.GetAttribute<LuaCompiled, ILuaTable>();
                compiled.Get(ExecutionMethod.Click.ToString(),      out pointer.OnClick);
                compiled.Get(ExecutionMethod.MouseOver.ToString(),  out pointer.OnMouseOver);
                compiled.Get(ExecutionMethod.MouseOut.ToString(),   out pointer.OnMouseOut);

                entity.ReplaceInstance(pointer);
            }
        }

        public TriggerOnEvent Trigger => Matcher
            .AllOf(typeof(LuaCompiled), typeof(LuaType), typeof(ViewLink))
            .AnyOf(typeof(LuaCodeOnClick), typeof(LuaCodeOnMouseOut), typeof(LuaCodeOnMouseOver))
            .OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _uiPool = typedPool;
            _view = _uiPool.Get<ViewProvider>().Value;
        }
    }
}