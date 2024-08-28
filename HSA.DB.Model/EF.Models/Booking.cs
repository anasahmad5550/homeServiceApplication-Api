using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HSA.DB.Model.EF.Models;

[Table("bookings")]
public partial class Booking
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("bookedAt")]
    public DateTime? BookedAt { get; set; }

    [Column("serviceId")]
    public int ServiceId { get; set; }

    [Column("customerId")]
    public int CustomerId { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Bookings")]
    public virtual User Customer { get; set; } = null!;

    [InverseProperty("Booking")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [ForeignKey("ServiceId")]
    [InverseProperty("Bookings")]
    public virtual Service Service { get; set; } = null!;
}
