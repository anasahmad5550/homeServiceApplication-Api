using HomeServiceApplication_Api.ViewModels.Shared;
using HSA.DB.Model.EF.Models;
using HSA.Services.ServiceModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeServiceApplication_Api.ViewModels
{
    public class UserVM : BaseAutoViewModel<UserSM,UserVM>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        [Required]
        public string PasswordHash { get; set; } = null!;

        [StringLength(50)]
        [Required]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        public UserRole? Role { get; set; }

    }

}
