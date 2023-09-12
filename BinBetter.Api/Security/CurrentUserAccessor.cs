using System.Security.Claims;

namespace BinBetter.Api.Security
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            var value = _httpContextAccessor.HttpContext
                ?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            int userId = 0;
            Int32.TryParse(value, out userId);

            return userId;
        }

        public string? GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext
                ?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)
                ?.Value;
        }
    }
}
