using SchoolIsComingSoon.Application.Interfaces;
using System.Security.Claims;

namespace SchoolIsComingSoon.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CurrentUserService> _logger;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, ILogger<CurrentUserService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public Guid UserId
        {
            get
            {
                var id = User?.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? User?.FindFirstValue("sub");

                if (!Guid.TryParse(id, out var guid))
                {
                    _logger.LogWarning("❗ Cannot parse UserId. Claims: {Claims}",
                        string.Join(", ", User?.Claims.Select(c => $"{c.Type}={c.Value}") ?? []));
                    return Guid.Empty;
                }

                return guid;
            }
        }

        public string UserName =>
            User?.FindFirstValue("name") ?? string.Empty;

        public string FirstName =>
            User?.FindFirstValue(ClaimTypes.GivenName)
            ?? User?.FindFirstValue("given_name")
            ?? string.Empty;

        public string LastName =>
            User?.FindFirstValue(ClaimTypes.Surname)
            ?? User?.FindFirstValue("family_name")
            ?? string.Empty;

        public string Email =>
            User?.FindFirstValue(ClaimTypes.Email)
            ?? User?.FindFirstValue("email")
            ?? User?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
            ?? string.Empty;

        public string Role =>
            User?.FindFirstValue(ClaimTypes.Role)
            ?? User?.FindFirstValue("role")
            ?? string.Empty;
    }
}