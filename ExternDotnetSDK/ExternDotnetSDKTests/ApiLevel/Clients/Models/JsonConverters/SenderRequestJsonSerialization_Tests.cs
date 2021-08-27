using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.Http.Serialization;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.JsonConverters
{
    [TestFixture]
    internal class SenderRequestJsonSerialization_Tests
    {
        private const string JsonWithoutCert = @"{
  ""inn"": ""3077668269"",
  ""kpp"": ""561650781"",
  ""is-representative"": true,
  ""ipaddress"": ""8.8.8.8""
}";
        private IJsonSerializer serializer;

        [SetUp]
        public void SetUp() => serializer = JsonSerializerFactory.CreateJsonSerializer();

        [Test]
        public void Should_serialize_sender_request_to_json()
        {
            var json = serializer.SerializeToIndentedString(CreateSenderRequestWithoutCert());

            json.Should().Be(JsonWithoutCert);
        }

        private static SenderRequest CreateSenderRequestWithoutCert()
        {
            return new SenderRequest
            {
                Inn = "3077668269",
                Kpp = "561650781",
                IsRepresentative = true,
                IpAddress = "8.8.8.8"
            };
        }
    }
}