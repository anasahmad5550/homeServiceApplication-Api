using HSA.DB.Model.EF.Models;
using System.ComponentModel.DataAnnotations;

public class UserRoleValidationAttribute : ValidationAttribute
{
    private readonly UserRole[] _validRoles = { UserRole.Seller, UserRole.Customer };

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is UserRole role && Array.Exists(_validRoles, r => r == role))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid role. Allowed values are Seller and Customer.");
    }
}
