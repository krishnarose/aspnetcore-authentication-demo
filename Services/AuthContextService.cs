using System.Text.Json;
using AuthProject.Models.DomainModels;
using AuthProject.Models.ServiceModel;

namespace AuthProject.Services
{
    public class AuthContextService : IAuthContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool SetSession(User user)
        {
            try
            {
                var session = new AuthSession
                {
                    Username = user.username,
                    Role = user.role,
                    SessionId = Guid.NewGuid(),
                    Name = user.name
                };

                var sessionString = JsonSerializer.Serialize(session);

                _httpContextAccessor.HttpContext.Session.SetString("AuthSession", sessionString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AuthSession? GetSession()
        {
            try
            {
                var sessionString = _httpContextAccessor.HttpContext.Session.GetString("AuthSession");
                if (string.IsNullOrEmpty(sessionString))
                {
                    return null;
                }

                var session = JsonSerializer.Deserialize<AuthSession>(sessionString);
                return session;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ClearSession()
        {
            try
            {
                _httpContextAccessor.HttpContext.Session.Remove("AuthSession");
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }

}