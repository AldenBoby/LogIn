using RembraceLogIn.Shared.Models;

namespace RembraceLogIn.Client.Service
{
    public interface IAuthService
    {
        Task<RegisterResult> Register(RegisterModel registerModel);

        Task<LoginResult> Login(LoginModel loginModel);

        Task Logout();
    }
}
