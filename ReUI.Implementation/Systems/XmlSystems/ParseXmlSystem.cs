using System;
using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class ParseXmlSystem : IReactiveSystem<IUIPool>,ISetPool<IUIPool>,ICleanupSystem
    {
        private Pool<IUIPool> _uiPool;
        private Group<IUIPool> _xmlGroup;

        public enum ExtraElementNode
        {
            Content,
            OnCreate,
            OnDestroy,
            OnTick,
            OnProps,
            OnState,
            OnMouseOver,
            OnMouseOut,
            OnMouseRelease,
            OnMousePress,
            OnClick,

            Collection,
            Itteration,

            Property,
//            Children,
        }

        public static Dictionary<string, ExtraElementNode> _extraElementNodesDictionary = new Dictionary<string, ExtraElementNode>
        {
            { "Content", ExtraElementNode.Content },
            { "OnCreate", ExtraElementNode.OnCreate },
            { "OnDestroy", ExtraElementNode.OnDestroy },
            { "OnTick", ExtraElementNode.OnTick },
            { "OnProps", ExtraElementNode.OnProps },
            { "OnState", ExtraElementNode.OnState },
            { "OnMouseOver", ExtraElementNode.OnMouseOver },
            { "OnMouseOut", ExtraElementNode.OnMouseOut },
            { "OnMousePress", ExtraElementNode.OnMousePress },
            { "OnMouseRelease", ExtraElementNode.OnMouseRelease},
            { "OnClick", ExtraElementNode.OnClick},
            { "Collection", ExtraElementNode.Collection},
            { "Itteration", ExtraElementNode.Itteration},
            { "Props", ExtraElementNode.Property},
//            { "Children", ExtraElementNode.Children},

        };

        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var xmlDocument in entities)
            {
                ParseDocument(xmlDocument, xmlDocument.Get<XmlDocument>().Root);
            }
        }

        private void ParseDocument(Entity<IUIPool> embedEntity, IXmlNode root)
        {
            if (root.Name != "Root" && root.Name != "Children")
            {
                Debug.LogError("Incorrect xml. Each ui xml must starts with Root node");
                return;
            }

            var rootEntity = embedEntity.Has<Element>() ? _uiPool.CreateChild(embedEntity.Get<Element>().Id) : _uiPool.CreateRoot();

            FillElement(rootEntity, rootEntity, root);
            ParseRecursive(rootEntity, rootEntity, root.SubNodes);
        }

        private void ParseRecursive(Entity<IUIPool> root, Entity<IUIPool> parent, IEnumerable<IXmlNode> nodes)
        {
            var parentId = parent.Get<Element>().Id;
            var order = 0;
            foreach (var node in nodes)
            {
                if(_extraElementNodesDictionary.ContainsKey(node.Name))
                    continue;

                var child = _uiPool.CreateChild(parentId, order++);
                FillElement(root, child, node);

                ParseRecursive(root, child, node.SubNodes);
            }
        }

        private void FillElement(Entity<IUIPool> root, Entity<IUIPool> element, IXmlNode node)
        {
            element.Add<Scope>(s => s.Id = root.Get<Element>().Id);

            var xmlElement = element.CreateComponent<XmlElement>();
            xmlElement.Name = node.Name;
            xmlElement.Attributes = node.Attributes.ToDictionary(a => a.Name, a => a.Value);
            xmlElement.Content = node.Value;

            foreach (var extraNode in node.SubNodes.Where(child => _extraElementNodesDictionary.ContainsKey(child.Name))
                )
            {
                var extraType = _extraElementNodesDictionary[extraNode.Name];
                var value = extraNode.Value;
                if (string.IsNullOrEmpty(value))
                    value = extraNode.Attributes.FirstOrDefault(a => a.Name == "Value")?.Value;
                xmlElement.Attributes.Add(extraNode.Name, value);
            }

            element.AddInstance(xmlElement);
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (XmlDocument)).OnEntityAdded();

        public void SetPool(Pool<IUIPool> pool)
        {
            _uiPool = pool;
            _xmlGroup = _uiPool.GetGroup(Matcher.AllOf(typeof (XmlDocument)));
        }

        public void Cleanup()
        {
//            foreach (var xml in _xmlGroup.GetEntities())
//            {
//                _uiPool.DestroyEntity(xml);
//            }
        }
    }
}