using HomeServiceApplication_Api.ViewModels.Shared;
using HSA.Services.ServiceModels;
using HSA.Services.Shared;

namespace HomeServiceApplication_Api.ViewModels
{
    public class RejectServiceVM : BaseAutoViewModel<ServiceSM, RejectServiceVM>
    {
        public string? RejectionReason { get; set; }
    }
}
