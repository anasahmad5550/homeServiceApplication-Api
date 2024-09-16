using HomeServiceApplication_Api.ViewModels.Shared;
using HSA.DB.Model.EF.Models;
using HSA.Services.ServiceModels;

namespace HomeServiceApplication_Api.ViewModels
{
    public class BookingVM : BaseAutoViewModel<BookingSM,  BookingVM>
    {
        public int Id { get; set; }
        public DateTime? BookedAt { get; set; }
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserSM Customer { get; set; } = null!;
        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ServiceSM Service { get; set; } = null!;
    }
}
