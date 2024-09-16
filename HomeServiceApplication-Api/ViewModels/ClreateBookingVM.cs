using HomeServiceApplication_Api.ViewModels.Shared;
using HSA.Services.ServiceModels;

namespace HomeServiceApplication_Api.ViewModels
{
    public class CreateBookingVM : BaseAutoViewModel<BookingSM, CreateBookingVM>
    {
        public DateTime? BookedAt { get; set; }
        public int ServiceId { get; set; }
    }
}
