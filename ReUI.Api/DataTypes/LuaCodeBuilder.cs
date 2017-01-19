using System.Collections.Generic;

namespace ReUI.Api
{
    public enum ExecutionMethod
    {
        Click,
        MouseOver,
        MouseOut,

        Tick,
        Create,
        Destroy,

        PropertyInjection,
        Props,
        State,

        LoopItteration,
        LoopCollection
    }


    public class LuaCodeBuilder
    {
        private readonly Dictionary<ExecutionMethod, string> _code;
        private string _codeCache;

        public LuaCodeBuilder()
        {
            _code = new Dictionary<ExecutionMethod, string>();
        }

        public LuaCodeBuilder Set(ExecutionMethod method, string code = null)
        {
            if (string.IsNullOrEmpty(code))
                return RemoveMethod(method);

            return AddMethod(method, code);
        }

        public void Reset()
        {
            _code.Clear();
            _codeCache = null;
        }

        public LuaCodeBuilder AddMethod(ExecutionMethod method, string code)
        {
            if(!_code.ContainsKey(method))
                _code.Add(method, string.Empty);

            _code[method] = code;

            return this;
        }

        public LuaCodeBuilder RemoveMethod(ExecutionMethod method)
        {
            if (_code.ContainsKey(method))
                _code.Remove(method);
            return this;
        }

        public override string ToString()
        {
            if (_codeCache == null)
                Build();
            return _codeCache;
        }

        private void Build()
        {
            var sb = new System.Text.StringBuilder();

            foreach (var kv in _code)
            {
                sb.AppendLine($"function {kv.Key}({SignatureFor(kv.Key)})");
                sb.AppendLine(kv.Value);
                sb.AppendLine("end");
            }

            _codeCache = sb.ToString();
        }

        private static string SignatureFor(ExecutionMethod method)
        {
            switch (method)
            {
                case ExecutionMethod.Click:
                case ExecutionMethod.MouseOut:
                case ExecutionMethod.MouseOver:
                    return "event";
                case ExecutionMethod.LoopItteration:
                    return "item, index";
                default:
                    return "";
            }
        }
    }
}