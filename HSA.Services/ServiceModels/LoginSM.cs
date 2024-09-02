using HSA.Services.ServiceModels.Shared;
using System.ComponentModel.DataAnnotations;
using HSA.DB.Model.EF.Models;
using Core.Repository.Infrastructure;

namespace HSA.Services.ServiceModels
{
    public class LoginSM : BaseServiceModel<User, LoginSM>, IObjectState
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Password { get; set; } = null!;
        public ObjectState ObjectState { get; set; }

    }
}
