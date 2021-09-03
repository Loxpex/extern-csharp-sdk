#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents.Requisites;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.JsonConverters
{
    [TestFixture]
    internal class DocflowDocumentDescriptionConverter_Tests
    {
        private static IJsonSerializer serializer = null!;
        private DocflowDocumentDescriptionGenerator descriptionGenerator = null!;
            
        [SetUp]
        public void SetUp()
        {
            serializer = JsonSerializerFactory.CreateJsonSerializer(ignoreNullValues: false);
            descriptionGenerator = new DocflowDocumentDescriptionGenerator();
        }
        
        [TestCaseSource(nameof(DocumentTypeToRequisitesCases))]
        public void Should_deserialize_document_description_with_requisites_by_it_document_type(RequisitesCase requisitesCase)
        {
            var (json, expectedDescription) = requisitesCase.GenerateDescription(serializer, descriptionGenerator);
            Console.WriteLine($"Generated JSON: {json}");
            
            var description = serializer.Deserialize<DocflowDocumentDescription>(json);

            description.Type.Should().Be(requisitesCase.DocumentType.ToUrn());
            DescriptionShouldBeEqual(description, expectedDescription);
        }

        [Test]
        public void Should_deserialize_common_requisites_in_case_of_unknown_document_type()
        {
            var unknownDocumentType = new DocumentType(DocumentType.Namespace.Append("unknown-document"));
            var documentDescription = descriptionGenerator.GenerateWithoutRequisites(unknownDocumentType);
            documentDescription.Requisites = new CommonDocflowDocumentRequisites();
            var json = serializer.SerializeToIndentedString(documentDescription);
            Console.WriteLine($"Generated JSON: {json}");

            var description = serializer.Deserialize<DocflowDocumentDescription>(json);

            DescriptionShouldBeEqual(description, documentDescription);
        }

        [Test]
        public void Should_deserialize_common_requisites_in_case_of_null_document_type()
        {
            var dummyDocumentType = DocumentType.Fns.Fns534.Report;
            var documentDescription = descriptionGenerator.GenerateWithoutRequisites(dummyDocumentType);
            documentDescription.Type = null;
            documentDescription.Requisites = new CommonDocflowDocumentRequisites();
            var json = serializer.SerializeToIndentedString(documentDescription);
            Console.WriteLine($"Generated JSON: {json}");

            var description = serializer.Deserialize<DocflowDocumentDescription>(json);

            DescriptionShouldBeEqual(description, documentDescription);
        }

        private static void DescriptionShouldBeEqual(DocflowDocumentDescription description, DocflowDocumentDescription expectedDescription)
        {
            description.Should().BeEquivalentTo(expectedDescription);
            description.Requisites.Should().BeOfType(expectedDescription.Requisites.GetType());
        }

        public static IEnumerable<RequisitesCase> DocumentTypeToRequisitesCases =>
            EnumLikeType
                .AllEnumValuesFromNestedTypesOfStruct<DocumentType>()
                .Select(dt => new RequisitesCase(dt, DocumentDescriptionRequisitesTypes.GetRequisiteType(dt)));
        
        public record RequisitesCase
        {
            public RequisitesCase(DocumentType documentType, Type? requisitesType)
            {
                DocumentType = documentType;
                RequisitesType = requisitesType;
            }

            public (string json, DocflowDocumentDescription expectedDescription) GenerateDescription(IJsonSerializer serializer, DocflowDocumentDescriptionGenerator descriptionGenerator)
            {
                var docflow = descriptionGenerator.GenerateWithRequisites(RequisitesType, DocumentType);
                var json = serializer.SerializeToIndentedString(docflow);
                return (json, docflow);
            }

            public DocumentType DocumentType { get; }
            public Type? RequisitesType { get; }

            public override string ToString() => $"{DocumentType} -> {RequisitesType?.Name ?? "<null>"}";
        }
    }
}