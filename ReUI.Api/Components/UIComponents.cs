using System;
using Rentitas;

namespace ReUI.Api
{
    public class Destroy : IViewComponent, IFlag { }
    public class Ready : IViewComponent, IFlag { }
    public class Disabled : IViewComponent, IFlag { }

    public class Element : IViewComponent
    {
        public Guid Id;
    }

    public class Scope : IViewComponent
    {
        public Guid Id;
    }

    public class Parent : IViewComponent
    {
        public Guid Id;
    }

    public class Order : IViewComponent, IAttributeValue<int>
    {
        public int Value { get; set; }
    }

    public class Root : IViewComponent, IFlag { }

    public class Embed : IViewComponent
    {
        public string Name;
    }
}