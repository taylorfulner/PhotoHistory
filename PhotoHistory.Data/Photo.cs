using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Data
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [Required]
        public string PhotoName { get; set; }

        public string PhotoDesc { get; set; }

        public DateTime PhotoDate { get; set; }

        [Required]
        public string PhotoStorageLocation { get; set; }

        [Required]
        [Display(Name = "Uploaded By")]
        public Guid AdminId { get; set; }

        [Required]
        public DateTimeOffset PhotoUploadDate { get; set; }

        public virtual List<PhotoTag> Tags { get; set; } = new List<PhotoTag>();

    }
}
