using HomeServiceApplication_Api.ViewModels.Shared;
using HSA.Services.ServiceModels;
namespace HomeServiceApplication_Api.ViewModels
{
    public class LoginVM : BaseAutoViewModel<LoginSM, LoginVM>
    {
        public string Email { get; set; } 
        public string Password { get; set; }
    }
}
