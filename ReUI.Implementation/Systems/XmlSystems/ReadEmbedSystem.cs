using System;
using System.Collections.Generic;
using System.IO;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class ReadEmbedSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
    {
        private IContentProvider _contentProvider;

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var entity in entities)
            {
                var path = entity.Get<Embed>().Name;

                // TODO: Move to pool object (maybe special pool?)
                _contentProvider.Request(path, delegate(string k, string content)
                {
                    entity.Add<XmlData>(doc => doc.Value = content);
                }, delegate(string p, Exception exception)
                {
                    Debug.LogError($"Error on request content at {p}: " + exception);
                });
            }
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (Embed)).OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _contentProvider = typedPool.Get<ContentProvider>().Value;
        }
    }
}