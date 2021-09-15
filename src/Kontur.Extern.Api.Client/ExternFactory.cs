using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Model.Configuration;
using Kontur.Extern.Api.Client.Models.ApiErrors;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives.Polling;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Auth.OpenId.Builder;
using Kontur.Extern.Api.Client.Authentication;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Api.Client.Http.Configurations;
using Kontur.Extern.Api.Client.Http.Options;
using Kontur.Extern.Api.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client
{
    internal class ExternFactory
    {
        private static IPollingStrategy DefaultDelayPollingStrategy => new ConstantDelayPollingStrategy(5.Seconds());
        private static ICrypt DefaultCryptoProvider => new WinApiCrypt();
        
        public bool EnableUnauthorizedFailover { get; set; }
        
        public IExtern Create(
            ContentManagementOptions? contentManagementOptions,
            IHttpClientConfiguration clientConfiguration, 
            IPollingStrategy? pollingStrategy, 
            ICrypt? cryptoProvider, 
            RequestTimeouts? requestTimeouts, 
            OpenIdSetup? openIdSetup, 
            ILog log)
        {
            contentManagementOptions ??= ContentManagementOptions.Default;
            pollingStrategy ??= DefaultDelayPollingStrategy;
            cryptoProvider ??= DefaultCryptoProvider;
            requestTimeouts ??=  new RequestTimeouts();
            
            var authProvider = CreateAuthProvider(openIdSetup, log);
            var jsonSerializer = JsonSerializerFactory.CreateJsonSerializer();
            var http = CreateHttp(clientConfiguration, requestTimeouts, authProvider, jsonSerializer, log);
            var api = new KeApiClient(http);
            var services = new ExternClientServices(contentManagementOptions, http, jsonSerializer, api, pollingStrategy, authProvider, cryptoProvider);
            return new Extern(services);
        }
        
        private HttpRequestsFactory CreateHttp(
            IHttpClientConfiguration clientConfiguration, 
            RequestTimeouts requestTimeouts, 
            IAuthenticationProvider authenticationProvider, 
            IJsonSerializer jsonSerializer,
            ILog log)
        {
            FailoverAsync? unauthorizedFailover =
                EnableUnauthorizedFailover
                    ? (response, attempt) => AuthorizationErrorsFailover(requestTimeouts, authenticationProvider, response, attempt)
                    : null;
            
            return new HttpRequestsFactory(
                clientConfiguration,
                requestTimeouts,
                (request, span) => AuthenticateRequestAsync(authenticationProvider, request, span),
                HandleApiErrors,
                unauthorizedFailover,
                jsonSerializer,
                log
            );
            
            static async Task<Request> AuthenticateRequestAsync(IAuthenticationProvider authProvider, Request request, TimeSpan timeout)
            {
                return await authProvider.AuthenticateRequestAsync(request, false, timeout).ConfigureAwait(false);
            }

            static async ValueTask<bool> HandleApiErrors(IHttpResponse response)
            {
                var status = response.Status;
                if (status.IsClientError)
                {
                    var errorResponse = await response.TryGetMessageAsync<ApiError>().ConfigureAwait(false);
                    if (errorResponse is not null && errorResponse.IsNotEmpty)
                    {
                        throw Errors.UnsuccessfulApiResponse(errorResponse);
                    }
                }

                return false;
            }

            static async Task<FailoverDecision> AuthorizationErrorsFailover(RequestTimeouts timeouts, IAuthenticationProvider authProvider, IHttpResponse response, uint attempt)
            {
                switch (attempt)
                {
                    case 0 when response.Status.IsUnauthorized:
                        await authProvider.AuthenticateAsync(true, timeouts.DefaultReadTimeout).ConfigureAwait(false);
                        return FailoverDecision.RepeatRequest;

                    default:
                        return FailoverDecision.LetItFail;
                }
            }
        }
        
        private IAuthenticationProvider CreateAuthProvider(OpenIdSetup? openIdSetup, ILog log)
        {
            if (openIdSetup is not null)
            {
                var builder = new OpenIdAuthenticationProviderBuilder(log);
                var configuredBuilder = openIdSetup(builder);
                return configuredBuilder.Build();
            }

            throw Errors.TheAuthProviderNotSpecifiedOrUnsupported();
        }

        private class Extern : IExtern
        {
            private readonly IExternClientServices services;

            public Extern(IExternClientServices services) => this.services = services;

            public Task ReauthenticateAsync(TimeSpan? timeout) => 
                services.AuthProvider.AuthenticateAsync(true, timeout);

            public AccountListPath Accounts => new(services);
        }
    }
}