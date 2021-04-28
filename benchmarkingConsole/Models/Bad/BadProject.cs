using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace benchmarkingConsole.Models.Bad
{
    /// <summary>
    /// A Project model to exemplify the current state of a bad database design
    /// </summary>
    public class BadProject
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Building { get; set; }

        [MaxLength(200)]
        public string Address1 { get; set; }

        [MaxLength(200)]
        public string Address2 { get; set; }

        [MaxLength(200)]
        public string City { get; set; }

        [MaxLength(200)]
        public string State { get; set; }

        [MaxLength(20)]
        public string Zip { get; set; }

        [MaxLength(4000)]
        public string Comments { get; set; }

        [MaxLength(200)]
        public string ClientName { get; set; }

        [MaxLength(200)]
        public string ClientContactPerson { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        [MaxLength(4000)]
        public string CreatedBy { get; set; }

        [MaxLength(4000)]
        public string ModifiedBy { get; set; }

        public long BadCompanyId { get; set; }

        public BadCompany BadCompany { get; set; }
                
        public ICollection<BadStandardProjectPenetration> BadStandardProjectPenetrations { get; set; }
                
        public ICollection<BadPenetrationAttribute> BadPenetrationAttributes { get; set; }

        public ICollection<BadPenetration> BadPenetrations { get; set; }

    }
}