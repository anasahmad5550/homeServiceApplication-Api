using HomeServiceApplication_Api.ViewModels.Shared;
using HSA.Services.ServiceModels;
using System.ComponentModel.DataAnnotations;

namespace HomeServiceApplication_Api.ViewModels
{
    public class CategoryVM : BaseAutoViewModel<CategorySM, CategoryVM>
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(50)]
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
