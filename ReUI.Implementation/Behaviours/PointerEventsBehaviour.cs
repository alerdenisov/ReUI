using Rentitas;
using ReUI.Api;
using Uniful;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReUI.Implementation.Behaviours
{
    public class PointerEventsBehaviour : MonoBehaviour, 
        IPointerEnterHandler, 
        IPointerExitHandler, 
        IPointerClickHandler, 
        IDragHandler
    {
        private bool _createViaStatic;

        public Pool<IUIPool> Pool { get; private set; }
        public Entity<IUIPool> Element { get; private set; }

        void Start()
        {
            if (!_createViaStatic)
            {
                Debug.LogError("Don't create PointerEventsBehaviour directly. Please use PointerEventsBehaviour.Create method");
                Destroy(this);
                return;
            }
        }

        public static void Create(Entity<IUIPool> element, Pool<IUIPool> pool)
        {
            var viewId = element.Get<ViewLink>().Id;
            var view = pool.Get<ViewProvider>().Value.GetByIdentity(viewId);

            var beh = view.GetObject().RequireComponent<PointerEventsBehaviour>();
            beh._createViaStatic = true;

            beh.Pool = pool;
            beh.Element = element;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}