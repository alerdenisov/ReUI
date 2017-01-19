using System;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Integrations.Content
{
    public class ContentResourcesProvider : IContentProvider
    {
        public void Request<T>(string path, ContentReceiveSuccessHandler<T> onSuccess)
        {
            Request(path, onSuccess, null);
        }

        public void Request<T>(string path, ContentReceiveSuccessHandler<T> onSuccess, ContentReceiveErrorHandler onError)
        {
            try
            {
                var content = Get<T>(path);
                onSuccess(path, content);
            }
            catch(Exception e)
            {
                if (onError != null)
                    onError(path, e);
                else
                {
                    Debug.LogError($"Content {path} request failded: {e}");
                }
            }
        }

        public T Get<T>(string key) //where T : IConvertible
        {
            try
            {
                if (typeof (T) == typeof (string))
                {
                    return (T) Convert.ChangeType(Resources.Load<TextAsset>(key).text, typeof (T));
                }

                if (typeof (UnityEngine.Object).IsAssignableFrom(typeof (T)))
                {
                    return (T) Convert.ChangeType(Resources.Load(key, typeof (T)), typeof (T));

                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Content {key} request failded: {e}" );
            }

            throw new ArgumentException($"Not supported type {typeof(T)}");
        }
    }
}