using System;

namespace ReUI.Api
{
    public delegate void ContentReceiveSuccessHandler<T>(string path, T content);
    public delegate void ContentReceiveErrorHandler(string path, Exception e);

    public interface IContentProvider {
        void Request<T>(string path, ContentReceiveSuccessHandler<T> onSuccess);
        void Request<T>(string path, ContentReceiveSuccessHandler<T> onSuccess, ContentReceiveErrorHandler onError);
        T Get<T>(string key);
    }

    public interface IContent { }
}