using HomeServiceApplication_Api.ViewModels.Shared;
using HSA.DB.Model.EF.Models;
using HSA.Services.ServiceModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeServiceApplication_Api.ViewModels
{
    public class ServiceVM : BaseAutoViewModel<ServiceSM, ServiceVM>
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Location { get; set; }

        public decimal? Price { get; set; }

        public int? Category_id { get; set; }

        public int? SellerId { get; set; }
        public ServiceStatus status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //[InverseProperty("Service")]
        //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        //[ForeignKey("SellerId")]
        public UserVM? Seller { get; set; }
        public CategoryVM? Category { get; set; }

    }

}
