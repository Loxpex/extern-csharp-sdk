using System;
using System.Diagnostics;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.Authentication.OpenId.Time;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider
{
    internal class OpenIdAuthenticationOptions
    {
        public readonly static TimeInterval DefaultInterval = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenIdAuthenticationOptions" /> class with the specified parameters.
        /// </summary>
        /// <param name="apiKey">ApiKey which will be send as the client secret to the auth server.</param>
        /// <param name="proactiveAuthTokenRefreshInterval">The interval before the current access token expires to refresh the current access token. By default equal to 5 seconds.</param>
        /// <param name="clientId">Client id which will be sent to the auth server.</param>
        public OpenIdAuthenticationOptions(string apiKey, string clientId, TimeInterval? proactiveAuthTokenRefreshInterval = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(apiKey));

            if (string.IsNullOrWhiteSpace(clientId))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(clientId));

            ApiKey = apiKey;
            ProactiveAuthTokenRefreshInterval = proactiveAuthTokenRefreshInterval ?? DefaultInterval;
            ClientId = clientId;
        }

        public TimeInterval ProactiveAuthTokenRefreshInterval { get; }
        public string ClientId { get; }
        public string ApiKey { get; }
        public string Scope => "extern.api";
    }
}