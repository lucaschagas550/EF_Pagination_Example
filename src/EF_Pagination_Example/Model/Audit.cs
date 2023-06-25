using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EF_Pagination_Example.Model
{
    [Owned]
    public class Audit
    {
        [ForeignKey(nameof(Created))]
        [Column("CreatedBy")]
        public string CreatedBy { get; set; } = string.Empty;

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(Updated))]
        [Column("UpdatedBy")]
        public string? UpdatedBy { get; set; }

        [Column("UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        /* EF One-to-One */
        public AppUser? Created { get; set; }

        /* EF One-to-One */
        public AppUser? Updated { get; set; }

        public Audit()
        {
        }

        public Audit(string createdBy, DateTime createdDate, string updatedBy, DateTime updatedDate)
        {
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            UpdatedBy = updatedBy;
            UpdatedDate = updatedDate;
        }

        public void CreationAudit(Guid userId)
        {
            CreatedBy = userId.ToString();
            CreatedDate = DateTime.Now;
        }

        public void UpdateAudit(Guid userId)
        {
            UpdatedBy = userId.ToString();
            UpdatedDate = DateTime.Now;
        }
    }
}
