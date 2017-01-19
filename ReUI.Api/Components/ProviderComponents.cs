using Rentitas;

namespace ReUI.Api
{
    public class ViewProvider : IAttributeValue<IViewProvider>, ISingleton, IUIPool
    {
        public IViewProvider Value { get; set; }
    }

    public class LuaProvider : IAttributeValue<ILuaProvider>, ISingleton, IUIPool
    {
        public ILuaProvider Value { get; set; }
    }

    public class ContentProvider : IAttributeValue<IContentProvider>, ISingleton, IUIPool
    {
        public IContentProvider Value    { get; set; }
    }
}