using System.Net.Http.Headers;

namespace DocArchive.Services
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly AuthService _authService;

        public AuthMessageHandler(AuthService authService)
        {
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _authService.GetToken();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}