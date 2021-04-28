using System.ComponentModel.DataAnnotations;

namespace benchmarkingConsole.Models.Bad
{
    public class BadPenetrationAttribute
    {
        public BadPenetrationAttribute()
        {
        }

        [Key]
        public long Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Type { get; set; }

        [MaxLength(4000)]
        public string SelectedValue { get; set; }
        
        public long BadPenetrationId { get; set; }

        public BadPenetration BadPenetration {get;set;}

        [MaxLength(1)]
        public string IsEditable { get; set; }

        public int Priority { get; set; }

        public int CategoryId { get; set; }
    }
}
