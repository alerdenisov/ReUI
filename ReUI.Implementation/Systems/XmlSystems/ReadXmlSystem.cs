using System;
using System.Collections.Generic;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class ReadXmlSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
    {
        private Pool<IUIPool> _uiPool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var xmlEntity in entities)
            {
                Parse(xmlEntity);
            }
        }

        private void Parse(Entity<IUIPool> xmlEntity)
        {
            var xmlText = xmlEntity.Get<XmlData>().Value;
            NanoXMLDocument document = null;

            try
            {
                document = new NanoXMLDocument(xmlText);
            }
            catch(Exception e)
            {
                _uiPool.DestroyEntity(xmlEntity);
                Debug.LogError(e);
                return;
            }

            var docComponent = xmlEntity.CreateComponent<XmlDocument>();
            docComponent.Root = document.RootNode;

            xmlEntity.AddInstance(docComponent);
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (XmlData)).NoneOf(typeof (XmlDocument)).OnEntityAdded();
        public void SetPool(Pool<IUIPool> pool)
        {
            _uiPool = pool;
        }
    }
}