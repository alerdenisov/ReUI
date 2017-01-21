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

                _contentProvider.RequestXml(path, delegate(string k, ContentReceiveResult<string> result)
                {
                    if (!result.IsError)
                        entity.Add<XmlData>(doc => doc.Value = result.Data);
                    else
                        Debug.LogError(result.ErrorMessage);
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