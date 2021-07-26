using FluentAssertions;
using Kontur.Extern.Client.Auth.OpenId.Provider;
using NUnit.Framework;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;

namespace Kontur.Extern.Client.Tests.Authentication.OpenId
{
    [TestFixture]
    internal class OpenIdAuthenticationResult_Tests
    {
        [Test]
        public void Should_set_auth_header_to_the_request()
        {
            var authResult = new OpenIdAuthenticationResult("access-token", 1.Seconds());

            var request = authResult.Apply(Request.Get("/some-url"));

            var authorizationHeader = request.Headers?.Authorization;
            authorizationHeader?.Should().Be("Bearer access-token");
        }
    }
}