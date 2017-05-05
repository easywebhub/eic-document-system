using eic.middleware.idsrv_wrapper.Models;

namespace ew.middleware.idsrv_wrapper
{
    public interface IIdSrvManager
    {
        IdSrvUser UserAdded { get; }
        IdSrvUserDetail UserLoggedFullInfo { get; }
        bool RegisterUser(RegisterUserDto dto);
        bool SignIn(SignInDto dto);
        string GetAccessToken();
    }
}