using HSA.Services.ServiceModels.Shared;
using HSA.DB.Model.EF.Models;
using Core.Repository.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace HSA.Services.ServiceModels
{
    public class UserSM : BaseServiceModel<User, UserSM>, IObjectState
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string salt { get; set; } = null!;
        public UserRole? Role { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ObjectState ObjectState { get; set; }
    }
}
