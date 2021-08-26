#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.ApiLevel.Json.SysTextJsonExtensions;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions;
using Kontur.Extern.Client.ApiLevel.Models.Documents;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Model.Docflows;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows
{
    internal class _DocflowContainingConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeof (IDocflow).IsAssignableFrom(typeToConvert) || typeof (IDocflowPageItem).IsAssignableFrom(typeToConvert);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeof (IDocflow).IsAssignableFrom(typeToConvert))
            {
                return new DocflowContainingConverter<IDocflow>();
            }

            if (typeof (IDocflowPageItem).IsAssignableFrom(typeToConvert))
            {
                return new DocflowContainingConverter<IDocflowPageItem>();
            }

            throw new InvalidOperationException();
        }

        private class DocflowContainingConverter<T> : JsonConverter<T>
        {
            private readonly Utf8String typePropName = "type";

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                if (value is IDocflow docflow)
                {
                    JsonSerializer.Serialize(writer, new SerializationDocflow<object>(docflow), options);
                }
                else if (value is IDocflowPageItem docflowPageItem)
                {
                    JsonSerializer.Serialize(writer, new SerializationDocflow<object>(docflowPageItem), options);
                }
                else
                {
                    throw Errors.UnknownSubtypeOf<IDocflowDto>(value.GetType());
                }
            }

            public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var descriptionType = GetDescriptionType(reader, typePropName);
                // todo: optimize it by caching
                var docflowType = typeof (SerializationDocflow<>).MakeGenericType(typeof (T), descriptionType);
                var serializationDocflow = (ISerializationDocflow?) JsonSerializer.Deserialize(ref reader, docflowType, options);
                return serializationDocflow == null ? default : serializationDocflow.ConvertTo<T>();

                static Type GetDescriptionType(Utf8JsonReader readerClone, in Utf8String typePropName)
                {
                    if (!readerClone.ScanToPropertyValue(typePropName.AsUtf8()))
                        throw Errors.JsonDoesNotContainProperty(typePropName.ToString());

                    var docflowTypeValue = readerClone.GetString();
                    if (docflowTypeValue == null)
                        return typeof (UnknownDescription);

                    var docflowType = new DocflowType(docflowTypeValue);
                    return DocflowDescriptionTypes.TryGetDescriptionType(docflowType) ??
                           typeof (UnknownDescription);
                }
            }

            class TypeCache
            {
                
            }

            private interface ISerializationDocflow
            {
                TResult ConvertTo<TResult>();
            }

            private class SerializationDocflow<TDescription> : ISerializationDocflow
            {
                [JsonConstructor]
                public SerializationDocflow()
                {
                }
                
                public SerializationDocflow(IDocflow docflow)
                {
                    Type = docflow.Type;
                    Status = docflow.Status;
                    SuccessState = docflow.SuccessState;
                    Links = docflow.Links;
                    Id = docflow.Id;
                    OrganizationId = docflow.OrganizationId;
                    SendDate = docflow.SendDate;
                    LastChangeDate = docflow.LastChangeDate;
                    Description = (TDescription) (object) docflow.Description;
                    Documents = docflow.Documents;
                }

                public SerializationDocflow(IDocflowPageItem docflow)
                {
                    Type = docflow.Type;
                    Status = docflow.Status;
                    SuccessState = docflow.SuccessState;
                    Links = docflow.Links;
                    Id = docflow.Id;
                    OrganizationId = docflow.OrganizationId;
                    SendDate = docflow.SendDate;
                    LastChangeDate = docflow.LastChangeDate;
                    Description = (TDescription) (object) docflow.Description;
                    Documents = null!;
                }

                public Guid Id { get; set; }
                public Guid OrganizationId { get; set; }
                public Urn Type { get; set; } = null!;
                public Urn Status { get; set; } = null!;
                public Urn SuccessState { get; set; } = null!;
                public List<Document> Documents { get; set; } = null!;
                public List<Link> Links { get; set; } = null!;
                public DateTime SendDate { get; set; }
                public DateTime? LastChangeDate { get; set; }
                public TDescription Description { get; set; } = default!;

                public TResult ConvertTo<TResult>()
                {
                    object docflow;
                    if (typeof (IDocflow).IsAssignableFrom(typeof (TResult)))
                    {
                        docflow = new Docflow(Id, OrganizationId, Type, Status, SuccessState, Documents, Links, SendDate, LastChangeDate, Description as DocflowDescription);
                    }
                    else if (typeof (IDocflowPageItem).IsAssignableFrom(typeof (TResult)))
                    {
                        docflow = new DocflowPageItem(Id, OrganizationId, Type, Status, SuccessState, Links, SendDate, LastChangeDate, Description as DocflowDescription);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                    
                    return (TResult) docflow;
                }
            }
        }
    }
}