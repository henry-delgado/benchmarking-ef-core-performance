using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace benchmarkingConsole.Models.Bad
{
    public class BadPenetration
    {
        public BadPenetration()
        {
        }

        [Key]
        public long Id { get; set; }

        public long BadProjectId { get; set; }

        public BadProject BadProject { get; set; }

        [MaxLength(12)]
        public string QrCode { get; set; }

        public int PenetrationNumber { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        public long ProductId { get; set; }

        public long ApprovalId { get; set; }

        [MaxLength(50)]
        public string LocalUniqueId { get; set; }

        public long Level1Id { get; set; }

        public long Level2Id { get; set; }

        public long Level3Id { get; set; }

        public long CategoryId { get; set; }

        public ICollection<BadPenetrationAttribute> BadPenetrationAttributes { get; set; }

    }
}
