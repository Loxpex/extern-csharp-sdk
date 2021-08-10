using System.IO;
using Kontur.Extern.Client.Http.Constants;
using Kontur.Extern.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http.Contents
{
    public static class ObjectJsonContent
    {
        public static ObjectJsonContent<T> WithMessage<T>(T message) => new(message);
    }

    public class ObjectJsonContent<T> : IHttpContent    
    {
        private readonly T message;

        public ObjectJsonContent(T message) => this.message = message;

        public long? Length => null;

        public Request Apply(Request request, IJsonSerializer serializer)
        {
            var memoryStream = new MemoryStream();
            serializer.SerializeToJsonStream(message, memoryStream);
            memoryStream.Position = 0;
            return request.WithContent(new StreamContent(memoryStream)).WithContentTypeHeader(ContentTypes.Json);
        }
    }
}