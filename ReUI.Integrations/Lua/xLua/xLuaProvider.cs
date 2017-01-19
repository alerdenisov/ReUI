using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReUI.Api;
using UnityEngine;
using XLua;

namespace ReUI.Integrations.Lua
{
    public class InputHelper
    {
        public Vector2 mousePosition => Input.mousePosition;
    }

    public class XLuaEnv : ILuaEnvironment
    {
        internal LuaEnv Environment { get; }

        private ILuaTable _cachedGlobal;

        public ILuaTable GetGlobal()
        {
            return _cachedGlobal ?? (_cachedGlobal = XLuaTable.Create(Environment.Global));
        }

        public XLuaEnv(LuaEnv env)
        {

            Environment = env;
            env.Global.Set("log", (Action<object>)Debug.Log);
            env.Global.Set("getTime", (Func<float>)(() => Time.time));
            env.Global.Set("input", new InputHelper());
            env.Global.Set("hslColor", (Func<float, string>)((hue) =>
            {
                hue /= 255f;
                var color = UnityEngine.Color.HSVToRGB(hue, 0.8f, 0.7f);
                var r = Mathf.FloorToInt(byte.MaxValue*color.r);
                var g = Mathf.FloorToInt(byte.MaxValue*color.g);
                var b = Mathf.FloorToInt(byte.MaxValue*color.b);
                return $"#{r:X}{g:X}{b:X}";
            }));
        }

        public ILuaTable ToLuaTable(object o)
        {
            return XLuaTable.Create((LuaTable)o);
        }

        public ILuaTable NewTable()
        {
            return XLuaTable.Create(Environment.NewTable());
        }

        public object[] Execute(string code, string key, ILuaTable context)
        {
            return Environment.DoString(code, key, (context as XLuaTable).SourceTable);
        }

        public object[] DoString(string code, string key, ILuaTable context)
        {
            return Environment.DoString(code, key, ((XLuaTable) context).SourceTable);
        }
    }

    public class XLuaTable : ILuaTable
    {
        private static readonly Stack<XLuaTable> _xLuaTablesCache = new Stack<XLuaTable>();

        internal LuaTable SourceTable { get; private set; }

        public override string ToString()
        {
            return "Xtable: " + SourceTable?.ToString();
        }

        private  XLuaTable(LuaTable table)
        {
            SourceTable = table;
        }

        ~XLuaTable()
        {
            Dispose();
        }

        public static XLuaTable Create(LuaTable table)
        {
//            if (_xLuaTablesCache.Count > 0)
//                return _xLuaTablesCache.Pop().Setup(table);
//            else
                return new XLuaTable(table);
        }

        private XLuaTable Setup(LuaTable table)
        {
            SourceTable = table;
            return this;
        }

        public void SetInPath(string key, object value)
        {
            SourceTable.SetInPath(key.ToString(), value);
        }

        public void Set(object key, object value)
        {
            SourceTable.Set(key, value);
        }

        public void Set(object key, ILuaTable value)
        {
            SourceTable.Set(key, (value as XLuaTable)?.SourceTable);
        }

        public IEnumerable GetKeys()
        {
            return SourceTable.GetKeys();
        }
        public IEnumerable<T> GetKeys<T>()
        {
            return SourceTable.GetKeys<T>();
        }

        public object this[object key] => SourceTable[key];

        public void SetMetaTable(ILuaTable meta)
        {
            SourceTable.SetMetaTable((meta as XLuaTable).SourceTable);
        }

        public object Get(object key)
        {
            throw new NotImplementedException();
        }

        public void Get<TKey, TValue>(TKey key, out TValue target)
        {
            SourceTable.Get(key, out target);
        }

        public void Dispose()
        {
            SourceTable.Dispose();
            _xLuaTablesCache.Push(this);
        }
    }

    public class XLuaProvider : ILuaProvider
    {
        private XLuaEnv _env;

        public XLuaProvider()
        {
            _env = new XLuaEnv(new LuaEnv());
        }

        public ILuaEnvironment GetEnvironment()
        {
            return _env;
        }

        public ILuaTable CreateContext()
        {
            return CreateContext(null);
        }

        public ILuaTable CreateContext(ILuaTable scope)
        {
            var env = _env;
            var tbl = env.NewTable();

            ILuaTable meta = NewTable();
            meta.Set("__index", scope != null ? (scope as XLuaTable).SourceTable : env.Environment.Global);
            tbl.SetMetaTable(meta);
            meta.Dispose();

            return tbl;
        }

        public object[] Execute(string code, string key, ILuaTable context)
        {
            return _env.DoString(code, key, context);
        }

        public ILuaTable NewTable()
        {
            return _env.NewTable();
        }

        public ILuaTable ToTable(object obj)
        {
            return _env.ToLuaTable(obj);
        }
    }
}
