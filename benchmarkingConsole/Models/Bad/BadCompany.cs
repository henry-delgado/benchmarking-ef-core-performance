using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace benchmarkingConsole.Models.Bad
{
    public class BadCompany
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

        public int PortfolioId { get; set; }

        public int LicenseId { get; set; }
                
        public ICollection<BadProject> BadProjects { get; set; }

    }
}