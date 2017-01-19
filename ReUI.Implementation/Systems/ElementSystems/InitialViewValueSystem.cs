using System.Collections.Generic;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation
{
    public class InitialViewValueSystem : IReactiveSystem<IUIPool>
    {
        public void Execute(List<Entity<IUIPool>> entities)
        {
            foreach (var element in entities)
            {
                InitialFor(element);
            }
        }

        private void InitialFor(Entity<IUIPool> element)
        {
            var isRoot = element.Has<ScopeType>();
            InitialSize(element, isRoot);
            InitialPosition(element, isRoot);
            InitialAnchor(element, isRoot);
            InitialPivot(element, isRoot);
            InitialMargin(element, isRoot);
        }

        private void InitialMargin(Entity<IUIPool> element, bool isRoot)
        {
            if (element.Has<Margin>()) return;
            var margin = element.CreateComponent<Margin>();
            margin.Value = new Vector4(0, 0, 0, 0);
            element.AddInstance(margin);
        }

        private void InitialPivot(Entity<IUIPool> element, bool isRoot)
        {
            if (element.Has<Pivot>()) return;
            var pivot = element.CreateComponent<Pivot>();
            pivot.Value = isRoot ? new Vector2(0.5f, 0.5f) : new Vector2(0, 1);
            element.AddInstance(pivot);
        }

        private void InitialAnchor(Entity<IUIPool> element, bool isRoot)
        {
            if (element.Has<Anchor>()) return;
            var anchor = element.CreateComponent<Anchor>();
            anchor.Value = isRoot ? new Vector4(0f, 0f, 1f, 1f) : new Vector4(0f, 1f, 0f, 1f);
            element.AddInstance(anchor);
        }

        private void InitialPosition(Entity<IUIPool> element, bool isRoot)
        {
            if (element.Has<Position>()) return;
            var position = element.CreateComponent<Position>();
            position.Value = new Vector2(0, 0);
            element.AddInstance(position);
        }

        private void InitialSize(Entity<IUIPool> element, bool isRoot)
        {
            if (element.Has<Size>()) return;
            var size = element.CreateComponent<Size>();
            size.Value = new Vector2(0, 0);
            element.AddInstance(size);
        }

        public TriggerOnEvent Trigger => Matcher.AllOf(typeof (Element)).OnEntityAdded();
    }
}