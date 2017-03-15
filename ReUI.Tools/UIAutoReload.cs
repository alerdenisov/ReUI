using System.Linq;
using Rentitas.Unity.VisualDebugging;
using ReUI.Api;
using ReUI.Implementation;
using ReUI.Integrations.Content;
using ReUI.Integrations.Lua;
using ReUI.Integrations.View;
using UnityEngine;

namespace ReUI.Tools
{
    public class UIAutoReload : MonoBehaviour, IAppProvider {
        #region Editor Fields
        public string DefaultView = "SimpleUI";
        #endregion

        private AutoReloadApp _application;
        private bool _create;
        private bool _reloaded;
        public Rentitas.Application Application => _application;

        private void Start()
        {
            // create instance of rentitas application
            _application = new AutoReloadApp(
                new SimpleViewProvider(),
                new ContentResourcesProvider(),
                new XLuaProvider());

            var _uiPool = _application.Pools.Get<IUIPool>();
            _uiPool.CreateEmbed(DefaultView);
        }

        public void Update()
        {
            // execute instance of rentitas application on each update
            _application.Execute();
            if (_create)
            {
                var testUI = _application.Pools.Get<IUIPool>().CreateEntity();
                testUI.Add<Embed>(e => e.Name = DefaultView);
                _create = false;
            }

            if (Input.GetKeyUp(KeyCode.F5))
                Reload();

        }

        private void Reload()
        {
            foreach (var entity in _application.Pools.Get<IUIPool>().GetEntities().Where(e => e.Has<Element>() || e.Has<Embed>()))
            {
                entity.Toggle<Destroy>(true);
                _reloaded = true;
            }

            _create = true;
        }
    }

    public class AutoReloadApp : Rentitas.Application
    {
        public AutoReloadApp(
            IViewProvider viewProvider,
            IContentProvider contentProvider,
            ILuaProvider luaProvider)
        {
            RegisterKernel(new UIKernel(viewProvider, contentProvider, luaProvider));
        }
    }
}
