using System;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.API.Models
{
#pragma warning disable CS1591
    public class PostJobRequest
    {
        [Key]
        public int? JobID { get; set; }
        [Required]
        [StringLength(200)]
        public string JobTitle { get; set; }
        [Required]
        public int? CompanyID { get; set; }
        [Required]
        [StringLength(50)]
        public string Location { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        public int? Salary { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? DatePosted { get; set; }
        public DateTime? DateExpires { get; set; }
    }

    public class UpdateJobRequest
    {
        [Required]
        [StringLength(200)]
        public string StockItemName { get; set; }

        [Required]
        public int? SupplierID { get; set; }

        public int? ColorID { get; set; }

        [Required]
        public decimal? UnitPrice { get; set; }
    }

    public static class Extensions
    {
        public static Job ToEntity(this PostJobRequest request)
            => new Job
            {
                JobID = request.JobID,
                JobTitle = request.JobTitle,
                CompanyID = request.CompanyID,
                Location = request.Location,
                Description = request.Description,
                Salary = request.Salary,
                ContactEmail = request.ContactEmail,
                DatePosted = request.DatePosted,
                DateExpires = request.DateExpires,
            };
    }
#pragma warning restore CS1591
}
