using ReUI.Implementation.Helpers;
using Rentitas;
using Rentitas.Unity;
using ReUI.Api;
using ReUI.Implementation.LuaSystems;
using ReUI.Implementation.Systems;
using ReUI.Implementation.Systems.LuaSystems;

namespace ReUI.Implementation
{
    public class UIKernel : IKernel
    {
        private ILuaProvider _luaProvider;
        private IContentProvider _contentProvider;
        private IViewProvider _viewProvider;

        public IPool[] PoolInterfaces { get; }

        public UIKernel(IViewProvider viewProvider, IContentProvider contentProvider, ILuaProvider luaProvider)
        {
            _viewProvider = viewProvider;
            _contentProvider = contentProvider;
            _luaProvider = luaProvider;

            PoolInterfaces = new IPool[]
            {
                new UIPool(),
            };
        }

        public BaseScenario SetupScenario(Pools pools)
        {
            var uiPool = pools.Get<IUIPool>();

            return new Scenario("UI Scenario")
                .Add(uiPool.CreateSystem(new InitializeDependencySingletons(
                    _contentProvider,
                    _viewProvider,
                    _luaProvider
                )))
                .Add(CreateReadSystem(uiPool))
                .Add(CreateSetupSystems(uiPool))
                .Add(CreateLuaSystems(uiPool))
                .Add(CreateUpdateSystem(uiPool))
                .Add(CreateManageSystems(uiPool))
                ;
        }

        private ISystem CreateReadSystem(Pool<IUIPool> uiPool)
        {
            return new Scenario("Read systems")
                .Add(uiPool.CreateSystem(new ReadEmbedSystem()))
                .Add(uiPool.CreateSystem(new ReadXmlSystem()))
                .Add(uiPool.CreateSystem(new ParseXmlSystem()))
                .Add(uiPool.CreateSystem(new CreateUIViewsSystem()));
        }

        private ISystem CreateSetupSystems(Pool<IUIPool> uiPool)
        {
            return new Scenario("Setup systems")

                // Init element
                .Add(uiPool.CreateSystem(new SetupElementTypeSystem()))
                .Add(uiPool.CreateSystem(new InitialViewValueSystem()))

                // Setup attributes via xml
                .Add(uiPool.CreateSystem(new SetupElementAnchorSystem()))
                .Add(uiPool.CreateSystem(new SetupElementMarginSystem()))
                .Add(uiPool.CreateSystem(new SetupElementPivotSystem()))
                .Add(uiPool.CreateSystem(new SetupElementPositionSystem()))
                .Add(uiPool.CreateSystem(new SetupElementSizeSystem()))
                .Add(uiPool.CreateSystem(new SetupElementColorSystem()))
                .Add(uiPool.CreateSystem(new SetupElementNameSystem()))
                .Add(uiPool.CreateSystem(new SetupElementResourceSystem()))
                // image only 
                .Add(uiPool.CreateSystem(new SetupImageModeSystem()))
                // text attributes
                .Add(uiPool.CreateSystem(new SetupTextContentSystem()))
                .Add(uiPool.CreateSystem(new SetupTextFontSizeSystem()))
                .Add(uiPool.CreateSystem(new SetupTextLineHeightSystem()))
                .Add(uiPool.CreateSystem(new SetupTextAlignmentSystem()))
                ;
        }

