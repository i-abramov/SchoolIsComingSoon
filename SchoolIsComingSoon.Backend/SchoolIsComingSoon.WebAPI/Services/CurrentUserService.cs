using SchoolIsComingSoon.Application.Interfaces;
using System.Security.Claims;

namespace SchoolIsComingSoon.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public Guid UserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User?
                    .FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
        }

        public string UserName
        {
            get
            {
                var name = _httpContextAccessor.HttpContext?.User?.FindFirstValue("name");
                return string.IsNullOrEmpty(name) ? string.Empty : name;
            }
        }

        public string FirstName
        {
            get
            {
                var given_name = _httpContextAccessor.HttpContext?.User?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");
                return string.IsNullOrEmpty(given_name) ? string.Empty : given_name;
            }
        }
        public string LastName
        {
            get
            {
                var family_name = _httpContextAccessor.HttpContext?.User?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname");
                return string.IsNullOrEmpty(family_name) ? string.Empty : family_name;
            }
        }
        public string Email
        {
            get
            {
                var email = _httpContextAccessor.HttpContext?.User?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
                return string.IsNullOrEmpty(email) ? string.Empty : email;
            }
        }

        public string Role
        {
            get
            {
                var role = _httpContextAccessor.HttpContext?.User?
                    .FindFirstValue(ClaimTypes.Role);
                return string.IsNullOrEmpty(role) ? string.Empty : role;
            }
        }
    }
}