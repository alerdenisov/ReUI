using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Rentitas.Unity;
using ReUI.Api;
using ReUI.Implementation;
using ReUI.Integrations.Content;
using ReUI.Integrations.Lua;
using ReUI.Integrations.View;
using Application = Rentitas.Application;

namespace ReUI.Tests
{
    public class XmlConstants
    {
        public const string TEST_1_XML = @"
<Root>
</Root>
";
    }


    public class TestContentProvider : IContentProvider
    {
        public void Request<T>(string path, ContentReceiveSuccessHandler<T> onSuccess)
        {
            Request<T>(path, onSuccess, null);
        }

        public void Request<T>(string path, ContentReceiveSuccessHandler<T> onSuccess, ContentReceiveErrorHandler onError)
        {
            if (path == "test1")
                onSuccess(path, (T)(object)XmlConstants.TEST_1_XML);
        }

        public T Get<T>(string key)
        {
            if (key == "test1")
                return (T)(object)XmlConstants.TEST_1_XML;

            return default(T);
        }
    }

    public class TestApp : Application
    {
        
    }

    [TestFixture]
    public class XmlTests
    {
        public Scenario Scenario;
        public UIPool Pool;

        [SetUp]
        public void Setup()
        {
            Pool = new UIPool();

            Scenario = new Scenario();
            Scenario.Add(Pool.CreateSystem(new InitializeDependencySingletons(
                new TestContentProvider(), 
                new SimpleViewProvider(), 
                new XLuaProvider())))
                // Read xml 
                .Add(Pool.CreateSystem(new ReadEmbedSystem()))
                .Add(Pool.CreateSystem(new ReadXmlSystem()))
                .Add(Pool.CreateSystem(new ParseXmlSystem()));

            Scenario.Initialize();
        }

        [Test]
        public void embed_must_be_loaded()
        {
            var embed = Pool.CreateEntity();
            embed.Add<Embed>(e => e.Name = "test1");

            Scenario.Execute();
            Scenario.Cleanup();

            Assert.IsTrue(embed.Has<XmlData>());
            Assert.AreEqual(embed.Get<XmlData>().Value, XmlConstants.TEST_1_XML);
        }

    }
}
//public class XmlTests {
//
//	[Test]
//	public void EditorTest() {
//		//Arrange
//		var gameObject = new GameObject();
//
//		//Act
//		//Try to rename the GameObject
//		var newGameObjectName = "My game object";
//		gameObject.name = newGameObjectName;
//
//		//Assert
//		//The object has a new name
//		Assert.AreEqual(newGameObjectName, gameObject.name);
//	}
//}
