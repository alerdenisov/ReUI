using System;

namespace ReUI.Api
{
    public struct ContentReceiveResult<T>
    {
        public bool IsError => !string.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage { get; private set; }
        public T Data { get; private set; }
        
        public static ContentReceiveResult<T> Error(string message = null)
        {
            if (message == null)
                message = "Unknown error";

            var result = new ContentReceiveResult<T>();
            result.ErrorMessage = message;

            return result;
        }

        public static ContentReceiveResult<T> Success(T data)
        {
            var result = new ContentReceiveResult<T>();
            result.ErrorMessage = null;
            result.Data = data;

            return result;

        }
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