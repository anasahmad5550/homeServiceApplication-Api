using HSA.Services.ServiceModels.Shared;
using System.ComponentModel.DataAnnotations;
using HSA.DB.Model.EF.Models;
using Core.Repository.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSA.Services.ServiceModels
{
    public class CategorySM : BaseServiceModel<Category, CategorySM>, IObjectState
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ObjectState ObjectState { get; set; }


    }
}
