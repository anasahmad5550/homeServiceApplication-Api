using Core.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSA.DB.Model.EF.Models;

public partial class User : Entity
{

}

public enum UserRole
{
    Customer,
    Seller,
    Admin
}