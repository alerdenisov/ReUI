using Rentitas;

namespace ReUI.Api
{
    public static class UIXmlExtensions
    {
        public static Entity<IUIPool> NewXmlUI(this Pool<IUIPool> @this, string xml)
        {
            return @this.CreateEntity().Add<XmlData>(d => d.Value = xml);
        }
    }
}