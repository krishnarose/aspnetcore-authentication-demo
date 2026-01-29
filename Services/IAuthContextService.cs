using AuthProject.Models;
using AuthProject.Models.ServiceModel;

namespace AuthProject.Services
{
    public interface IAuthContextService
    {
        bool SetSession(User user);
        AuthSession? GetSession();
        bool ClearSession();
    }
    
}