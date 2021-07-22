using Kontur.Extern.Client.Authentication.OpenId.Time;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider
{
    internal class OpenIdAuthenticationResult : IAuthenticationResult
    {
        public OpenIdAuthenticationResult(string accessToken, TimeInterval remainingTime)
        {
            AccessToken = accessToken;
            RemainingTime = remainingTime;
        }

        public string AccessToken { get; }
        public TimeInterval RemainingTime { get; }

        public Request Apply(Request request) => request.WithAuthorizationHeader("Bearer", AccessToken); 
    }
}