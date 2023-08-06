using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Pagination_Example.Model
{
    [Table("Addresses")]
    public class Address : Entity
    {
        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string State { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string ZipCode { get; set; } = string.Empty;

        [Required]
        public int Number { get; set; }

        public Audit Audit { get; set; }
        
        /* EF One-to-One */
        public Supplier? Supplier { get; set; }


        public Address()
        {
            Audit = new Audit();
        }

        public Address(Guid supplierId, string country, string state, string city, string zipCode, int number, Audit audit)
        {
            SupplierId=supplierId;
            Country=country;
            State=state;
            City=city;
            ZipCode=zipCode;
            Number=number;
            Audit=audit;
        }
    }
}
