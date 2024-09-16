using HSA.DB.Model.EF.Models;
using HSA.Services.ServiceModels.Shared;
using Core.DataMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Infrastructure;

namespace HSA.Services.ServiceModels
{
    public class BookingSM : BaseServiceModel<Booking, BookingSM>, IObjectState
    {
        public int Id { get; set; }

        public DateTime? BookedAt { get; set; }

        public int ServiceId { get; set; }

        public int CustomerId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual User Customer { get; set; } = null!;

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [Complex]
        public virtual Service Service { get; set; } = null!;
        public ObjectState ObjectState { get; set; }

    }
}
