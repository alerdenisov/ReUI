using System;
using UnityEngine.EventSystems;

namespace ReUI.Api
{
    public class PointerClick : IPointerComponent
    {
        public Guid Id { get; set; }
        public PointerEventData Data { get; set; }
    }

    public class PointerOver : IPointerComponent
    {
        public Guid Id { get; set; }
        public PointerEventData Data { get; set; }
    }

    public class PointerOut : IPointerComponent
    {
        public Guid Id { get; set; }
        public PointerEventData Data { get; set; }
    }

    public class PointerPress : IPointerComponent
    {
        public Guid Id { get; set; }
        public PointerEventData Data { get; set; }
    }

    public class PointerRelease : IPointerComponent
    {
        public Guid Id { get; set; }
        public PointerEventData Data { get; set; }
    }
}