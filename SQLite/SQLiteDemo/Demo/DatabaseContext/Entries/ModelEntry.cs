using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DatabaseContext.Entries
{
    public class ModelEntry
    {
        [Key]
        public int Id { get; set; }
        [Index("IX_ModelEntry_ModelUniqueID", IsUnique = true, Order = 1)]
        [MaxLength(100)]
        public string ModelId { get; set; }
    }
}