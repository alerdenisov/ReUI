using System;
using Rentitas;

namespace ReUI.Api
{
    public interface IUIPool : IComponent {}

    public class UIPool : Pool<IUIPool>
    {
        public PrimaryEntityIndex<IUIPool, Guid> Index { get; private set; }
        public EntityIndex<IUIPool, Guid> Children { get; private set; }
        public EntityIndex<IUIPool, Guid> Scope { get; private set; }

        public UIPool() : base(RentitasUtility.CollectComponents<IUIPool>().Build())
        {
            Index = new PrimaryEntityIndex<IUIPool, Guid>(
                GetGroup(Matcher.AllOf(typeof (Element))),
                (e, c) => ((Element) c).Id);

            Children = new EntityIndex<IUIPool, Guid>(
                GetGroup(Matcher.AllOf(typeof (Parent))),
                (e, c) => ((Parent) c).Id);

            Scope = new EntityIndex<IUIPool, Guid>(
                GetGroup(Matcher.AllOf(typeof(Scope))),
                (e, c) => ((Scope)c).Id);
        }
    }
}