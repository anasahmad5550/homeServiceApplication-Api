using HSA.Services.ServiceModels.Shared;
using System.ComponentModel.DataAnnotations;
using HSA.DB.Model.EF.Models;
using Core.Repository.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DataMapper;

namespace HSA.Services.ServiceModels
{
    public class ServiceSM : BaseServiceModel<Service, ServiceSM>, IObjectState
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? Location { get; set; }

        public decimal? Price { get; set; }

        public int? Category_id { get; set; }

        public int? SellerId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        [Complex]
        public UserSM? Seller { get; set; }
        [Complex]
        public CategorySM? Category { get; set; }
        public ObjectState ObjectState { get; set; }


    }
}
