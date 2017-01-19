using System;
using System.Collections;
using System.Collections.Generic;
using Rentitas;

namespace ReUI.Api
{
    public interface ILuaProvider
    {
        ILuaEnvironment GetEnvironment();
        ILuaTable CreateContext();
        ILuaTable CreateContext(ILuaTable scope);
        object[] Execute(string code, string key, ILuaTable context);
        ILuaTable NewTable();
        ILuaTable ToTable(object obj); 
    }

    public interface ILuaEnvironment
    {
        ILuaTable GetGlobal();
        ILuaTable NewTable();
        object[] Execute(string code, string key, ILuaTable context);
    }

    public interface ILuaTable : IDisposable {
        void SetInPath(string key, object value);
        void Set(object key, object value);
        void Set(object key, ILuaTable value);
        IEnumerable GetKeys();
        IEnumerable<T> GetKeys<T>();
        object this[object key] { get; }
        void SetMetaTable(ILuaTable meta);
        object Get(object key);
        void Get<TKey, TValue>(TKey key, out TValue target);
    }
}