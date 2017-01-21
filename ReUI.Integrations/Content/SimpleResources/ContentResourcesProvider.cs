using System;
using ReUI.Api;
using UnityEngine;
using Font = UnityEngine.Font;
using Sprite = UnityEngine.Sprite;
using Texture = UnityEngine.Texture;

namespace ReUI.Integrations.Content
{
    public class ContentResourcesProvider : IContentProvider
    {
        public void RequestXml(string key, ContentReceiveHandler<string> onResult)
        {
            var xmlAsset = Resources.Load<TextAsset>(key);
            onResult(key, ContentReceiveResult<string>.Success(xmlAsset.text));
        }

        private void GenericRequest<T>(string key, ContentReceiveHandler<T> onResult) where T : UnityEngine.Object
        {
            var asset = Resources.Load<T>(key);
            if (asset == null)
                onResult(key, ContentReceiveResult<T>.Error($"Content ({key}) not found"));
            else
                onResult(key, ContentReceiveResult<T>.Success(asset));
        }

        public void RequestSprite(string key, ContentReceiveHandler<Sprite> onResult)
        {
            GenericRequest(key, onResult);
        }

        public void RequestFont(string key, ContentReceiveHandler<Font> onResult)
        {
            GenericRequest(key, onResult);
        }

        public void RequestAudio(string key, ContentReceiveHandler<AudioClip> onResult)
        {
            GenericRequest(key, onResult);
        }

        public void RequestTexture(string key, ContentReceiveHandler<Texture> onResult)
        {
            GenericRequest(key, onResult);
        }
    }
}