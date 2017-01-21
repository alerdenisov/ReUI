using System;

namespace ReUI.Api
{
    public struct ContentReceiveResult<T>
    {
        public bool Error;
        public string ErrorMessage;
        public T Data;
    }

    public delegate void ContentReceiveHandler<T>(string path, ContentReceiveResult<T> onResult);

    public interface IContentProvider
    {
        void RequestXml(string key, ContentReceiveHandler<string> onResult);
        void RequestSprite(string key, ContentReceiveHandler<UnityEngine.Sprite> onResult);
        void RequestFont(string key, ContentReceiveHandler<UnityEngine.Font> onResult);
        void RequestAudio(string key, ContentReceiveHandler<UnityEngine.AudioClip> onResult);
        void RequestTexture(string key, ContentReceiveHandler<UnityEngine.Texture> onResult);
    }
}