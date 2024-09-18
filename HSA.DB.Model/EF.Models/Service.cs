using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HSA.DB.Model.EF.Models;

[Table("services")]
public partial class Service
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("location")]
    [StringLength(50)]
    public string? Location { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [Column("category_id")]
    public int? CategoryId { get; set; }

    [Column("sellerId")]
    public int? SellerId { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "int")]
    public ServiceStatus Status { get; set; }

    [Column("comments")]
    [StringLength(255)]
    public string? Comments { get; set; }

    [Column("rejection_reason")]
    [StringLength(250)]
    [Unicode(false)]
    public string? RejectionReason { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Services")]
    public virtual Category? Category { get; set; }

    [ForeignKey("SellerId")]
    [InverseProperty("Services")]
    public virtual User? Seller { get; set; }
}
