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

        public Category? Category { get; set; }

        public int? SellerId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        //[InverseProperty("Service")]
        //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        //[ForeignKey("SellerId")]
        //[InverseProperty("Services")]
        //public virtual User? Seller { get; set; }

    }

}
