using System;
using System.Collections.Generic;
using Rentitas;

// ReSharper disable InconsistentNaming
namespace ReUI.Api
{
    public static class UIExtensions
    {
        public static Entity<IUIPool> GetElement(this Pool<IUIPool> pool, Guid id)
        {
            return ((UIPool) pool).Index.TryGetEntity(id);
        }

        public static HashSet<Entity<IUIPool>> GetElementsInScope(this Pool<IUIPool> pool, Guid scopeId)
        {
            return ((UIPool) pool).Scope.GetEntities(scopeId);
        }

        public static Entity<IUIPool> GetScope(this Pool<IUIPool> pool, Entity<IUIPool> element)
        {
            if (element == null || !element.Has<Scope>())
                return null;
            return pool.GetScope(element.Get<Scope>().Id);
        }

        public static Entity<IUIPool> GetScope(this Pool<IUIPool> pool, Guid scopeId)
        {
            return pool.GetElement(scopeId);
        }

        public static HashSet<Entity<IUIPool>> GetChildren(this Pool<IUIPool> pool, Entity<IUIPool> parent)
        {
            return pool.GetChildren(parent.Get<Element>().Id);
        }

        public static HashSet<Entity<IUIPool>> GetChildren(this Pool<IUIPool> pool, Guid parentId)
        {
            return ((UIPool) pool).Children.GetEntities(parentId);
        }

        public static Entity<IUIPool> GetParent(this Pool<IUIPool> pool, Entity<IUIPool> child)
        {
            return pool.GetElement(child.Get<Parent>().Id);
        }

        public static TValue GetAttribute<TAttribute, TValue>(this Entity<IUIPool> @this)
            where TAttribute : IUIPool, IAttributeValue<TValue>
        {
            if (@this == null || !@this.Has<TAttribute>())
                return default(TValue);

            return @this.Get<TAttribute>().Value;
        }

        public static void SetAttribute<TAttribute, TValue>(this Entity<IUIPool> @this, TValue value)
            where TAttribute : IUIPool, IAttributeValue<TValue>, new()
        {
            var component = @this.Need<TAttribute>();
            component.Value = value;
            @this.ReplaceInstance(component);
        }

        public static void SetupCode(this Entity<IUIPool> @this, ExecutionMethod method, string code)
        {
            var codeComponent = @this.Need<LuaCode>();
            codeComponent.Value.Set(method, code);
            @this.ReplaceInstance(codeComponent);
        }
    }
}