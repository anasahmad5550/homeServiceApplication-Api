using HSA.Services.ServiceModels.Shared;
using System.ComponentModel.DataAnnotations;
using HSA.DB.Model.EF.Models;
using Core.Repository.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSA.Services.ServiceModels
{
    public class ServiceSM : BaseServiceModel<Service, ServiceSM>, IObjectState
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? Location { get; set; }

        public decimal? Price { get; set; }

        public Category? Category { get; set; }

        public int? SellerId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("Service")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        [ForeignKey("SellerId")]
        [InverseProperty("Services")]
        public virtual User? Seller { get; set; }
        public ObjectState ObjectState { get; set; }


    }
}
