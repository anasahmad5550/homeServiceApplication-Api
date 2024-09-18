
using Core.Repository;

namespace HSA.DB.Model.EF.Models;


public partial class Service : Entity
{
    
}
public enum ServiceStatus
{
    InReview,
    Approved,
    Rejected
}