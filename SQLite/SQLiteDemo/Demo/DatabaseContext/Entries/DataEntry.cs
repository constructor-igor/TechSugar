using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.DatabaseContext.Entries
{
    public class DataEntry
    {
        [Key]
        public int Id { get; set; }
        public String Content { get; set; }

        public virtual ModelEntry ModelEntry { get; set; }
        public virtual PointEntry PointEntry { get; set; }
    }
}