        private ISystem CreateLuaSystems(Pool<IUIPool> uiPool)
        {
            return new Scenario("Lua Systems")
                // Inject props to scopes
                // Simple ones 
                .Add(uiPool.CreateSystem(new ExecuteLuaPropsInjectionSystem()))
                // Loops ones
                .Add(uiPool.CreateSystem(new ExecuteLuaLooperPropsInjectionSystem()))

                // Create root context
                .Add(uiPool.CreateSystem(new SetupLuaScopeSystem()))

                // Collect and setup lua code parts
                .Add(uiPool.CreateSystem(new SetupElementLuaCodeSystem()))
                .Add(uiPool.CreateSystem(new SetupEmbedPropertyInjectionSystem()))
                .Add(uiPool.CreateSystem(new SetupLoopItemSystem()))
                .Add(uiPool.CreateSystem(new SetupLoopLuaSystem()))

                // Fill lua code 
                .Add(uiPool.CreateSystem(new UpdateLuaPropertyInjectionCodeSystem()))
                .Add(uiPool.CreateSystem(new UpdateLuaOnPropsCodeSystem()))
                .Add(uiPool.CreateSystem(new UpdateLuaOnStateCodeSystem()))
                .Add(uiPool.CreateSystem(new UpdateLuaOnTickCodeSystem()))
                .Add(uiPool.CreateSystem(new UpdateLuaOnMouseOverCodeSystem()))
                .Add(uiPool.CreateSystem(new UpdateLuaOnMouseOutCodeSystem()))
                .Add(uiPool.CreateSystem(new UpdateLuaOnClickCodeSystem()))
                .Add(uiPool.CreateSystem(new UpdateLuaLooperCollectionCodeSystem()))
                .Add(uiPool.CreateSystem(new UpdateLuaLooperItterationCodeSystem()))


                // Create local context
                .Add(uiPool.CreateSystem(new SetupLuaLocalContextSystem()))
                // Inject state to local context
                .Add(uiPool.CreateSystem(new InjectScopeStateToContextSystem()))
                // Inject props to local context
                .Add(uiPool.CreateSystem(new InjectScopePropsToContextSystem()))
                // Compile code to local context
                .Add(uiPool.CreateSystem(new LuaCompileSystem()))

                // Setup delegates to invoke
                .Add(uiPool.CreateSystem(new SetupLuaLifeCycleSystem()))
                .Add(uiPool.CreateSystem(new SetupLuaLooperSystem()))
                .Add(uiPool.CreateSystem(new SetupLuaPointerSystem()))

                // Execute OnProps in scope
                .Add(uiPool.CreateSystem(new ExecuteLuaScopeOnPropsSystem()))
                .Add(uiPool.CreateSystem(new NotifyElementsStateUpdatesSystem()))

                .Add(uiPool.CreateSystem(new ExeculeLuaLooperSystem()))
                .Add(uiPool.CreateSystem(new ExecuteLuaScopeOnStateSystem()))
                .Add(uiPool.CreateSystem(new ExecuteLuaOnStateSystem()))
                .Add(uiPool.CreateSystem(new ExecuteLuaTickSystem()))

                .Add(uiPool.CreateSystem(new ExecuteLuaOnClickSystem()))
                .Add(uiPool.CreateSystem(new ExecuteLuaOnMouseOutSystem()))
                .Add(uiPool.CreateSystem(new ExecuteLuaOnMouseOverSystem()))
//
//                .Add(uiPool.CreateSystem(new UpdateLuaScopeContextSystem()))






//
//                // lua
//                .Add(uiPool.CreateSystem(new SetupScopeStateSystem()))
//                .Add(uiPool.CreateSystem(new ExecuteLuaScopePropsSystem()))
//
//                .Add(uiPool.CreateSystem(new ForceUpdatePropsSystem()))
////                .Add(uiPool.CreateSystem(new UpdateLuaPointerSystem()))
//                .Add(uiPool.CreateSystem(new ExecuteLuaPropsRequestSystem()))
//
////                .Add(uiPool.CreateSystem(new WaitRootPropertiesSystem()))
////                .Add(uiPool.CreateSystem(new UpdateRootPropertiesSystem()))
                ;
        }

        private ISystem CreateUpdateSystem(Pool<IUIPool> uiPool)
        {
            return new Scenario("Update systems")
                // Update attributes
                .Add(uiPool.CreateSystem(new UpdateElementTypeSystem()))
                .Add(uiPool.CreateSystem(new UpdateElementRectSystem()))
                .Add(uiPool.CreateSystem(new UpdateElementPositionSystem()))
                .Add(uiPool.CreateSystem(new UpdateElementColorSystem()))
                .Add(uiPool.CreateSystem(new UpdateElementNameSystem()))

                //
                //                // image only
                .Add(uiPool.CreateSystem(new UpdateSpriteResourceSystem()))
                .Add(uiPool.CreateSystem(new UpdateSpriteModeSystem()))

                // Image only
                .Add(uiPool.CreateSystem(new UpdateTextureResourceSystem()))

                // text only
                .Add(uiPool.CreateSystem(new UpdateTextResourceSystem()))
                .Add(uiPool.CreateSystem(new UpdateTextContentSystem()))
                .Add(uiPool.CreateSystem(new UpdateTextFontSizeSystem()))
                .Add(uiPool.CreateSystem(new UpdateTextLineHeightSystem()))
                .Add(uiPool.CreateSystem(new UpdateTextAlignmentSystem()))

                .Add(uiPool.CreateSystem(new UpdateElementOrderSystem()))
                ;
        }

        private ISystem CreateManageSystems(Pool<IUIPool> uiPool)
        {
            return new Scenario("Manage systems")
                .Add(uiPool.CreateSystem(new UIElementIsReadySystem()))
                .Add(uiPool.CreateSystem(new EnableReadyUIsSystem()))

                .Add(uiPool.CreateSystem(new CleanupPointerEvents()))

                .Add(uiPool.CreateSystem(new DisableAndEnableViewSystem()))
                .Add(uiPool.CreateSystem(new DestroySystem()));
        }
    }
}