using System;
using System.Linq;
using Rentitas;
using ReUI.Api;

namespace ReUI.Implementation.Systems
{
    public class SetupNestedHierarchySystem : AbstractSetupAttributeSystem, ISetPool<IUIPool>
    {
        private Pool<IUIPool> _pool;
        protected override Type[] EnsureTypes => new[] {typeof (HierarchyType), typeof(Ready) };

        protected override void SetupFor(Entity<IUIPool> entity, XmlElement xml)
        {
            var scope = _pool.GetScope(entity);
            var embed = _pool.GetParent(scope);

            var children = _pool.GetChildren(embed).Where(c => c.Has<ChildrenType>());
            if (!children.Any())
                return;

            var child = children.First();

            var parent = child.Need<Parent>();
            parent.Id = entity.Get<Element>().Id;

            child.ReplaceInstance(parent);


            // Have no sub hierarchy case
//            if (!embed.Has<XmlSubHierarchy>())
//                return;

//            var root = embed.Get<XmlSubHierarchy>().Root;
//            var doc = entity.CreateComponent<XmlDocument>();
//            doc.Root = root;
//            entity.AddInstance(doc);
        }

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _pool = typedPool;
        }
    }
}