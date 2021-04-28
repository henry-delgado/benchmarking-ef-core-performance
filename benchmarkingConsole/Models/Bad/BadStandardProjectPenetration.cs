using System.ComponentModel.DataAnnotations;
namespace benchmarkingConsole.Models.Bad
{
    public class BadStandardProjectPenetration
    {
        public BadStandardProjectPenetration()
        {
        }
        [Key]
        public long Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        [MaxLength(4000)]
        public string Value { get; set; }

        public long BadProjectId { get; set; }

        public BadProject BadProject { get; set; }

        [MaxLength(1)]
        public string IsCustomizable { get; set; }

        [MaxLength(1)]
        public string IsEditable { get; set; }

        public int Priority { get; set; }

        public int CategoryId { get; set; }

        [MaxLength(1)]
        public string IsDeleted { get; set; }
    }
}
